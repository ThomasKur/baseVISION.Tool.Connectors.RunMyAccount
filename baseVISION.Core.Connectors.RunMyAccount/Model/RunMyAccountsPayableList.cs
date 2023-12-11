using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace baseVISION.Core.Connectors.RunMyAccount.Model
{
    public class RunMyAccountsPayableList
    {
        public RunMyAccountsPayableList()
        {
            payable = new List<RunMyAccountsPayable>();
        }
        [JsonConverter(typeof(SingleOrArrayConverter<RunMyAccountsPayable>))]
        public List<RunMyAccountsPayable> payable { get; set; }
    }
}
