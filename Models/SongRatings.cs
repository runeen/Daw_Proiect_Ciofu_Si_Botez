using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace cbapp.Models
{
    public class SongRatings   
    {
        
         public int SongId { get; set; } 
        public string UserId { get; set; }
        [Required]
        
        public DateTime rating_date{get;set;}=DateTime.UtcNow;

        [Required]
        [Column(TypeName = "decimal(4, 1)")]
        [Range(1, 10, ErrorMessage = "Valoarea ratingului este maxim 10!.")]
        public decimal score {get;set; }

        public Songs Song { get; set; } 
        public CustomUsers User { get; set; }

      


    }
}