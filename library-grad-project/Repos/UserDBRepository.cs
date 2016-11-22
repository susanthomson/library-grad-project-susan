using LibraryGradProject.Models;
using LibraryGradProject.DAL;
using System.Collections.Generic;
using System.Linq;

namespace LibraryGradProject.Repos
{
    public class UserDBRepository : IUserRepository
    {

        private ILibraryContext db;

        public UserDBRepository(ILibraryContext db)
        {
            this.db = db;
        }

        public int GetUserId(string name)
        {
            User foundUser = db.Users.Where(user => user.Name == name).SingleOrDefault();
            if (foundUser != null)
            {
                return foundUser.Id;
            }
            else
            {
                var addedUser = db.Users.Add(new User() { Name = name });
                return addedUser.Id;
            }
        }

    }
}