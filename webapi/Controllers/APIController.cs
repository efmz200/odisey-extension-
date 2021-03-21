using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using webapi.Data;
using webapi.Metadata;
using AutoMapper;
using webapi.DTOs;

namespace webapi.Controllers
{
    ///API controller class, manages specified endpoints, an implementation of the .NET ControllerBase interface.
    ///
    ///
    ///This is a class that effectively controls the endpoints
    ///of the REST API that have to do with obtaining data of multiple songs at once and creating new song entries
    ///Http/Https routes are declared and every action performed is different by the verb it uses.

    [Route("api/songs")]
    [ApiController]
    public class MetadataController : ControllerBase
    {
        private readonly IWebApi _repository;
        private readonly IMapper _mapper;

        ///public constructor of the class.
        ///
        ///The constructor of the controller class takes 2 parameters listed below, this is a 
        ///standard constructor implemented for this technology, makes yse of readonly types
        ///@param repository An instance of a IWebApi object
        ///@param mapper An instance of an IMapper object
        public MetadataController(IWebApi repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        ///An action result that retrieves all the songs in the SQL DB.
        ///An action result is a member function that holds the logical operations performed
        ///when a request reaches an endpoint. This specific endpoint runs on https://localhost:5001/api/songs
        ///and http://localhost:5000/api/songs, this action result handles the GET operation, and returns back
        ///all the songs stored on the SQL DB and a 200 ok code back to the front end
        
        [HttpGet]
        public ActionResult<IEnumerable<SongRead>> GetAllSongs()
        {
            Request.Headers.TryGetValue("email",out var emailValue);
            Request.Headers.TryGetValue("UserName",out var userID);
            
            if(_repository.GetUserById(userID) == null){
                if(userID!=""){
                    var user = new UserCreate();
                    user.name = userID;
                    user.email = emailValue;
                    var userModel = _mapper.Map<UserData>(user);
                    _repository.CreateUser(userModel);
                    _repository.SaveChanges();
                }else{
                    return Unauthorized("Invalid user");
                }   
            }
            
            var songItems = _repository.GetAllSongs();
            return Ok(_mapper.Map<IEnumerable<SongRead>>(songItems));
            
        }

        ///An action result that retrieves a specific song on the DB that matches a given search criteria.
        ///An action result is a member function that holds the logical operations performed
        ///when a request reaches an endpoint. This specific endpoint runs on https://localhost:5001/api/songs/{criteria}
        ///and http://localhost:5000/api/songs/{criteria}, the criteria value is specified by the user upon search,
        ///this action result handles the GET operation, and returns back
        ///a song stored on the SQL DB that matches the search criteria and a 200 ok code back to the front end in case
        ///that the operation was succesful, if no song matching the given criteria was found it returns a 404 not found code
        ///@param criteria is a string value with the search criteria given by the user in the frontend
        [HttpGet("{criteria}")]
        [ActionName(nameof(GetSongByCriteria))]
        public ActionResult <IEnumerable<SongRead>> GetSongByCriteria(string criteria)
        {
            Request.Headers.TryGetValue("email",out var emailValue);
            Request.Headers.TryGetValue("UserName",out var userID);
           

            if(_repository.GetUserById(userID) == null){
                if(userID!=""){
                    var user = new UserCreate();
                    user.name = userID;
                    user.email = emailValue;
                    var userModel = _mapper.Map<UserData>(user);
                    _repository.CreateUser(userModel);
                    _repository.SaveChanges();
                }else{
                    return Unauthorized("Invalid user");
                }   
            }

            var songItem = _repository.GetSongByCriteria(criteria);
            if(songItem!=null){

                return Ok(_mapper.Map<IEnumerable<SongRead>>(songItem));

            }else{

                return NotFound();
            }
            

        }

        ///An action result that posts a song to the backend server in order to be added to the SQL DB.
        ///An action result is a member function that holds the logical operations performed
        ///when a request reaches an endpoint. This specific endpoint runs on https://localhost:5001/api/songs
        ///and http://localhost:5000/api/songs, this action result handles the POST operation, and returns back
        ///all the newly created song stored on the SQL DB and a 200 ok code back to the front end
        ///@param newSong A DTO containing the required metadata of the song that's being created
        [HttpPost]
        public ActionResult<SongRead> CreateSong(SongCreate newSong)
        {
            Request.Headers.TryGetValue("email",out var emailValue);
            Request.Headers.TryGetValue("UserName",out var userID);
            
            if(_repository.GetUserById(userID) == null){
                if(userID!=""){
                    var user = new UserCreate();
                    user.name = userID;
                    user.email = emailValue;
                    var userModel = _mapper.Map<UserData>(user);
                    _repository.CreateUser(userModel);
                    _repository.SaveChanges();
                }else{
                    return Unauthorized("Invalid user");
                }   
            }

            var songModel = _mapper.Map<SongData>(newSong);
            _repository.CreateSong(songModel);
            _repository.SaveChanges();
            var songRead = _mapper.Map<SongRead>(songModel);
            return Ok(songRead);
            
        }

    }

    ///API controller class, manages specified endpoints, an implementation of the .NET ControllerBase interface.
    ///
    ///This is the class that controls the endpoints of the REST API that perform actions that have to do with fetching and
    ///deleting single metadata elements on the DB, making use of the ID identifiers assigned to them.
    ///Http/Https routes are declared and every action performed is different by the verb it uses.
    [Route("api/songs/id/")]
    [ApiController]
    public class FetchSongController : ControllerBase
    {
        private readonly IWebApi _repository;
        private readonly IMapper _mapper;

        ///public constructor of the class.

        ///The constructor of the controller class takes 2 parameters listed below, this is a 
        ///standard constructor implemented for this technology, makes yse of readonly types
        ///@param repository An instance of a IWebApi object
        ///@param mapper An instance of an IMapper object
        public FetchSongController(IWebApi repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        ///An action result that retrieves a specific song on the DB that matches a given DB Id.
        ///An action result is a member function that holds the logical operations performed
        ///when a request reaches an endpoint. This specific endpoint runs on https://localhost:5001/api/songs/id/{id}
        ///and http://localhost:5000/api/songs/id/{id}, the id value is specified by the user upon search,
        ///this action result handles the GET operation, and returns back
        ///a song stored on the SQL DB that matches the unique DB Id and a 200 ok code back to the front end in case that
        ///the operation was successful, if no song matching the Id was found, a 404 not found code is returned to the frontend
        [HttpGet("{id}")]
        public ActionResult<SongRead> GetSongById(int id)
        {
            Request.Headers.TryGetValue("email",out var emailValue);
            Request.Headers.TryGetValue("UserName",out var userID);
            
            if(_repository.GetUserById(userID) == null){
                if(userID!=""){
                    var user = new UserCreate();
                    user.name = userID;
                    user.email = emailValue;
                    var userModel = _mapper.Map<UserData>(user);
                    _repository.CreateUser(userModel);
                    _repository.SaveChanges();
                }else{
                    return Unauthorized("Invalid user");
                }   
            }
            var songItem = _repository.GetSongById(id);
            
            if(songItem!=null)
            {
                return Ok(_mapper.Map<SongRead>(songItem));
            }else{
                return NotFound();
            }
        }

        ///An action result that deletes a specific song on the DB that matches a given DB Id.
        ///An action result is a member function that holds the logical operations performed
        ///when a request reaches an endpoint. This specific endpoint runs on https://localhost:5001/api/songs/id/{id}
        ///and http://localhost:5000/api/songs/id/{id}, the id value is specified by the user upon search,
        ///this action result handles the DELETE operation, and returns back
        ///a 204 no content code back to the front end in case that
        ///the operation was successful, if no song matching the Id was found, a 404 not found code is returned to the frontend
        [HttpDelete("{id}")]
        public ActionResult DeleteSong(int id)
        {
            Request.Headers.TryGetValue("email",out var emailValue);
            Request.Headers.TryGetValue("UserName",out var userID);
            
            if(_repository.GetUserById(userID) == null){
                if(userID!=""){
                    var user = new UserCreate();
                    user.name = userID;
                    user.email = emailValue;
                    var userModel = _mapper.Map<UserData>(user);
                    _repository.CreateUser(userModel);
                    _repository.SaveChanges();
                }else{
                    return Unauthorized("Invalid user");
                }   
            }

            var songItem = _repository.GetSongById(id);

            if(songItem!=null)
            {
                _repository.DeleteSong(songItem);
                _repository.SaveChanges();
                return NoContent();
            }else{
                return NotFound();
            }
        }
    }

    ///API controller class, manages specified endpoints, an implementation of the .NET ControllerBase interface.
    ///
    ///This is a class that effectively controls the endpoints
    ///of the REST API that has to do with obtaining  and creating data relevant to users of the web extension
    ///Http/Https routes are declared and every action performed is different by the verb it uses.
    [Route("api/users")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        private readonly IWebApi _repository;
        private IMapper _mapper;

        ///public constructor of the class.
        ///
        ///The constructor of the controller class takes 2 parameters listed below, this is a 
        ///standard constructor implemented for this technology, makes use of readonly types
        ///@param repository An instance of a IWebApi object
        ///@param mapper An instance of an IMapper object
        public UserDataController(IWebApi repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        ///An action result that retrieves all the users in the SQL DB.
        ///
        ///An action result is a member function that holds the logical operations performed
        ///when a request reaches an endpoint. This specific endpoint runs on https://localhost:5001/api/users/
        ///and http://localhost:5000/api/users/, this action result handles the GET operation, and returns back
        ///all the songs stored on the SQL DB and a 200 ok code back to the front end
        [HttpGet]
        public ActionResult<IEnumerable<UserRead>> GetAllUsers()
        {
            var userItems = _repository.GetAllUSers();
            return Ok(_mapper.Map<IEnumerable<UserRead>>(userItems));
        }

        ///An action result that gets a specific user on the DB that matches a given DB Id.
        ///
        ///An action result is a member function that holds the logical operations performed
        ///when a request reaches an endpoint. This specific endpoint runs on https://localhost:5001/api/users/{id}
        ///and http://localhost:5000/api/users/{id}, the id value is specified by the user upon search,
        ///this action result handles the GET operation, and returns back
        ///an user that matches the given Id and an ok 200 code back to the front end in case that
        ///the operation was successful, if no user matching the Id was found, a 404 not found code is returned to the frontend
        [HttpGet("{id}")]
        public ActionResult<UserRead> GetUserById(string id)
        {
            var userItem = _repository.GetUserById(id);
            
            if(userItem!=null)
            {
                return Ok(_mapper.Map<UserRead>(userItem));
            }else{
                return NotFound(null);
            }
        }

        ///An action result that deletes a specific user on the DB that matches a given DB Id.
        ///
        ///An action result is a member function that holds the logical operations performed
        ///when a request reaches an endpoint. This specific endpoint runs on https://localhost:5001/api/users/{id}
        ///and http://localhost:5000/api/songs/id/{id}, the id value is specified by the user upon search,
        ///this action result handles the DELETE operation, and returns back
        ///a 204 no content code back to the front end in case that
        ///the operation was successful, if no user matching the Id was found, a 404 not found code is returned to the frontend
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(string id)
        {
            Request.Headers.TryGetValue("email",out var emailValue);
            Request.Headers.TryGetValue("UserName",out var userID);
            
            if(_repository.GetUserById(userID) == null){
                if(userID!=""){
                    var user = new UserCreate();
                    user.name = userID;
                    user.email = emailValue;
                    var userModel = _mapper.Map<UserData>(user);
                    _repository.CreateUser(userModel);
                    _repository.SaveChanges();
                }else{
                    return Unauthorized("Invalid user");
                }   
            }
            var userItem = _repository.GetUserById(id);

            if(userItem!=null)
            {
                _repository.DeleteUser(userItem);
                _repository.SaveChanges();
                return NoContent();
            }else{
                return NotFound();
            }
        }

        ///An action result that creates a new user on the DB.
        ///
        ///An action result is a member function that holds the logical operations performed
        ///when a request reaches an endpoint. This specific endpoint runs on https://localhost:5001/api/users/
        ///and http://localhost:5000/api/users/, the id value is specified by the user upon search,
        ///this action result handles the POST operation, and returns back
        ///a 200 ok code back to the front end in case that
        ///the operation was successful along with the newly created user's data
        [HttpPost]
        public ActionResult<UserRead> CreateUser(UserCreate newUser)
        {
            var userModel = _mapper.Map<UserData>(newUser);
            _repository.CreateUser(userModel);
            _repository.SaveChanges();

            var userRead = _mapper.Map<UserRead>(userModel);

            return Ok(userRead);

        }
    }

    ///API controller class, manages specified endpoints, an implementation of the .NET ControllerBase interface.
    ///
    ///This is a class that effectively controls the endpoints
    ///of the REST API that has to do with obtaining confirmation of existence of data relevant to users of the web extension
    ///Http/Https routes are declared and every action performed is different by the verb it uses.
    [Route("api/users/exists")]
    [ApiController]
    public class UserExistsController : ControllerBase
    {
        private readonly IWebApi _repository;
        private IMapper _mapper;


        ///public constructor of the class.
        ///
        ///The constructor of the controller class takes 2 parameters listed below, this is a 
        ///standard constructor implemented for this technology, makes use of readonly types
        ///@param repository An instance of a IWebApi object
        ///@param mapper An instance of an IMapper object
        public UserExistsController (IWebApi repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        ///An action result that checks for the existence of an user on the DB.
        ///
        ///An action result is a member function that holds the logical operations performed
        ///when a request reaches an endpoint. This specific endpoint runs on https://localhost:5001/api/users/exists/{id}
        ///and http://localhost:5000/api/users/exists/{id}, the id value is specified by the user upon search,
        ///this action result handles the GET operation, and returns back
        ///a 200 ok code and a boolean true back to the front end in case that
        ///the operation was successful, if no user matching the Id was found, a 404 not found code is returned to the frontend
        [HttpGet("{id}")]
        public ActionResult<UserRead> GetUserById(string id)
        {
            var userItem = _repository.GetUserById(id);
            
            if(userItem!=null)
            {
                return Ok(true);
            }else{
                return NotFound(false);
            }
        }

    }

}