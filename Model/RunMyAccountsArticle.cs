using System;
using System.Collections.Generic;
using System.Text;

namespace baseVISION.Tool.Connectors.RunMyAccount.Model
{
    public class RunMyAccountsArticle
    {
        public string income_accno { get; set; }
        public string expense_accno { get; set; }
        public string partnumber { get; set; }
        public string description { get; set; }
        public string unit { get; set; }
        public Double quantity { get; set; }
        public Double sellprice { get; set; }
        public Double discount { get; set; }
        public string tax_accnos { get; set; }
        public string itemnote { get; set; }
        public DateTime price_update { get; set; }
        public override string ToString()
        {
            return partnumber;
        }
        public RunMyAccountsArticle Clone()
        {
            return this.MemberwiseClone() as RunMyAccountsArticle;
        }
    }
}
