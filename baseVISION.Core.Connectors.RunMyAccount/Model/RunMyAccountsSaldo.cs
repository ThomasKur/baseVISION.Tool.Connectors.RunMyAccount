using System;
using System.Collections.Generic;
using System.Text;

namespace baseVISION.Core.Connectors.RunMyAccount.Model
{
    public class RunMyAccountsSaldo
    {
        public List<string> column { get; set; }

        public string accno { get { return this.column[0]; } }
        public string name { get { return this.column[1]; } }
        public Double saldo { get { return Double.Parse(this.column[2]); } }
    }
}
