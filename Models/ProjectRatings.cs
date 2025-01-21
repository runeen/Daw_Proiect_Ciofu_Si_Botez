using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace cbapp.Models
{
    [PrimaryKey(nameof(projectId), nameof(UserId))]
    public class ProjectRatings   
    {

        [Display(Name = "ID proiect")]
         public int projectId { get; set; }

        [Display(Name = "ID utilizator")]  
        public string UserId { get; set; }
        [Required]

        [Display(Name = "Data")]
        public DateTime rating_date{get;set;}=DateTime.UtcNow;

        [Display(Name = "Scor")]
        [Required]
        [Column(TypeName = "decimal(4, 1)")]
        [Range(1, 10, ErrorMessage = "Valoarea ratingului este maxim 10!.")]
        public decimal score {get;set; }

        public Project Project { get; set; } 
        public CustomUsers User { get; set; }

      


    }
}