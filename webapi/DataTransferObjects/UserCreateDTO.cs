using System.ComponentModel.DataAnnotations;
namespace webapi.DTOs
{

    ///A data transfer object used to create extension users with the json object that holds the data given by the end user.
    public class UserCreate
    {
        
        [Required]
        public string email { get; set; }

        [Required]
        public string name { get; set; }

    }
}