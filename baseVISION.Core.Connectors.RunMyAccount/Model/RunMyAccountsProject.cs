using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace baseVISION.Core.Connectors.RunMyAccount.Model
{
    public class RunMyAccountsProject
    {
        [JsonProperty("@id")]
        public string id { get; set; }
        [JsonProperty("@number")]
        public string number { get; set; }
        [JsonProperty("@description")]
        public string description { get; set; }
        public override string ToString()
        {
            return number;
        }

    }
}
