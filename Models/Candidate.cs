using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeManager.Models
{
    public class Candidate
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [RegularExpression(@"^\d{10}$", ErrorMessage = "The mobile phone must have at least 10 numbers")]
        public string? Mobile { get; set; }

        public int? DegreeId { get; set; }

        [ForeignKey("DegreeId")]
        public Degree? Degree { get; set; }

        public string? CvFilePath { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
