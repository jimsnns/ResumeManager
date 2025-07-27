using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeManager.Models
{
    public class Degree
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
