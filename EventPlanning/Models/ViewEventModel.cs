using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventPlanning.Annotations;
using System.Linq;
using System.Web;

namespace EventPlanning.Models
{
    public class ViewEventModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        [StringLength(20)]
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Fields]
        [Display(Name = "Names")]
        public string[] Names { get; set; }

        [Fields]
        [Display(Name = "Values")]
        public string[] Values { get; set; }
    }
}