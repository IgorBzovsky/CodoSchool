using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace CodoSchool.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<StudentProgress> StudentProgress { get; set; }
        public ApplicationUser()
        {
            StudentProgress = new List<StudentProgress>();
        }
    }
}
