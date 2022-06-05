using System;
using System.Text.Json;
using AutoMapper;
using KweetService.Data;
using KweetService.Dtos;
using KweetService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace KweetService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, AutoMapper.IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.UserCreated:
                    addUser(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notifcationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notifcationMessage);

            switch(eventType.Event)
            {
                case "User_Created":
                    Console.WriteLine("--> User Created Event Detected");
                    return EventType.UserCreated;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void addUser(string userCreatedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IKweetRepo>();
                
                var userCreatedDto = JsonSerializer.Deserialize<UserCreatedDto>(userCreatedMessage);

                try
                {
                    var user = _mapper.Map<User>(userCreatedDto);
                    if(!repo.ExternalUserExists(user.ExternalID))
                    {
                        user.Id = 0;
                        repo.CreateUser(user);
                        repo.SaveChanges();
                        Console.WriteLine("--> User added!");
                    }
                    else
                    {
                        Console.WriteLine("--> User already exisits...");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add User to DB {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        UserCreated,
        Undetermined
    }
}