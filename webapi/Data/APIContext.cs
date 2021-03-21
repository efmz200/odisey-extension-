using Microsoft.EntityFrameworkCore;
using webapi.Metadata;

namespace webapi.Data
{
    ///The database context for the API.
    ///
    ///This class is an implementation of class Microsoft.EntityFrameworkCore.DbContext
    ///A DbContext instance represents a session with the database and can be used to query and save
    /// instances of your entities. DbContext is a combination of the Unit Of Work and Repository patterns.  
    public class APIContext : DbContext
    {

        public APIContext(DbContextOptions<APIContext> opt) : base(opt)
        {

        }


        ///class Microsoft.EntityFrameworkCore.DbContext
        ///A DbContext instance represents a session with the database and can be used to query and save instances of your entities,
        /// DbContext is a combination of the Unit Of Work and Repository patterns.
        public DbSet<SongData> Songs { get; set; }

        ///class Microsoft.EntityFrameworkCore.DbContext
        ///A DbContext instance represents a session with the database and can be used to query and save instances of your entities,
        /// DbContext is a combination of the Unit Of Work and Repository patterns.
        public DbSet<UserData> UserTable { get; set; }

    }
}