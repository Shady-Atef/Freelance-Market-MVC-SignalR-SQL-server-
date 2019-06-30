using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Identityvedio.Models
{
    public class Client
    {
    
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "ID")]
        public string SSN { get; set; }
        [Display(Name = "Profesion")]
        public string Profesion { get; set; }

        [Required]
        [Display(Name = "personal Description")]
        public string Description { get; set; }
        public string ImagePath { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ClientId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


    }
}