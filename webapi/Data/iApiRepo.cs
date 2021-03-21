using System.Collections.Generic;
using webapi.Metadata;

namespace webapi.Data
{

    ///The method signatures that are provided to the consumer.
    ///
    ///It's the basic contract given to the end user, This interface provides all the needed
    ///actions and logic to be executed server sideto be able to meet the end user requirements
    ///This interface needs to be implemented in order to control all the serverside logic
    public interface IWebApi
    {
        ///Saves the changes that were performed on the SQL DB.
        bool SaveChanges();

        ///Retrieves all the songs on the Songs table in the SQL DB.
        ///
        ///returns a json array with all the metadata of the existing songs
        IEnumerable<SongData> GetAllSongs();

        ///Returns all the songs on the Songs table in the SQL DB that match a given criteria.
        ///
        ///@param criteria is a string value that holds the search criteria given by the user
        IEnumerable<SongData> GetSongByCriteria (string criteria);

        ///Creates a song on the Songs table in the SQL DB.
        ///
        ///@param song is a SongData object created with the metadata values provided by the user/service
        void CreateSong(SongData song);

        ///Deletes a song on the Songs table in the SQL DB.
        ///
        ///@param song is a SongData object created with the metadata values provided by the user/service
        void DeleteSong(SongData song);

        ///Retrieves a song on the Songs table in the SQL DB.
        ///
        ///Returns a SongData object created with the data in the SQL DB of a specific song that matched the given int id
        ///@param id is an integer value created given by the user
        SongData GetSongById(int id);

        ///Retrieves an user on the UserData table in the SQL DB.
        ///
        ///Returns a UserData object created with the data in the SQL DB of a specific user that matched the given int id
        ///@param id is an integer value created given by the user
        UserData GetUserById(string id);

        ///Retrieves a song on the Songs table in the SQL DB.
        ///
        ///@param id is an integer value created given by the user
        void CreateUser(UserData user);

        ///Retrieves all the users on the UserData table in the SQL DB.
        ///
        ///Returns an IEnumerable object that holds UserData objects created with the data of the
        ///existing users on the UserData table
        IEnumerable<UserData> GetAllUSers();

        ///Deletes an user on the UserData table in the SQL DB.
        ///
        ///@param user is a UserData object created with the extension user values provided by the user
        void DeleteUser(UserData user);



    }
}