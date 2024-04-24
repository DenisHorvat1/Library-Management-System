using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LibraryApp.Models;
using LibraryApp.Repositories;
using Moq;
using System.Threading.Tasks;

namespace LibraryApp.UnitTests.Mocks
{
    public class MockIUserRepository
    {
        public static Mock<IUserRepository> GetMock()
        {
            var mock = new Mock<IUserRepository>();
            var users = GetUsers();

            mock.Setup(u => u.GetAllUsersAsync()).ReturnsAsync(users);
            mock.Setup(u => u.GetUserByIdAsync(It.IsAny<int>()))
                .Returns<int>((id) => Task.FromResult(users.FirstOrDefault(u => u.Id == id)));
            mock.Setup(u => u.AddUserAsync(It.IsAny<User>()))
                .Callback<User>((user) => users.Add(user));
            mock.Setup(u => u.UpdateUserAsync(It.IsAny<User>()))
                .Callback<User>((user) =>
                {
                    var existingUser = users.FirstOrDefault(u => u.Id == user.Id);
                    if (existingUser != null)
                    {
                        existingUser.Name = user.Name;
                        existingUser.Email = user.Email;
                    }
                });
            mock.Setup(u => u.DeleteUserAsync(It.IsAny<int>()))
                .Callback<int>((id) =>
                {
                    var userToRemove = users.FirstOrDefault(u => u.Id == id);
                    if (userToRemove != null)
                    {
                        users.Remove(userToRemove);
                    }
                });
            mock.Setup(u => u.UserExistsAsync(It.IsAny<int>()))
                .Returns<int>((id) => Task.FromResult(users.Any(u => u.Id == id)));

            return mock;
        }

        public static List<User> GetUsers()
        {
            return new List<User>
            {
                new User { Id = 1, Name = "User 1", Email = "user1@example.com" },
                new User { Id = 2, Name = "User 2", Email = "user2@example.com" }
            };
        }
    }
}
