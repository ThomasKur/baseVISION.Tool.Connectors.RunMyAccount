using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace baseVISION.Core.Connectors.RunMyAccount.Model
{
    public class RunMyAccountsPayable
    {
        public string invnumber { get; set; }
        public string ordnumber { get; set; }
        public string status { get; set; }
        public string currency { get; set; }
        public string ap_accno { get; set; }
        public DateTime transdate { get; set; }
        public DateTime duedate { get; set; }
        public string description { get; set; }
        public string notes { get; set; }
        public string intnotes { get; set; }
        public string taxincluded { get; set; }
        public string dcn { get; set; }
        public Double amount { get; set; }
        public Double netamount { get; set; }
        public Double paid { get; set; }
        public string paymentAccno { get; set; }
        public RunMyAccountsVendor vendor { get; set; }
        public RunMyAccountsArticleList parts { get; set; }
        public override string ToString()
        {
            return invnumber;
        }
        public RunMyAccountsPayable Clone()
        {
            return this.MemberwiseClone() as RunMyAccountsPayable;
        }
    }
}
