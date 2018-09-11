using System;
using System.Collections.Generic;
using System.Text;

namespace baseVISION.Tool.Connectors.RunMyAccount.Model
{
    public class RunMyAccountsContact {
        public RunMyAccountsContact() { }
        public string id { get; set; }
        public string customernumber { get; set; }
        public string name { get; set; }
        public DateTime created { get; set; }
        public string contactId { get; set; }
        public string salutation { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string addressId { get; set; }
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
        public string cc { get; set; }
        public string bcc { get; set; }
        public string language_code { get; set; }
        public string remittancevoucher { get; set; }
        public string notes { get; set; }
        public string taxnumber { get; set; }
        public string taxfree { get; set; }
        public Chart chart { get; set; }
        public PaymentChart paymentChart { get; set; }
        public string terms { get; set; }
        public string tax_accnos { get; set; }
        public string bank_iban { get; set; }
        public string bank_bic { get; set; }
        public string typeofcontact { get; set; }
        public string gender { get; set; }
        public string arap_accno { get; set; }
        public string payment_accno { get; set; }
    }
    public class Chart
    {
        public string @id { get; set; }
        public string @accno { get; set; }
        public string @link { get; set; }
        public string @allow_gl { get; set; }
        public string @is_active { get; set; }
    }

    public class PaymentChart
    {
        public string @id { get; set; }
        public string @accno { get; set; }
        public string @link { get; set; }
        public string @allow_gl { get; set; }
        public string @is_active { get; set; }
    }
}
