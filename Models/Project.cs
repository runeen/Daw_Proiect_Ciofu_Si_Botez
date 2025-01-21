using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace cbapp.Models
{
    public class Project
    {
        [Key]
        public int project_id{get;set;}

        [Required]
        public string artist{get;set;}=String.Empty;
        
        [Required]
        [MaxLength(65)]
        public string release_title{get;set;}

        [Required]
        public DateTime release_date {get;set; }

        
         public ICollection<Songs> Songs { get; set; }
         public ICollection<ProjectRatings> ProjectRatings { get; set; }

        [Required]
        public string type{ get; set; }="Single";
        
        public Project(){
            Songs=new HashSet<Songs>();
            ProjectRatings = new HashSet<ProjectRatings>();

        }




    }
}
