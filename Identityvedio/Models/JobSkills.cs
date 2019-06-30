using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Identityvedio.Models
{
    public class JobSkills
    {
        public int ID { get; set; }
        [ForeignKey("Job")]
        public int JobId { get; set; }
        public virtual Job Job { get; set; }
        [ForeignKey("Skills")]
        public int SkillId { get; set; }
        public virtual Skills Skills { get; set; }
    }
}