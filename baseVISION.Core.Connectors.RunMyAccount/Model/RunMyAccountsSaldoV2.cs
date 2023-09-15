using System;
using System.Collections.Generic;
using System.Text;

namespace baseVISION.Core.Connectors.RunMyAccount.Model
{
    public class RunMyAccountsSaldoV2
    {
        
        public string accountnumber { get; set; }
        public string accountname { get; set; }
        public string amount { get; set; }
        
        
        public Double GetAmount() { 
            return Double.Parse(amount); 
        } 
    }
}
