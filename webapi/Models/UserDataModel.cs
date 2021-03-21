using System.ComponentModel.DataAnnotations;
namespace webapi.Metadata
{

    ///A data model for the extension user data.
    ///
    ///This class holds properties with data annotations that will serve as a model for the creation of SQL data tables
    ///This particular model is used to create a DbSet on the APIContext class which will in turn serve to create and manage
    ///the UserData table on the SQL DB
    public class UserData
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

    }
}