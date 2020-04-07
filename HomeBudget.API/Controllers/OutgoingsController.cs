using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using HomeBudget.API.Data;
using HomeBudget.API.DTOs;
using HomeBudget.API.Helpers;
using HomeBudget.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeBudget.API.Controllers
{
    [Authorize]
    [Route("api/{userId}/[controller]")]
    [ApiController]
    public class OutgoingsController : ControllerBase
    {
        
        public IMapper _mapper { get; }
        public IBudgetRepository _repo { get; }
    
        public OutgoingsController(IBudgetRepository repo, IMapper mapper)
        {

            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name ="GetOutgoing")]
        public async Task<IActionResult> GetOutgoing(int id, int userId)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
                
            var currentUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var outgoingFromRepo = await _repo.GetOutgoing(id);

            if(currentUser != outgoingFromRepo.UserId)
                return Unauthorized();

            var outgoing = _mapper.Map<OutgoingForReturnDto>(outgoingFromRepo);

            return Ok(outgoing);
        }

        [HttpGet]
        public async Task<IActionResult> GetOutgoings(int userId, [FromQuery]DataFilter dateFilter)
        {   
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var outgoingsFromRepo = await _repo.GetOutgoings(userId, dateFilter);

            var outgoings = _mapper.Map<IEnumerable<OutgoingForReturnDto>>(outgoingsFromRepo);

            return Ok(outgoings);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOutgoing(int id, int userId)
        {
            
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _repo.GetUser(userId);

            if(!user.Outgoings.Any(p => p.Id == id))
                return Unauthorized();

            var outgoingFromRepo = await _repo.GetOutgoing(id);

            _repo.Delete(outgoingFromRepo);

            if(await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete outgoing.");

        }

        [HttpPost]
        public async Task<IActionResult> AddOutgoing(int userId, OutgoingForCreationDto outgoingForCreation)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(userId);

            var outgoing = _mapper.Map<Outgoing>(outgoingForCreation);

            userFromRepo.Outgoings.Add(outgoing);

            if(await _repo.SaveAll())
            {
                var outgoingToReturn = _mapper.Map<OutgoingForReturnDto>(outgoing);
                return CreatedAtRoute("GetOutgoing", new {userId = userId, id = outgoing.Id}, outgoingToReturn);
            }

            return BadRequest("Could not add the outgoing.");

            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOutgoing(int id, int userId, OutgoingForReturnDto outgoingForUpdateDto) {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var outgoingFromRepo = await _repo.GetOutgoing(id);

            _mapper.Map(outgoingForUpdateDto, outgoingFromRepo);

            if(await _repo.SaveAll())
                return NoContent();

            return BadRequest("Could not update outgoing.");
        }

        


    }
}