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

namespace HomeBudget.API.Data
{
    [Authorize]
    [Route("api/{userId}/[controller]")]
    [ApiController]
    public class IncomesController : ControllerBase
    {
        public IMapper _mapper { get; }
        public IBudgetRepository _repo { get; }
    
        public IncomesController(IBudgetRepository repo, IMapper mapper)
        {

            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name ="GetIncome")]
        public async Task<IActionResult> GetIncome(int id, int userId)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
                
            var currentUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var incomeFromRepo = await _repo.GetIncome(id);

            if(currentUser != incomeFromRepo.UserId)
                return Unauthorized();

            var income = _mapper.Map<IncomeForReturnDto>(incomeFromRepo);

            return Ok(income);
        }

        [HttpGet]
        public async Task<IActionResult> GetIncomes(int userId, [FromQuery]DataFilter dateFilter)
        {   
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var incomesFromRepo = await _repo.GetIncomes(userId, dateFilter);

            var incomes = _mapper.Map<IEnumerable<IncomeForReturnDto>>(incomesFromRepo);

            return Ok(incomes);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(int id, int userId)
        {
            
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _repo.GetUser(userId);

            if(!user.Incomes.Any(p => p.Id == id))
                return Unauthorized();

            var incomeFromRepo = await _repo.GetIncome(id);

            _repo.Delete(incomeFromRepo);

            if(await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete income.");

        }

        [HttpPost]
        public async Task<IActionResult> AddIncome(int userId, IncomeForCreationDto IncomeForCreation)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(userId);

            var income = _mapper.Map<Income>(IncomeForCreation);

            userFromRepo.Incomes.Add(income);

            if(await _repo.SaveAll())
            {
                var incomeToReturn = _mapper.Map<IncomeForReturnDto>(income);
                return CreatedAtRoute("GetIncome", new {userId = userId, id = income.Id}, incomeToReturn);
            }

            return BadRequest("Could not add the income.");

            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncome(int id, int userId, IncomeForReturnDto incomeForUpdateDto) {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var incomeFromRepo = await _repo.GetIncome(id);

            _mapper.Map(incomeForUpdateDto, incomeFromRepo);

            if(await _repo.SaveAll())
                return NoContent();

            return BadRequest("Could not update income.");
        }

        


    }

        
}
