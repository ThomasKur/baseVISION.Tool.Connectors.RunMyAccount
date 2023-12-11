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
     *  This class contains all methods regrding Projects. 
     * 
     */
    public partial class RunMyAccountsClient
    {
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

    }
}
