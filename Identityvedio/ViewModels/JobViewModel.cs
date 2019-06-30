using Identityvedio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Identityvedio.ViewModels
{
    public class JobViewModel
    {
        public int JobID { get; set; }
        public string JobTitle { get; set; }
        public int CatID { get; set; }
        public string Desc { get; set; }
        public string IMG { get; set; }
        public int PaymentTypeId { get; set; }
        public int ExperienceLevelId { get; set; }
        public DateTime JobDuration { get; set; }

        public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();
        public List<JobExperienceLevel> JobExperienceLevels { get; set; } = new List<JobExperienceLevel>();
        public List<JobPaymentType> JobPaymentTypes { get; set; } = new List<JobPaymentType>();
        public List<Skills> Skills { get; set; } = new List<Skills>();
    }
}