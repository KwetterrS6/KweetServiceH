using System.Collections.Generic;
using KweetService.Models;

namespace KweetService.SyncDataServices.Grpc
{
    public interface IUserDataClient
    {
        IEnumerable<User> GetAllUsers();
    }
}