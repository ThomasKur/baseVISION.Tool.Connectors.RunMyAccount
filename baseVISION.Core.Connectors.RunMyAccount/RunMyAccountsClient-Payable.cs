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
     * 
     *  This class contains all methods regrding payable accounts. Invoices you need to pay to suppliers. 
     * 
     */
    public partial class RunMyAccountsClient
    {
        public async Task<List<RunMyAccountsPayable>> ListAllPayablesAsync(DateTime? from, DateTime? to)
        {
            var request = new RestRequest("{tenant}/payables", Method.Get);
            request.RequestFormat = DataFormat.Json;
            request.Timeout = 300000;

            if (from != null)
            {
                request.AddQueryParameter("from", from.Value.ToString("yyyy-MM-dd"));
            }
            if (to != null)
            {
                request.AddQueryParameter("to", to.Value.ToString("yyyy-MM-dd"));
            }

            var response = await client.ExecuteAsync<RunMyAccountsPayableList>(request);

            if (response.ErrorException != null)
            {
                throw new Exception("Failed to get RMA Payables: " + response.ErrorMessage);
            }
            if (response.Content.Contains("{ \"error\": \""))
            {
                throw new Exception("Failed to get RMA Paables: " + response.Content);
            }
            return response.Data.payable.ToList<RunMyAccountsPayable>();
        }
        public List<RunMyAccountsPayable> ListAllPayables(DateTime? from, DateTime? to)
        {
            return Task.Run(() => ListAllPayablesAsync(from, to)).Result;
        }
    }
}
