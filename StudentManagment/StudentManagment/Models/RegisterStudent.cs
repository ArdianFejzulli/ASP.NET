using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagment.Models
{
    public class RegisterStudent
    {
        public int id { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        public string Nic { get; set; }

        [Required]
        public int Courseid { get; set; }
        public Course Course { get; set; }

        [Required]
        public int Batchid { get; set; }
        public Batch Batch { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
