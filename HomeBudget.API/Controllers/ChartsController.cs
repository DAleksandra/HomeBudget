using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using HomeBudget.API.Data;
using HomeBudget.API.Helpers;
using HomeBudget.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeBudget.API.Controllers
{
    [Authorize]
    [Route("api/{userId}/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        public IMapper _mapper { get; }
        public IBudgetRepository _repo { get; }
    
        public ChartsController(IBudgetRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("bar")]
        public async Task<IActionResult> GetData(int userId, [FromQuery]DataFilter dateFilter)
        {   
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var incomesFromRepo = await _repo.GetIncomes(userId, dateFilter);
            var outgoingsFromRepo = await _repo.GetOutgoings(userId, dateFilter);
            var chartData = new Charts();
            CultureInfo culture = new CultureInfo("de-DE"); 
            chartData.Dates = new List<string>();
            DateTime tempDate = DateTime.Parse(dateFilter.DateStart);
            
            while( tempDate.DayOfYear <= DateTime.Parse(dateFilter.DateEnd).DayOfYear + 1)
            {
                chartData.Dates.Add(tempDate.ToString("d", culture));
                tempDate = tempDate.AddDays(1.0);
            }
            
            //chartData.DateIncomes = incomesFromRepo.OrderBy(x => x.DateAdded).Select(x => x.DateAdded.ToString("d", culture)).Distinct();
            float sum;
            chartData.Incomes = new List<float>();

            foreach(var date in chartData.Dates)
            {
                sum = 0;
                foreach(var el in incomesFromRepo)
                {
                    if(el.DateAdded.ToString("d", culture) == date)
                        sum = sum + el.Amount;
                    
                }
                chartData.Incomes.Add(sum);
            }

            chartData.Outgoings = new List<float>();
            
            foreach(var date in chartData.Dates)
            {
                sum = 0;
                foreach(var el in outgoingsFromRepo)
                {
                    if(el.DateAdded.ToString("d", culture) == date)
                        sum = sum + el.Cost;
                    
                }
                chartData.Outgoings.Add(sum);
            }

            return Ok(chartData);
        }

        [HttpGet("pie")]
        public async Task<IActionResult> GetDataPie(int userId, [FromQuery]DataFilter dateFilter)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var chartData = new PieChart() {
                Incomes = new List<float>(),
                Outgoings = new List<float>()
            };
            var incomesFromRepo = await _repo.GetIncomes(userId, dateFilter);
            var outgoingsFromRepo = await _repo.GetOutgoings(userId, dateFilter);

            chartData.IncomeCategories = incomesFromRepo.Select(x => x.Category).Distinct();
            chartData.OutgoingCategories = outgoingsFromRepo.Select(x => x.Category).Distinct();
            float sum;

             foreach(var cat in chartData.IncomeCategories)
            {
                sum = 0;
                foreach(var el in incomesFromRepo)
                {
                    if(cat == el.Category)
                        sum = sum + el.Amount;
                    
                }
                chartData.Incomes.Add(sum);
            }

             foreach(var cat in chartData.OutgoingCategories)
            {
                sum = 0;
                foreach(var el in outgoingsFromRepo)
                {
                    if(cat == el.Category)
                        sum = sum + el.Cost;
                    
                }
                chartData.Outgoings.Add(sum);
            }

            return Ok(chartData);

        }

    }
}