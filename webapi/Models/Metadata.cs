using System.ComponentModel.DataAnnotations;
namespace webapi.Metadata
{

    ///A data model for the songs Metadata.
    ///
    ///This class holds properties with data annotations that will serve as a model for the creation of SQL data tables
    ///This particular model is used to create a DbSet on the APIContext class which will in turn serve to create and manage
    ///the Songs table on the SQL DB
    public class SongData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Artist { get; set; }

        [Required]
        [MaxLength(500)]
        public string Album { get; set; }

        public string Lyrics{ get; set; }

        [Required]
        public string ImageURL{ get; set;}

        [Required]
        public string SpotifyID{ get; set;}



        
    }


}


    
