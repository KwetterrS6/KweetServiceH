using System;
using System.Collections.Generic;
using AutoMapper;
using KweetService.Data;
using KweetService.Dtos;
using KweetService.Models;
using Microsoft.AspNetCore.Mvc;

namespace KweetService.Controllers
{
    [Route("api/c/users/{userId}/[controller]")]
    [ApiController]
    public class KweetsController : ControllerBase
    {
        private readonly IKweetRepo _repository;
        private readonly IMapper _mapper;

        public KweetsController(IKweetRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<KweetReadDto>> GetKweetsForUser(int userId)
        {
            Console.WriteLine($"--> Hit GetKweetsForUser: {userId}");

            if (!_repository.UserExits(userId))
            {
                return NotFound();
            }

            var kweets = _repository.GetKweetsForUser(userId);

            return Ok(_mapper.Map<IEnumerable<KweetReadDto>>(kweets));
        }

        [HttpGet("{kweetId}", Name = "GetKweetForUser")]
        public ActionResult<KweetReadDto> GetKweetForUser(int userId, int kweetId)
        {
            Console.WriteLine($"--> Hit GetKweetForUser: {userId} / {kweetId}");

            if (!_repository.UserExits(userId))
            {
                return NotFound();
            }

            var kweet = _repository.GetKweet(userId, kweetId);

            if(kweet == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<KweetReadDto>(kweet));
        }

        [HttpPost]
        public ActionResult<KweetReadDto> CreateKweetForUser(int userId, KweetCreateDto kweetDto)
        {
             Console.WriteLine($"--> Hit CreateKweetForUser: {userId}");

            if (!_repository.UserExits(userId))
            {
                return NotFound();
            }

            var kweet = _mapper.Map<Kweet>(kweetDto);

            _repository.CreateKweet(userId, kweet);
            _repository.SaveChanges();

            var kweetReadDto = _mapper.Map<KweetReadDto>(kweet);

            return CreatedAtRoute(nameof(GetKweetForUser),
                new {userId = userId, kweetId = kweetReadDto.Id}, kweetReadDto);
        }

    }
}