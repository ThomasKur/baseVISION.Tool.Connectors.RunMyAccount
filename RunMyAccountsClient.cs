using baseVISION.Tool.Connectors.RunMyAccount.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace baseVISION.Tool.Connectors.RunMyAccount
{
    public class RunMyAccountsClient
    {
        private string ApiKey;
        private RestClient client;
        public NewtonsoftJsonSerializer serializer = null;
        public RunMyAccountsClient(string tenant, string apikey)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;

            ApiKey = apikey;
            client = new RestClient("https://service.runmyaccounts.com/api/latest/clients/");
            client.Encoding = Encoding.UTF8;
            client.AddDefaultHeader("ContentType", "application/json");
            client.AddDefaultParameter("api_key", apikey, ParameterType.QueryString);
            client.AddDefaultParameter("tenant", tenant, ParameterType.UrlSegment);

            // Setting up JSON Serialization Engine
            serializer = NewtonsoftJsonSerializer.Default;
        }
        public RunMyAccountsClient(string tenant, string apikey, string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            ApiKey = apikey;
            client = new RestClient(url);
            client.Encoding = Encoding.UTF8;
            client.AddDefaultHeader("ContentType", "application/json");
            client.AddDefaultParameter("api_key", apikey, ParameterType.QueryString);
            client.AddDefaultParameter("tenant", tenant, ParameterType.UrlSegment);

            // Setting up JSON Serialization Engine
            serializer = NewtonsoftJsonSerializer.Default;
        }
        public List<RunMyAccountsContact> ListCustomers()
        {
            var request = new RestRequest("{tenant}/customers", Method.GET);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute<RunMyAccountsContactList>(request);
            if (response.ErrorException != null)
            {
                throw new Exception("Failed to get RMA Contacts: " + response.ErrorMessage);
            }
            if (response.Content.Contains("{ \"error\": \""))
            {
                throw new Exception("Failed to get RMA Contacts: " + response.Content);
            }
            return response.Data.customer.ToList<RunMyAccountsContact>();
        }
        
        public void CreateCustomer(RunMyAccountsContact r)
        {
            
            var request = new RestRequest("{tenant}/customers", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = serializer;
            request.AddBody(r);
            try
            {
                var response = client.Execute(request);
                if (response.ErrorException != null)
                {
                    throw new Exception("Failed to add RMA Contact: " + response.ErrorMessage);

                }
                if (response.Content.Contains("{ \"error\": \""))
                {
                    throw new Exception("Failed to add RMA Contact: " + response.Content);
                }
            } catch (Exception e)
            {
                throw new Exception("Adding failed with: " + e.StackTrace);
            }
            
        }
        public void CreateInvoice(RunMyAccountsInvoice i)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9_\\.\\-\\\\]");
            i.ordnumber = rgx.Replace(i.ordnumber, "");
            var request = new RestRequest("{tenant}/invoices", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = serializer;
            request.AddBody(i);
            var response = client.Execute(request);
            if (response.ErrorException != null)
            {
                throw new Exception("Failed to add RMA Invoice: " + response.ErrorMessage);
            }
            if (response.Content.Contains("{ \"error\": \""))
            {
               throw new Exception("Failed to add RMA Invoice: " + response.Content);
            }
        }
        public List<RunMyAccountsInvoiceExist> ListAllInvoices()
        {
            var request = new RestRequest("{tenant}/invoices", Method.GET);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute<RunMyAccountsInvoiceExistList>(request);
            if (response.ErrorException != null)
            {
                throw new Exception("Failed to get RMA Invoices: " + response.ErrorMessage);
            }
            if (response.Content.Contains("{ \"error\": \""))
            {
                throw new Exception("Failed to get RMA Invoices: " + response.Content);
            }
            return response.Data.invoice.ToList<RunMyAccountsInvoiceExist>();
        }
        public List<RunMyAccountsSaldo> ListAllSaldo(string accno, DateTime? from, DateTime? to)
        {
            var request = new RestRequest("{tenant}/gl/saldo", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddQueryParameter("accno", accno);
            request.AddQueryParameter("exclude_yearend_bookings", "true");
            if (from != null)
            {
                request.AddQueryParameter("from", from.Value.ToString("yyyy-MM-dd"));
            }
            if (to != null)
            {
                request.AddQueryParameter("to", to.Value.ToString("yyyy-MM-dd"));
            }
            request.Timeout = 3000;
            var response = client.Execute<RunMyAccountsSaldoList>(request);
            if (response.ErrorException != null)
            {
                throw new Exception("Failed to get RMA Saldo: " + response.ErrorMessage);
            }
            if (response.Content.Contains("{ \"error\": \""))
            {
                throw new Exception("Failed to get RMA Saldo: " + response.Content);
            }
            return response.Data.row.ToList<RunMyAccountsSaldo>();
        }
        public RunMyAccountsInvoiceExist GetInvoice(string invoicenr)
        {
            var request = new RestRequest("{tenant}/invoices/{invoicenr}", Method.GET);
            request.AddParameter("invoicenr", invoicenr, ParameterType.UrlSegment);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute<RunMyAccountsInvoiceExist>(request);
            if (response.ErrorException != null)
            {
                throw new Exception("Failed to get RMA Invoice " + invoicenr + ": " + response.ErrorMessage);
            }
            if (response.Content.Contains("{ \"error\": \""))
            {
                throw new Exception("Failed to get RMA Invoice: " + response.Content);
            }
            return response.Data;
        }
        public List<RunMyAccountsArticle> ListAllArticles()
        {
            var request = new RestRequest("{tenant}/parts", Method.GET);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute<RunMyAccountsArticleList>(request);
            if (response.ErrorException != null)
            {
                throw new Exception("Failed to get RMA Articles: " + response.ErrorMessage);
            }
            if (response.Content.Contains("{ \"error\": \""))
            {
                throw new Exception("Failed to get RMA Articles: " + response.Content);
            }
            return response.Data.part.ToList<RunMyAccountsArticle>();
        }
        public byte[] DownloadInvoicePDF(string invoicenr)
        {
            var request = new RestRequest("{tenant}/invoices/{invoicenr}/pdf", Method.GET);
            request.AddParameter("invoicenr", invoicenr, ParameterType.UrlSegment);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "application/pdf");
            var response = client.Execute(request);
            if (response.ErrorException != null)
            {
                throw new Exception("Failed to download RMA Invoice PDF " + invoicenr + ": " + response.ErrorMessage);
            }
            if (response.Content.Contains("{ \"error\": \""))
            {
                throw new Exception("Failed to download RMA Invoice PDF " + invoicenr + ": " + response.Content);
            }
            return response.RawBytes;
        }
        public void DownloadInvoicePDF(string invoicenr, string savePath)
        {
            byte[] file = DownloadInvoicePDF(invoicenr);
            File.WriteAllBytes(savePath, file);
        }
    }
}
