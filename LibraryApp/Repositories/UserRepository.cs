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
    

        public void DeleteUser(string userId)
        {
            var userToDelete = _users.FirstOrDefault(u => u.Id == userId);
            if (userToDelete != null)
            {
                _users.Remove(userToDelete);
            }
        }

        public User GetUserByName(string name)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                // Update other properties as needed
            }
        }
    }
}

