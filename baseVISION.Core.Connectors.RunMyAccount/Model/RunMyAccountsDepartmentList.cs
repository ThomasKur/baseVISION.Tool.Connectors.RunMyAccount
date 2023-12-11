using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace baseVISION.Core.Connectors.RunMyAccount.Model
{
    public class RunMyAccountsDepartmentList
    {
        public RunMyAccountsDepartmentList()
        {
            department = new List<RunMyAccountsDepartment>();
        }
        [JsonConverter(typeof(SingleOrArrayConverter<RunMyAccountsDepartment>))]
        public List<RunMyAccountsDepartment> department { get; set; }
    }
}
