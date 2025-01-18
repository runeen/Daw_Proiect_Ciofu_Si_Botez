using Microsoft.AspNetCore.Identity;

namespace cbapp.Models
{
    public class CustomUsers : IdentityUser
    {
        public ICollection<SongRatings> SongRatings { get; set; }

        public CustomUsers()
        {
            SongRatings = new HashSet<SongRatings>();
        }
    }
}