using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace cbapp.Models
{
public class Project
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int project_id { get; set; }

    [Required]
    [Display(Name = "Artist/s")]
    public string artist { get; set; } = String.Empty;

    [Required]
    [MaxLength(65)]
    [Display(Name = "Full Title")]
    public string release_title { get; set; }

    [Required]
    [Display(Name = "Release Date")]
    public DateTime release_date { get; set; }

    public ICollection<ProjectRatings> ProjectRatings { get; set; } = new HashSet<ProjectRatings>();
    public ICollection<Songs> Songs { get; set; } = new HashSet<Songs>();

    [Required]
    [Display(Name = "Type")]
    public string type { get; set; } = "Single";
  }
}