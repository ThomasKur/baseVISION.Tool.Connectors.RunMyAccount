using baseVISION.Core.Connectors.RunMyAccount.Model;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace baseVISION.Core.Connectors.RunMyAccount
{
    public partial class RunMyAccountsClient
    {

        public async Task CreateInvoiceAsync(RunMyAccountsInvoice i)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9_\\.\\-\\\\]");
            i.ordnumber = rgx.Replace(i.ordnumber, "");
            var request = new RestRequest("{tenant}/invoices", Method.Post);
            request.RequestFormat = DataFormat.Json;
            // request.JsonSerializer = serializer;
            request.AddJsonBody(i);
            //request.AddBody(i);
            var response = await client.ExecuteAsync(request);
            if (response.ErrorException != null)
            {
                throw new Exception("Failed to add RMA Invoice: " + response.ErrorMessage);
            }
            if (response.Content.Contains("{ \"error\": \""))
            {
                throw new Exception("Failed to add RMA Invoice: " + response.Content);
            }
            if (response.ResponseStatus == ResponseStatus.Error)
            {
                throw new Exception("Failed to create RMA Invoice with 408");
            }
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new Exception("Failed to create RMA Invoice with " + response.StatusDescription);
            }
        }
        public void CreateInvoice(RunMyAccountsInvoice i)
        {

            Task.Run(() => CreateInvoiceAsync(i));
        }
        public async Task<String> CreateInvoiceWithStatusAsync(RunMyAccountsInvoice i)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9_\\.\\-\\\\]");
            i.ordnumber = rgx.Replace(i.ordnumber, "");
            var request = new RestRequest("{tenant}/invoices", Method.Post);
            request.RequestFormat = DataFormat.Json;
            // request.JsonSerializer = serializer;
            request.AddJsonBody(i);
            //request.AddBody(i);
            var response = await client.ExecuteAsync(request);
            if (response.ErrorException != null)
            {
                throw new Exception("Failed to add RMA Invoice: " + response.ErrorMessage);
            }
            if (response.Content.Contains("{ \"error\": \""))
            {
                throw new Exception("Failed to add RMA Invoice: " + response.Content);
            }
            if (response.ResponseStatus == ResponseStatus.Error)
            {
                throw new Exception("Failed to create RMA Invoice with 408");
            }
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new Exception("Failed to create RMA Invoice with http Status Code" + response.StatusDescription);
            }
            return response.Content;
        }
        public string CreateInvoiceWithStatus(RunMyAccountsInvoice i)
        {

            return Task.Run(() => CreateInvoiceWithStatusAsync(i)).Result;
        }

        public async Task<List<RunMyAccountsInvoiceExist>> ListAllInvoicesAsync2(DateTime? from, DateTime? to)
        {
            var request = new RestRequest("{tenant}/invoices", Method.Get);
            request.RequestFormat = DataFormat.Json;
            if (from != null)
            {
                request.AddQueryParameter("from", from.Value.ToString("yyyy-MM-dd"));
            }
            if (to != null)
            {
                request.AddQueryParameter("to", to.Value.ToString("yyyy-MM-dd"));
            }
            var response = await client.ExecuteAsync<RunMyAccountsInvoiceExistList>(request);
            
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
        [Obsolete("ListAllInvoicesAsync is deprecated, please use ListAllInvoicesAsync2 instead.")]
        public async Task<List<RunMyAccountsInvoiceExist>> ListAllInvoicesAsync()
        {
            HttpClient cl = new HttpClient();
            cl.Timeout = new TimeSpan(0,0,30);
            cl.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
            var result = cl.GetAsync("https://service.runmyaccounts.com/api/latest/clients/basevision/invoices?api_key="+ApiKey);
            result.Wait();
            var contentstream = await result.Result.Content.ReadAsStringAsync();
            RunMyAccountsInvoiceExistList l = JsonConvert.DeserializeObject<RunMyAccountsInvoiceExistList>(contentstream);
            
            
            
            if (contentstream.Contains("{ \"error\": \""))
            {
                throw new Exception("Failed to get RMA Invoices: " + contentstream);
            }
            return l.invoice.ToList<RunMyAccountsInvoiceExist>();
        }
        [Obsolete("ListAllInvoices is deprecated, please use ListAllInvoices2 instead.")]
        public List<RunMyAccountsInvoiceExist> ListAllInvoices()
        {
            return Task.Run(() => ListAllInvoicesAsync()).Result;
        }

        public List<RunMyAccountsInvoiceExist> ListAllInvoices2(DateTime? from, DateTime? to)
        {
            return Task.Run(() => ListAllInvoicesAsync2(from, to)).Result;
        }
        public List<RunMyAccountsInvoiceExist> ListAllInvoices2()
        {
            return Task.Run(() => ListAllInvoicesAsync2(null,null)).Result;
        }


        public async Task<RunMyAccountsInvoiceExist> GetInvoiceAsync(string invoicenr)
        {
            var request = new RestRequest("{tenant}/invoices/{invoicenr}", Method.Get);
            request.AddParameter("invoicenr", invoicenr, ParameterType.UrlSegment);
            request.RequestFormat = DataFormat.Json;
            var response = await client.ExecuteAsync<RunMyAccountsInvoiceExist>(request);
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
        public RunMyAccountsInvoiceExist GetInvoice(string invoicenr)
        {
            return Task.Run(() => GetInvoiceAsync(invoicenr)).Result;
        }


        public async Task<byte[]> DownloadInvoicePDFAsync(string invoicenr)
        {
            var request = new RestRequest("{tenant}/invoices/{invoicenr}/pdf", Method.Get);
            request.AddParameter("invoicenr", invoicenr, ParameterType.UrlSegment);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "application/pdf");
            var response = await client.ExecuteAsync(request);
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
        public byte[] DownloadInvoicePDF(string invoicenr)
        {

            return Task.Run(() => DownloadInvoicePDFAsync(invoicenr)).Result;
        }

        public void DownloadInvoicePDF(string invoicenr, string savePath)
        {
            byte[] file = DownloadInvoicePDF(invoicenr);
            File.WriteAllBytes(savePath, file);
        }
    }
}
