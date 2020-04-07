using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
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
    public class RecurringOutgoingsController : ControllerBase
    {
        public IBudgetRepository _repo { get; }
        public IMapper _mapper { get; }
        public RecurringOutgoingsController(IBudgetRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecurringOutgoings(int userId)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var recurringOutgoingsFromRepo = await _repo.GetRecurringOutgoings(userId);

            var recurringOutgoings = _mapper.Map<IEnumerable<RecurringOutgoingForReturnDto>>(recurringOutgoingsFromRepo);

            return Ok(recurringOutgoings);
        }

        [HttpGet("{id}", Name="GetRecurringOutgoing")]
        public async Task<IActionResult> GetRecurringOutgoing(int userId, int id)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var recurringOutgoingFromRepo = await _repo.GetRecurringOutgoing(id);

            var recurringOutgoing = _mapper.Map<OutgoingForReturnDto>(recurringOutgoingFromRepo);

            return Ok(recurringOutgoing);
        }

         [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteRecurringOutgoing(int userId, int id)
         {
             if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                 return Unauthorized();

             var user = await _repo.GetUser(userId);

             if(!user.RecurringOutgoing.Any(p => p.Id == id))
                return Unauthorized();

            var recurringOutgoingFromRepo = await _repo.GetRecurringOutgoing(id);

            _repo.Delete(recurringOutgoingFromRepo);

            if(await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete recurring recurringOutgoing.");
    
         }

         [HttpPost]
         public async Task<IActionResult> AddRecurringOutgoing(int userId, RecurringOutgoingForCreationDto recurringOutgoingForCreation)
         {
             if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                 return Unauthorized();

            var userFromRepo = await _repo.GetUser(userId);

            var recurringOutgoing = _mapper.Map<RecurringOutgoing>(recurringOutgoingForCreation);

            if(userFromRepo.RecurringOutgoing == null)
            {
                userFromRepo.RecurringOutgoing = new List<RecurringOutgoing>();
            }

            userFromRepo.RecurringOutgoing.Add(recurringOutgoing);
            
            var outgoingForRepo = _mapper.Map<Outgoing>(recurringOutgoingForCreation);

            userFromRepo.Outgoings.Add(outgoingForRepo);

            if(await _repo.SaveAll())
            {
                var recurringOutgoingToReturn = _mapper.Map<RecurringOutgoingForReturnDto>(recurringOutgoing);
                return CreatedAtRoute("GetOutgoing", new {userId = userId, id = recurringOutgoing.Id}, recurringOutgoingToReturn);
            }

            return BadRequest("Could not add the recurringOutgoing.");
         }

         [HttpPut("{id}")]
         public async Task<IActionResult> UpdateRecurringOutgoing(int userId, int id, RecurringOutgoingForReturnDto recurringOutgoingForUpdate)
         {
             if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                 return Unauthorized();

            var recurringOutgoingFromRepo = await _repo.GetOutgoing(id);

            _mapper.Map(recurringOutgoingForUpdate, recurringOutgoingFromRepo);

            if(await _repo.SaveAll())
                return NoContent();

            return BadRequest("Could not update recurringOutgoing.");
         }
        
    }
}
