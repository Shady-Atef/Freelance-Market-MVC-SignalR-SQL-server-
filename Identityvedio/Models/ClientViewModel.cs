using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Identityvedio.Models
{
    public class ClientViewModel
    {
        public int ID { get; set; }

        public string Description { get; set; }
        public HttpPostedFileBase ImagePath { get; set; }
        public string FullName { get; set; }
        public string SSN { get; set; }
        public string Profesion { get; set; }
        public string ClientId { get; set; }

    }
}