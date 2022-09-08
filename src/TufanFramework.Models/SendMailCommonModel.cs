using System;
using System.Collections.Generic;

namespace TufanFramework.Models
{
    public class SendMailCommonModel
    {
        public Guid TransactionId { get; set; }
        public List<string> To { get; set; }
        public string Message { get; set; }
    }
}