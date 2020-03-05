using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using System.Web.Mvc;

namespace Jop_Offers_Website.Models
{
    public class Job
    {


        //1
        public int Id { get; set; }

        //2
        [Required]
        [Display(Name ="اسم الوظيفه")]
        public string JobName { get; set; }

        //3
        [AllowHtml]
        [Display(Name = "وصف الوظيفه")]
        public string JobContent { get; set; }

        //4
        [Display(Name = "صورة الوظيفه")]
        public string jobImage { get; set; }


        // 5
        [Display(Name = "نوع الوظيفه")]
        public int  CategoryID { get; set; } // forgien Key

        public string UserID { get; set; }


        public virtual Category category { get; set; } // object form class category represent relation 1

        public virtual ApplicationUser user { get; set; }
    }
}