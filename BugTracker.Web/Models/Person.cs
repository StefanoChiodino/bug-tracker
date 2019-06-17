using System.ComponentModel.DataAnnotations;

namespace BugTracker.Web.Models
{
    // TODO: consider if could be part of Identity framework.
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