namespace webapi.DTOs
{

    ///A data transfer object used to read the relevant song metadata requested by the user.
    public class SongRead
    {


        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Artist { get; set; }

        public string Album { get; set; }

        public string ImageURL { get; set; }

        public string SpotifyID { get; set; }

    }


}