using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Identityvedio.Models
{
    //enum Paymenttype
    //{
    //    Fixed,
    //    PerHour
    //}

    //enum ExperienceLevel
    //{
    //    Entry,
    //    Intermediate,
    //    Epert
    //}
    public class Job
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Job Category")]
        [ForeignKey("JobCategory")]
        public int CatID { get; set; }
        public virtual JobCategory JobCategory { get; set; }
        [Required]

        [Display(Name = "Job Description")]
        public string Desc { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ClientId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Payment Type")]
        public int? Price { get; set; }
        public DateTime PostTime { get; set; } = DateTime.Now;
        public DateTime? EndTime { get; set; } = null;

        public bool Ended { get; set; }

        [ForeignKey("JobExperienceLevel")]

        [Display(Name = "Job Experience Level")]
        public int ExperienceLevelId { get; set; }
        public virtual JobExperienceLevel JobExperienceLevel { get; set; }
        
        public virtual ICollection<JobSkills> JobSkills { get; set; }



    }
}