using System;
using System.Collections.Generic;
using System.Text;

namespace TestBussiness.ServiceMessage
{
    public class AccountBalanceInfoMessage
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
