using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTracker.Web.Models
{
    public class Bug
    {
        // TODO: Use GUID/int dual ID for this and all other models.
        [Key]
        public int BugId { get; set; }
        
        // TODO: store user TimeZone and apply to this.
        [Required]
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
        
        [Required]
        [MinLength(5)]
        public string Title { get; set; }
        
        // TODO: may need to become IHtmlString but don't have time to plug in TinyMCE or similar yet.
        public string Description { get; set; }
        
        // TODO: this would probably be an identity user, but maybe needs the flexibility to be anything you want,
        // including just text.
        public virtual Person Assignee { get; set; }
        
        // TODO: this will probably need to become an Enum or link to a status that can be edited on the same app.
        [DisplayName("Closed")]
        public bool IsClosed { get; set; }
        
        // TODO: record who reported the bug.
        // public virtual IdentityUser Reporter { get; set; }
    }

    public class BugCreateViewModel : Bug
    {
        public IList<SelectListItem> PeopleSelectListItems { get; set; }
    }
}
