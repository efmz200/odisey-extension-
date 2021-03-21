using System;
using System.Collections.Generic;
using System.Linq;
using webapi.Metadata;

namespace webapi.Data
{
    ///An implementation of the IWebApi interface.
    public class SqlRepo : IWebApi
    {
        private readonly APIContext _context;

        public SqlRepo(APIContext context)
        {
            _context = context;
        }

        public void CreateSong(SongData song)
        {
            if(song == null)
            {
                throw new ArgumentNullException(nameof(song));
            }

            _context.Songs.Add(song);
        }

        public void CreateUser(UserData user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.UserTable.Add(user);
        }

        public void DeleteSong(SongData song)
        {
            if(song == null)
            {
                throw new ArgumentNullException(nameof(song));
            }

            _context.Songs.Remove(song);
            
        }

        public void DeleteUser(UserData user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.UserTable.Remove(user);
        }

        public IEnumerable<SongData> GetAllSongs()
        {
            return _context.Songs.ToList();
        }

        public IEnumerable<UserData> GetAllUSers()
        {
            return _context.UserTable.ToList();
        }

        public IEnumerable<SongData> GetSongByCriteria(string criteria)
        {
            if(_context.Songs.Where(p => p.Album.Contains(criteria) || p.Artist.Contains(criteria) || p.Name.Contains(criteria) || p.Lyrics.Contains(criteria)).ToList().Count>0){
                
                return _context.Songs.Where(p => p.Id.ToString().Equals(criteria)|| p.Album.Contains(criteria) || p.Artist.Contains(criteria) || p.Name.Contains(criteria) || p.Lyrics.Contains(criteria)).ToList();
            }else{

                return null;
            }
                
        }

        public SongData GetSongById(int id)
        {
 
            if(_context.Songs.FirstOrDefault(p => p.Id == id)!=null)
            {
                return _context.Songs.FirstOrDefault(p => p.Id == id);
            }else{

                return null;
            }
        }

        

        public UserData GetUserById(string id)
        {
            
            if(_context.UserTable.FirstOrDefault(p => p.Name == id)!=null)
            {
                return _context.UserTable.FirstOrDefault(p => p.Name == id);
            }else{

                return null;
            }
            
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}