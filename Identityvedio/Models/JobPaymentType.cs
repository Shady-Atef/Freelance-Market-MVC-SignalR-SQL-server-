using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Identityvedio.Models
{
    public class JobPaymentType
    {
        public int ID { get; set; }
        public string PaymentType { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}