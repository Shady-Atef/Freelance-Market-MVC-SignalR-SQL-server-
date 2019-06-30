using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Identityvedio.Models
{
    public class ProposalViewModel
    {
        public string proposal { get; set; }

        public DateTime proposaldate { get; set; }
        public string Duration { get; set; }
        public HttpPostedFileBase FilePath { get; set; }

        public int JobId { get; set; }

        public string FreelancerId { get; set; }

    }
}