using System;
using System.Collections.Generic;
using System.Text;

namespace baseVISION.Core.Connectors.RunMyAccount.Model
{
    public class RunMyAccountsVendor
    {
        public string id { get; set; }
        public string vendornumber { get; set; }
        public string name { get; set; }
        public DateTime created { get; set; }
        public string salutation { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string terms { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string zipcode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string taxnumber { get; set; }
        public string arap_accno { get; set; }
        public string payment_accno { get; set; }
        public string bank_name { get; set; }
        public string bank_address1 { get; set; }
        public string bank_address2 { get; set; }
        public string bank_zipcode { get; set; }
        public string bank_city { get; set; }
        public string bank_country { get; set; }
        public string bank_iban { get; set; }
        public string bank_bic { get; set; }
        public override string ToString()
        {
            return name;
        }
        public RunMyAccountsVendor Clone()
        {
            return this.MemberwiseClone() as RunMyAccountsVendor;
        }
    }
}
