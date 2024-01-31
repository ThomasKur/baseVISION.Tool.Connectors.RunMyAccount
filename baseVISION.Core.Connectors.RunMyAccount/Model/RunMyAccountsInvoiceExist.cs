using System;
using System.Collections.Generic;
using System.Text;

namespace baseVISION.Core.Connectors.RunMyAccount.Model
{
    public class RunMyAccountsInvoiceExist : RunMyAccountsInvoice
    {
        public Double amount { get; set; }
        public Double netamount { get; set; }
        public RunMyAccountsContact customer { get; set; }

    }
}
