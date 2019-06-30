using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Identityvedio.Models
{
    public class Message
    {
        public int ID { get; set; }
        [ForeignKey("FreelanceUser")]
        public string FreelanceId { get; set; }
        public virtual ApplicationUser FreelanceUser { get; set; }
        [ForeignKey("ManagerUser")]
        public string ManagerId { get; set; }
        public virtual ApplicationUser ManagerUser { get; set; }
        public string MessageText { get; set; }
        public DateTime MessageTime { get; set; }
        [ForeignKey("Proposal")]
        public int ProposalId { get; set; }
        public virtual Proposal Proposal { get; set; }
        public string Sender { get; set; }
                                           // public virtual ICollection<Attachment> Attachments { get; set; }
    }
}