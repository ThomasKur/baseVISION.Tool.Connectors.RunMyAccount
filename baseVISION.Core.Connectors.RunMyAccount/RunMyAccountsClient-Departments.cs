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
        public async Task<List<RunMyAccountsDepartment>> ListAllDepartmentsAsync()
        {
            var request = new RestRequest("{tenant}/departments", Method.Get);
            request.RequestFormat = DataFormat.Json;

            var response = await client.ExecuteAsync<RunMyAccountsDepartmentList>(request);

            if (response.ErrorException != null)
            {
                throw new Exception("Failed to get RMA Departments: " + response.ErrorMessage);
            }
            if (response.Content.Contains("{ \"error\": \""))
            {
                throw new Exception("Failed to get RMA Departments: " + response.Content);
            }
            return response.Data.department.ToList<RunMyAccountsDepartment>();
        }
        public List<RunMyAccountsDepartment> ListAllDepartments()
        {
            return Task.Run(() => ListAllDepartmentsAsync()).Result;
        }
    }
}
