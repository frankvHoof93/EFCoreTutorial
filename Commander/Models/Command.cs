using System.ComponentModel.DataAnnotations;

namespace Commander.Models 
{
    public class Command
    {
        // Set to PK due to name
        [Key] // Not required due to name
        public int Id { get; set; }
        
        // Not Nullable
        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }
        [Required]
        public string Line { get; set; }
        [Required]
        public string Platform { get; set; }
    }
}