using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace baseVISION.Tool.Connectors.RunMyAccount.Model
{
    public class RunMyAccountsArticleList
    {
        public RunMyAccountsArticleList()
        {
            part = new List<RunMyAccountsArticle>();
        }
        [JsonConverter(typeof(SingleOrArrayConverter<RunMyAccountsArticle>))]
        public List<RunMyAccountsArticle> part { get; set; }
    }
}
