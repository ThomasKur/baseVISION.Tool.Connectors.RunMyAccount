using baseVISION.Core.Connectors.RunMyAccount.Model;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;


namespace baseVISION.Core.Connectors.RunMyAccount
{

    /**
     * Invoices you need to pay to suppliers. 
     * 
     */
    public partial class RunMyAccountsClient
    {
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
            }
            catch (Exception e)
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


    }
}
