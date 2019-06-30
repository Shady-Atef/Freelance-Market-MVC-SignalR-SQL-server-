using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Identityvedio.Models
{
    public class JobCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Skills> Skills { get; set; }

    }
}