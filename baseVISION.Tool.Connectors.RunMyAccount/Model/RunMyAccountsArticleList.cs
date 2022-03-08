using System;
using System.Collections.Generic;
using System.Text;

namespace baseVISION.Tool.Connectors.RunMyAccount.Model
{
    public class RunMyAccountsArticleList
    {
        public RunMyAccountsArticleList()
        {
            part = new List<RunMyAccountsArticle>();
        }
        public List<RunMyAccountsArticle> part { get; set; }
    }
}
