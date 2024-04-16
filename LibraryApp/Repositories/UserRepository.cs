using System;
using LibraryApp.Models;

namespace LibraryApp.Repositories
{
	public class UserRepository : IUserRepository
	{

        private readonly List<User> _users = new List<User>();

        public UserRepository()
		{
		}

        public void AddUser(User user)
        {
            _users.Add(user);
        }
    

        public void DeleteUser(int userId)
        {
            var userToDelete = _users.FirstOrDefault(u => u.Id == userId);
            if (userToDelete != null)
            {
                _users.Remove(userToDelete);
            }
        }

        public User GetUserById(int userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId);
        }

        public void UpdateUser(User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                // Update other properties as needed
            }
        }
    }
}

