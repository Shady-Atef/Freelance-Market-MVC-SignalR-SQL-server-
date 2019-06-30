using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identityvedio.Models
{
    public class Skills
    {
        public int ID { get; set; }
        public string SkillsName { get; set; }
        [ForeignKey("JobCategory")]
        public int CatId { get; set; }
        public virtual JobCategory JobCategory { get; set; }
        public virtual ICollection<JobSkills> JobSkills { get; set; }

    }
}