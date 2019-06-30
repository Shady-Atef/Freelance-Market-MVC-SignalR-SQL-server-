using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Identityvedio.Models
{
    public class JobExperienceLevel
    {
        public int ID { get; set; }
        public string ExperienceLevel { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}