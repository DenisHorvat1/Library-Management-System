using System;
using LibraryApp.Models;

namespace LibraryApp.Repositories
{
	public interface IUserRepository
	{
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        User GetUserById(int userId);
    }
}

