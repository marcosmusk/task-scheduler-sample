using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerUWP.Models
{
    public class Task
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [StringLength(800)]
        public string Description { get; set; }
        public byte Priority { get; set; }
        public bool Important { get; set; }
    }
}
