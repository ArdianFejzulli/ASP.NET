using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagment.Models
{
    public class Batch
    {
        public int id { get; set; }
        [Required]
        [StringLength(100)]
        public string BatchName { get; set; }
        [Required]
        public DateTime Year { get; set; }
    }
}
