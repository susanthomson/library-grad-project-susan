using System.Collections.Generic;

namespace LibraryGradProject.Repos
{
    public interface IUserRepository
    {
        int GetUserId(string name);
    }
}