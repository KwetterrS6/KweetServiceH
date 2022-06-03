using System.Collections.Generic;
using KweetService.Models;

namespace KweetService.Data
{
    public interface IKweetRepo
    {
        bool SaveChanges();

        // Users
        IEnumerable<User> GetAllUsers();
        void CreateUser(User user);
        bool UserExits(int userId);
        bool ExternalUserExists(int externalUserId);

        // Kweets
        IEnumerable<Kweet> GetKweetsForUser(int userId);
        Kweet GetKweet(int userId, int KweetId);
        void CreateKweet(int userId, Kweet kweet);
    }
}