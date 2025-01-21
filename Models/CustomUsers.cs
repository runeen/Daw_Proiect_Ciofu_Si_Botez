using Microsoft.AspNetCore.Identity;

namespace cbapp.Models
{
    public class CustomUsers : IdentityUser
    {
        public ICollection<ProjectRatings> ProjectRatings { get; set; }

        

        public CustomUsers()
        {
            ProjectRatings = new HashSet<ProjectRatings>();
        }
    }
}