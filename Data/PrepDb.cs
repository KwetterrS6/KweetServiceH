using System;
using System.Collections.Generic;
using KweetService.Models;
using KweetService.SyncDataServices.Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace KweetService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IUserDataClient>();

                var users = grpcClient.GetAllUsers();

                SeedData(serviceScope.ServiceProvider.GetService<IKweetRepo>(), users);
            }
        }
        
        private static void SeedData(IKweetRepo repo, IEnumerable<User> users)
        {
            Console.WriteLine("Seeding new users...");

            foreach (var user in users)
            {
                if(!repo.ExternalUserExists(user.ExternalID))
                {
                    repo.CreateUser(user);
                }
                repo.SaveChanges();
            }
        }
    }
}