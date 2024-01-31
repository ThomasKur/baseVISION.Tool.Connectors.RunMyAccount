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
            return await ListAllSaldoV2Async(accno, from, to, department, project, true);
        }
        public async Task<List<RunMyAccountsSaldoV2>> ListAllSaldoV2Async(string accno, DateTime? from, DateTime? to, String? department, String? project, Boolean exclude_yearend_bookings)
        {
            var request = new RestRequest("{tenant}/gl/v2/saldo", Method.Get);
            request.RequestFormat = DataFormat.Json;
            request.AddQueryParameter("accno", accno);
            if (department != null)
            {
                request.AddQueryParameter("department", department);
            }
            if (project != null)
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

    }
}
