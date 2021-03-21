using System.ComponentModel.DataAnnotations;

namespace webapi.DTOs
{
    ///A data transfer object used to create Songs with the json object that holds the metadata given by the user/service.
    public class SongCreate
    {


        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Artist { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Album { get; set; }

        [Required]
        [MaxLength(500)]
        public string Lyrics{ get; set; }

        [Required]
        public string ImageURL { get; set; }

        [Required]
        public string SpotifyID { get; set; }

        
    }
}