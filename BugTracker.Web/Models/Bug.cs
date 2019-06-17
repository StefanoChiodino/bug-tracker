using System;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Web.Models
{
    public class Bug
    {
        // TODO: Use GUID for this and all other IDs.
        [Key]
        public int BugId { get; set; }
        
        [Required]
        public DateTime Created { get; set; }
        
        [Required]
        [MinLength(5)]
        public string Title { get; set; }
        
        // TODO: may need to become IHtmlString but don't have time to plug in TinyMCE or similar yet.
        public string Description { get; set; }
        
        public virtual Person Assignee { get; set; }
        
        // TODO: this will probably need to become an Enum or link to a status that can be edited on the same app.
        public bool IsClosed { get; set; }
        
        // TODO: record who reported the bug.
        // public virtual IdentityUser Reporter { get; set; }
    }
}
