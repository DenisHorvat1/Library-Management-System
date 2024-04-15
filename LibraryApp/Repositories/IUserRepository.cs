using System;
using LibraryApp.Models;

namespace LibraryApp.Repositories
{
	public interface IUserRepository
	{
        void AddUser(User author);
        void UpdateUser(User author);
        void DeleteUser(string name);
        User GetUserByName(string name);
    }
}

