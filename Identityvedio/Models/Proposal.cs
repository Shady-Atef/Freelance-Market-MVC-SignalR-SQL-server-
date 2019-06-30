using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Identityvedio.Models
{
    public class Proposal
    {

        public int ID { get; set; }
        public string proposal { get; set; }

        //public TimeSpan ProposalTime { get; set; }

        public DateTime proposaldate { get; set; }
        //[Required]
        public string Duration { get; set; }
        //[Required]
        public string FilePath { get; set; }

        [ForeignKey("Job")]
        public int JobId { get; set; }
        public virtual Job Job { get; set; }

        public int status { get; set; } = 0;

        [ForeignKey("ApplicationUser")]
        public string FreelancerId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}