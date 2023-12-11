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
    }
}
