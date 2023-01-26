using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace appGestion_conge.Models
{
    public class Contactmodel
    {
        public string phone { get; set; }
        [Required]
        
        public string name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string message { get; set; }
    }
}