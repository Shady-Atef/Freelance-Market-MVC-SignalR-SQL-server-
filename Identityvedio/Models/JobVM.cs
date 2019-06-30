using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Identityvedio.Models
{
    public class JobVM
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Job Category")]
        //[ForeignKey("JobCategory")]
        public int CatID { get; set; }
        public virtual JobCategory JobCategory { get; set; }
        [Required]

        [Display(Name = "Job Description")]
        public string Desc { get; set; }

        //[ForeignKey("ApplicationUser")]
        public string ClientId { get; set; }
        //public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Payment Type")]
        public int? Price { get; set; }

        //[ForeignKey("JobExperienceLevel")]

        [Display(Name = "Job Experience Level")]
        public int ExperienceLevelId { get; set; }

        public List<SkillModel> SkillList { get; set; }

    }
}