using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace baseVISION.Core.Connectors.RunMyAccount.Model
{
    public class RunMyAccountsDepartment
    {
        [JsonProperty("@id")]
        public string id { get; set; }
        [JsonProperty("@name")]
        public string name { get; set; }
        public override string ToString()
        {
            return name + "-" + id;
        }

    }
}
