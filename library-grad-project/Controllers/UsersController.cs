using LibraryGradProject.Models;
using LibraryGradProject.Repos;
using System.Web.Http;

namespace LibraryGradProject.Controllers
{
    public class UsersController : ApiController
    {

        private IUserRepository _userRepo;

        public UsersController(IUserRepository userRepository)
        {
            _userRepo = userRepository;
        }

        // GET api/<controller>/
        public int Get()
        {
            return _userRepo.GetUserId(User.Identity.Name);
        }

    }
}