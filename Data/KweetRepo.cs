using System;
using System.Collections.Generic;
using System.Linq;
using KweetService.Models;

namespace KweetService.Data
{
    public class KweetRepo : IKweetRepo
    {
        private readonly AppDbContext _context;

        public KweetRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateKweet(int userId, Kweet kweet)
        {
            if (kweet == null)
            {
                throw new ArgumentNullException(nameof(kweet));
            }

            kweet.UserId = userId;
            _context.Kweets.Add(kweet);
        }

        public void CreateUser(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Add(user);
        }

        public bool ExternalUserExists(int externalUserId)
        {
            return _context.Users.Any(p => p.ExternalID == externalUserId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public Kweet GetKweet(int userId, int kweetId)
        {
            return _context.Kweets
                .Where(c => c.UserId == userId && c.Id == kweetId).FirstOrDefault();
        }

        public IEnumerable<Kweet> GetKweetsForUser(int userId)
        {
            return _context.Kweets
                .Where(c => c.UserId == userId)
                .OrderBy(c => c.User.Name);
        }

        public bool UserExits(int userId)
        {
            return _context.Users.Any(p => p.Id == userId);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}