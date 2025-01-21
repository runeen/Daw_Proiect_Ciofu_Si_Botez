using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
namespace cbapp.Models
{
    public class Songs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int song_id { get; set; }


        [MaxLength(65)]
        public string title { get; set; }


        public string? length { get; set; }

        [ForeignKey("Project")]
        public int? project_id { get; set; }


        public Project? Project { get; set; }


        public int tracklist_number { get; set; }







    }








}