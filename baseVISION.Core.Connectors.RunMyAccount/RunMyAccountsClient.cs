using baseVISION.Core.Connectors.RunMyAccount.Model;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace baseVISION.Core.Connectors.RunMyAccount
{
    public class RunMyAccountsClient
    {
        private string ApiKey;
        private RestClient client;

        public RunMyAccountsClient(string tenant, string apikey)
        {
            // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;

            ApiKey = apikey;
            
            RestClientOptions option = new RestClientOptions("https://service.runmyaccounts.com/api/latest/clients/");
            option.Encoding = Encoding.UTF8;
            option.MaxTimeout = 30000;
            
            client = new RestClient(option, configureSerialization: s => s.UseSerializer(() => new NewtonsoftJsonSerializer()));
     
            client.AddDefaultHeader("ContentType", "application/json");
            client.AddDefaultParameter("api_key", apikey, ParameterType.QueryString);
            client.AddDefaultParameter("tenant", tenant, ParameterType.UrlSegment);

        }
        public RunMyAccountsClient(string tenant, string apikey, string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            ApiKey = apikey;

            RestClientOptions option = new RestClientOptions(url);
            option.Encoding = Encoding.UTF8;
            option.MaxTimeout = 10000;
            client = new RestClient(option, configureSerialization: s => s.UseSerializer(() => new NewtonsoftJsonSerializer()));
            client.AddDefaultHeader("ContentType", "application/json");
            client.AddDefaultParameter("api_key", apikey, ParameterType.QueryString);
            client.AddDefaultParameter("tenant", tenant, ParameterType.UrlSegment);

        }
        public async Task<List<RunMyAccountsContact>> ListCustomersAsync()
        {
            var request = new RestRequest("{tenant}/customers", Method.Get);
            request.RequestFormat = DataFormat.Json;
            var response = await client.ExecuteAsync<RunMyAccountsContactList>(request);
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
        public List<RunMyAccountsContact> ListCustomers()
        {
            return Task.Run(() => ListCustomersAsync()).Result;
        }

        public async Task CreateCustomerAsync(RunMyAccountsContact r)
        {
            
            var request = new RestRequest("{tenant}/customers", Method.Post);
            request.RequestFormat = DataFormat.Json;
           // request.JsonSerializer = serializer;
            request.AddBody(r);
            try
            {
                var response = await client.ExecuteAsync(request);
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
        public void CreateCustomer(RunMyAccountsContact r)
        {
            Task.Run(() => CreateCustomerAsync(r));
        }
        public async Task<string> CreateCustomerWithStatusAsync(RunMyAccountsContact r)
        {

            var request = new RestRequest("{tenant}/customers", Method.Post);
            request.RequestFormat = DataFormat.Json;
            // request.JsonSerializer = serializer;
            request.AddJsonBody(r);
            try
            {
                var response = await client.ExecuteAsync(request);
                if (response.ErrorException != null)
                {
                    throw new Exception("Failed to add RMA Contact: " + response.ErrorMessage);

                }
                if (response.Content.Contains("{ \"error\": \""))
                {
                    throw new Exception("Failed to add RMA Contact: " + response.Content);
                }
                return response.Content;
            }
            catch (Exception e)
            {
                throw new Exception("Adding failed with: " + e.StackTrace);
            }
            
        }
        public string CreateWithStatusCustomer(RunMyAccountsContact r)
        {
            return Task.Run(() => CreateCustomerWithStatusAsync(r)).Result;
        }

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

        public async Task<List<RunMyAccountsInvoiceExist>> ListAllInvoicesAsync2()
        {
            var request = new RestRequest("{tenant}/invoices", Method.Get);
            request.RequestFormat = DataFormat.Json;
            
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
        public async Task<List<RunMyAccountsInvoiceExist>> ListAllInvoicesAsync()
        {
            HttpClient cl = new HttpClient();
            cl.Timeout = new TimeSpan(0,0,30);
            cl.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
            var result = cl.GetAsync("https://service.runmyaccounts.com/api/latest/clients/basevision/invoices?api_key=OGOR1515dkkhuZRpfoOpK7cdoGzHWSv4");
            result.Wait();
            var contentstream = await result.Result.Content.ReadAsStringAsync();
            RunMyAccountsInvoiceExistList l = JsonConvert.DeserializeObject<RunMyAccountsInvoiceExistList>(contentstream);
            
            
            
            if (contentstream.Contains("{ \"error\": \""))
            {
                throw new Exception("Failed to get RMA Invoices: " + contentstream);
            }
            return l.invoice.ToList<RunMyAccountsInvoiceExist>();
        }
        public List<RunMyAccountsInvoiceExist> ListAllInvoices()
        {
            return Task.Run(() => ListAllInvoicesAsync()).Result;
        }


        public async Task<List<RunMyAccountsSaldo>> ListAllSaldoAsync(string accno, DateTime? from, DateTime? to)
        {
            var request = new RestRequest("{tenant}/gl/saldo", Method.Get);
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
            var response = await client.ExecuteAsync<RunMyAccountsSaldoList>(request);
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
        public List<RunMyAccountsSaldo> ListAllSaldo(string accno, DateTime? from, DateTime? to)
        {
            return Task.Run(() => ListAllSaldoAsync(accno, from, to)).Result;
        }

        public async Task<List<RunMyAccountsSaldoV2>> ListAllSaldoV2Async(string accno, DateTime? from, DateTime? to, String? department, String? project)
        {
            return await ListAllSaldoV2Async(accno, from, to, department, project, false);
        }
        public async Task<List<RunMyAccountsSaldoV2>> ListAllSaldoV2Async(string accno, DateTime? from, DateTime? to, String? department, String? project, Boolean exclude_yearend_bookings)
        {
            var request = new RestRequest("{tenant}/gl/v2/saldo", Method.Get);
            request.RequestFormat = DataFormat.Json;
            request.AddQueryParameter("accno", accno);
            if(department == null)
            {
                request.AddQueryParameter("department", department);
            }
            if (project == null)
            {
                request.AddQueryParameter("project", project);
            }
            request.AddQueryParameter("exclude_yearend_bookings", exclude_yearend_bookings);
            if (from != null)
            {
                request.AddQueryParameter("from", from.Value.ToString("yyyy-MM-dd"));
            }
            if (to != null)
            {
                request.AddQueryParameter("to", to.Value.ToString("yyyy-MM-dd"));
            }
            request.Timeout = 3000;
            var response = await client.ExecuteAsync<RunMyAccountsSaldoV2List>(request);
            if (response.ErrorException != null)
            {
                throw new Exception("Failed to get RMA Saldo: " + response.ErrorMessage);
            }
            if (response.Content.Contains("{ \"error\": \""))
            {
                throw new Exception("Failed to get RMA Saldo: " + response.Content);
            }
            return response.Data.row.ToList<RunMyAccountsSaldoV2>();
        }
        public List<RunMyAccountsSaldoV2> ListAllSaldoV2(string accno, DateTime? from, DateTime? to, String? department, String? project)
        {
            return Task.Run(() => ListAllSaldoV2Async(accno, from, to, department, project)).Result;
        }
        public List<RunMyAccountsSaldoV2> ListAllSaldoV2(string accno, DateTime? from, DateTime? to, String? department, String? project, Boolean exclude_yearend_bookings)
        {
            return Task.Run(() => ListAllSaldoV2Async(accno, from, to, department, project, exclude_yearend_bookings)).Result;
        }

        public async Task<List<RunMyAccountsProject>> ListAllProjectsAsync()
        {
            var request = new RestRequest("{tenant}/projects", Method.Get);
            request.RequestFormat = DataFormat.Json;
            var response = await client.ExecuteAsync<RunMyAccountsProjectList>(request);
            if (response.ErrorException != null)
            {
                throw new Exception("Failed to get RMA Projects: " + response.ErrorMessage);
            }
            if (response.Content.Contains("{ \"error\": \""))
            {
                throw new Exception("Failed to get RMA Projects: " + response.Content);
            }
            return response.Data.project.ToList<RunMyAccountsProject>();
        }
        public List<RunMyAccountsProject> ListAllProjects()
        {
            return Task.Run(() => ListAllProjectsAsync()).Result;
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

        public async Task<List<RunMyAccountsArticle>> ListAllArticlesAsync()
        {
            var request = new RestRequest("{tenant}/parts", Method.Get);
            request.RequestFormat = DataFormat.Json;
            var response = await client.ExecuteAsync<RunMyAccountsArticleList>(request);
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
        public List<RunMyAccountsArticle> ListAllArticles()
        {
            return Task.Run(() => ListAllArticlesAsync()).Result;
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
