using System;
using System.ComponentModel.DataAnnotations;

namespace TaskScheduler.Models
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
