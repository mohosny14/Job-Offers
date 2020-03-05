using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jop_Offers_Website.Models
{
    public class ContactModel
    {
        [Required]
        [Display(Name = "الإسم")]
        public string name { get; set; }

        [Required]
        [Display(Name = "البريد")]
        public string E_mail { get; set; }

        [Required]
        [Display(Name = " موضوع الرسالة")]
        public string Subject { get; set; } // عنوان الرسالة

        [Required]
        [Display(Name = " نص الرسالة")]
        public string message { get; set; }
    }
}