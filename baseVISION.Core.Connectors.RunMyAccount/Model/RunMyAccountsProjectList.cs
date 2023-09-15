using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace baseVISION.Core.Connectors.RunMyAccount.Model
{
    public class RunMyAccountsProjectList
    {
        public RunMyAccountsProjectList()
        {
            project = new List<RunMyAccountsProject>();
        }
        [JsonConverter(typeof(SingleOrArrayConverter<RunMyAccountsProject>))]
        public List<RunMyAccountsProject> project { get; set; }
    }
}
