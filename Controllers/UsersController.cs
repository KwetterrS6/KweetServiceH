using System;
using System.Collections.Generic;
using AutoMapper;
using KweetService.Data;
using KweetService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace KweetService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IKweetRepo _repository;
        private readonly IMapper _mapper;

        public UsersController(IKweetRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserReadDto>> GetUsers()
        {
            Console.WriteLine("--> Getting Users from KweetService");

            var userItems = _repository.GetAllUsers();

            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(userItems));
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound POST # Kweet Service");

            return Ok("Inbound test of from Users Controler");
        }
    }
}