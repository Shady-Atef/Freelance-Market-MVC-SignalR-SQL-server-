using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Identityvedio.Models
{
    public class Contract
    {
        public int ID { get; set; }
        [ForeignKey("Proposal")]
        public int ProposalId { get; set; }
        public virtual Proposal Proposal { get; set; }
        [ForeignKey("Job")]
        public int JobId { get; set; }
        public virtual Job Job { get; set; }
        [ForeignKey("ApplicationFreelancer")]
        public string FreelanceId { get; set; }
        public virtual ApplicationUser ApplicationFreelancer { get; set; }
        [ForeignKey("ApplicationClient")]
        public string ClientId { get; set; }
        public virtual ApplicationUser ApplicationClient { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? FinalPrice { get; set; }
        public int? Hours { get; set; }


    }
}