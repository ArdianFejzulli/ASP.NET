using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagment.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }
        [Required]
        public int Duration { get; set; }

    }
}
