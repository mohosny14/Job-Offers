using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jop_Offers_Website.Models
{
    public class Category
    {
        //1
        public int Id { get; set; }
        
        // 2
        [Required]
        [Display(Name ="نوع الوظيفة")]
        public string CategoryName { get; set; }
        // 3
        [Display(Name = "وصف الوظيفة")]
        [Required]
        public string CatehoryDescription { get; set; }

        public virtual ICollection<Job> jobs { get; set; } // object from class job: represent many relationship
        // one job to many category
    }
}