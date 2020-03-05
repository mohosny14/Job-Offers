using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Jop_Offers_Website.Models
{
    public class ApplyForJob
    {
        // "Many to Many "جدول ناتج عن علاقة
        //بين جدولين
        //jobs and Users
        public int Id { get; set; }

        [Display(Name = "الرسالة")]
        public string Message { get; set; }
        [Display(Name = "البيانات المرسلة")]
        public DateTime ApplyDate { get; set; }

        public int JobId { get; set; }
        public string UserId { get; set; }

        // job table to connect with this table
        public virtual Job objJob { get; set; }

        // Users table to connect with this table
        public virtual ApplicationUser user { get; set; }
    }
}