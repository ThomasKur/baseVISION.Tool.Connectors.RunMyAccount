using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace baseVISION.Core.Connectors.RunMyAccount.Model
{
    public class RunMyAccountsInvoice
    {
        public string invnumber { get; set; }
        public string ordnumber { get; set; }
        public InvoiceStatus status { get; set; }
        public string currency { get; set; }
        public int ar_accno { get; set; }
        private DateTime _transdate;
        public DateTime transdate
        {
            get
            {
                if (_transdate.Hour > 20)
                {
                    return _transdate.AddDays(1).Date;
                }
                else
                {
                    return _transdate.Date;
                }
            }
            set { _transdate = value; }
        }
        private DateTime _duedate;
        public DateTime duedate
        {
            get
            {
                if (_duedate.Hour > 20)
                {
                    return _duedate.AddDays(1).Date;
                }
                else
                {
                    return _duedate.Date;
                }
            }
            set { _duedate = value; }
        }
        public string description { get; set; }
        public string notes { get; set; }
        public string intnotes { get; set; }
        public bool taxincluded { get; set; }
        public string dcn { get; set; }
        public string customernumber { get; set; }
        public string department { get; set; }
        public RunMyAccountsArticleList parts { get; set; }
    }
}
