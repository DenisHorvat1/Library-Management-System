using System;
using LibraryApp.Models;
using LibraryApp.UnitTests.Mocks;

namespace LibraryApp.UnitTests;

public class UnitTest1
{
    [Fact]
    public void GetAllUsers_ReturnsAllUsers()
    {
        // Arrange
        var mockUserRepository = MockIUserRepository.GetMock();
        var userRepository = mockUserRepository.Object;

        // Act
        var users = userRepository.GetAllUsersAsync().Result;

        // Assert
        Assert.NotNull(users);
        Assert.Equal(2, users.Count());
    }

    [Fact]
    public async Task GetUserByIdAsync_ReturnsCorrectUser()
    {
        // Arrange
        var mockUserRepository = MockIUserRepository.GetMock();
        var userRepository = mockUserRepository.Object;
        int userId = 1; // Choose a user ID from the mock data

        // Act
        var user = await userRepository.GetUserByIdAsync(userId);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(userId, user.Id);
    }

    [Fact]
    public async Task AddUserAsync_ReturnCorrctUsers()
    {
        // Arrange
        var mockUserRepository = MockIUserRepository.GetMock();
        var userRepository = mockUserRepository.Object;
        var newUser = new User { Id = 3, Name = "User 3", Email = "user3@example.com" };

        // Act
        await userRepository.AddUserAsync(newUser);

        var users = userRepository.GetAllUsersAsync().Result;
        // Assert
        Assert.NotNull(users);
        Assert.Equal(2, users.Count());
    }
}