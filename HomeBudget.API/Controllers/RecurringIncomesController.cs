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
    public class RecurringIncomesController : ControllerBase
    {
        public IBudgetRepository _repo { get; }
        public IMapper _mapper { get; }
        public RecurringIncomesController(IBudgetRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecurringIncomes(int userId)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var recurringIncomesFromRepo = await _repo.GetRecurringIncomes(userId);

            var recurringIncomes = _mapper.Map<IEnumerable<RecurringIncomeForReturnDto>>(recurringIncomesFromRepo);

            return Ok(recurringIncomes);
        }

        [HttpGet("{id}", Name="GetRecurringIncome")]
        public async Task<IActionResult> GetRecurringIncome(int userId, int id)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var recurringIncomeFromRepo = await _repo.GetRecurringIncome(id);

            var recurringIncome = _mapper.Map<RecurringIncomeForReturnDto>(recurringIncomeFromRepo);

            return Ok(recurringIncome);
        }

         [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteRecurringIncome(int userId, int id)
         {
             if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                 return Unauthorized();

             var user = await _repo.GetUser(userId);

             if(!user.RecurringIncome.Any(p => p.Id == id))
                return Unauthorized();

            var recurringIncomeFromRepo = await _repo.GetRecurringIncome(id);

            _repo.Delete(recurringIncomeFromRepo);

            if(await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete recurring recurringIncome.");
    
         }

         [HttpPost]
         public async Task<IActionResult> AddRecurringIncome(int userId, RecurringIncomeForCreationDto recurringIncomeForCreation)
         {
             if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                 return Unauthorized();

            var userFromRepo = await _repo.GetUser(userId);

            var recurringIncome = _mapper.Map<RecurringIncome>(recurringIncomeForCreation);

            if(userFromRepo.RecurringIncome == null)
            {
                userFromRepo.RecurringIncome = new List<RecurringIncome>();
            }

            userFromRepo.RecurringIncome.Add(recurringIncome);

            var incomeForRepo = _mapper.Map<Income>(recurringIncomeForCreation);

            userFromRepo.Incomes.Add(incomeForRepo);



            if(await _repo.SaveAll())
            {
                var recurringIncomeToReturn = _mapper.Map<RecurringIncomeForReturnDto>(recurringIncome);
                return CreatedAtRoute("GetIncome", new {userId = userId, id = recurringIncome.Id}, recurringIncomeToReturn);
            }

            return BadRequest("Could not add the recurringIncome.");
         }

         [HttpPut("{id}")]
         public async Task<IActionResult> UpdateRecurringIncome(int userId, int id, RecurringIncomeForReturnDto recurringIncomeForUpdate)
         {
             if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                 return Unauthorized();

            var recurringIncomeFromRepo = await _repo.GetRecurringIncome(id);

            _mapper.Map(recurringIncomeForUpdate, recurringIncomeFromRepo);

            if(await _repo.SaveAll())
                return NoContent();

            return BadRequest("Could not update recurringIncome.");
         }
    }
}