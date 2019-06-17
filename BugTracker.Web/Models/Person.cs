using System.ComponentModel.DataAnnotations;

namespace BugTracker.Web.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Surname { get; set; }
    }
}