using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace cbapp.Models
{
    public class ProjectRatings   
    {
        
        [ForeignKey("Project")]
    public int projectId { get; set; }
    public Project Project { get; set; }

    
    [ForeignKey("User")]
    public string UserId { get; set; }
    public CustomUsers User { get; set; }
        
        public DateTime rating_date{get;set;}=DateTime.UtcNow;

        [Required]
        [Column(TypeName = "decimal(4, 1)")]
        [Range(1, 10, ErrorMessage = "Valoarea ratingului este maxim 10!.")]
        public decimal score {get;set; }

        

      


    }
}