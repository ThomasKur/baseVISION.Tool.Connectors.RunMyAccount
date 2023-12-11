using baseVISION.Core.Connectors.RunMyAccount.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace baseVISION.Core.Connectors.RunMyAccount
{
    public partial class RunMyAccountsClient
    {
        private string ApiKey;
        private RestClient client;

        public RunMyAccountsClient(string tenant, string apikey)
        {
            // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;

 
            Initialize(tenant, apikey, "https://service.runmyaccounts.com/api/latest/clients/");

        }
        public RunMyAccountsClient(string tenant, string apikey, string url)
        {
            // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Initialize(tenant, apikey, url);

        }

        private void Initialize(string tenant, string apikey, string url)
        {
            ApiKey = apikey;

            RestClientOptions option = new RestClientOptions(url);
            option.Encoding = Encoding.UTF8;
            option.MaxTimeout = 300000;

            client = new RestClient(option, configureSerialization: s => s.UseSerializer(() => new NewtonsoftJsonSerializer()));
            client.AddDefaultHeader("ContentType", "application/json");
            client.AddDefaultParameter("api_key", apikey, ParameterType.QueryString);
            client.AddDefaultParameter("tenant", tenant, ParameterType.UrlSegment);
        }




    }
}
