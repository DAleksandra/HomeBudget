using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HomeBudget.API.Data;
using HomeBudget.API.DTOs;
using HomeBudget.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HomeBudget.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public IMapper _mapper { get; }

        public IBudgetRepository _budget { get; }

        public AuthController(IAuthRepository repo, IMapper mapper, IConfiguration config, IBudgetRepository budget)
        {
            _budget = budget;
            _config = config;
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if (await _repo.UserExists(userForRegisterDto.Username))
                return BadRequest("Username already exists");

            var userToCreate = _mapper.Map<User>(userForRegisterDto);

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            var userToReturn = _mapper.Map<UserToReturnDto>(createdUser);



            return Ok(userToReturn);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _repo.Login(userForLoginDto.Username, userForLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_config.GetSection("AppSettings:Token").Value));

            //decripting key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            //Allow to create token
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var user = _mapper.Map<UserToReturnDto>(userFromRepo);

            List<RecurringOutgoing> recurringOutgoings = await _budget.GetRecurringOutgoings(userFromRepo.Id);
            var outgoings = await _budget.GetOutgoings(userFromRepo.Id);

            foreach(var rec in recurringOutgoings)
            {
                var outgoingForRepo = _mapper.Map<OutgoingForCreationDto>(rec);
                outgoingForRepo.DateAdded = DateTime.Now;
                rec.DateAdded = DateTime.Now;

                if(rec.Interval == "Daily")
                {
                    if(outgoings.Where(x => x.Description == rec.Description).OrderByDescending(x => x.DateAdded).FirstOrDefault().DateAdded.AddDays(1) < rec.DateAdded)
                    {
                        userFromRepo.Outgoings.Add(_mapper.Map<Outgoing>(outgoingForRepo));
                        await _budget.SaveAll();
                        rec.DateAdded = outgoingForRepo.DateAdded;
                    }
                }
                else if(rec.Interval == "Weekly")
                {
                    if(outgoings.Where(x => x.Description == rec.Description).OrderByDescending(x => x.DateAdded).FirstOrDefault().DateAdded.AddDays(7) < rec.DateAdded)
                    {
                        userFromRepo.Outgoings.Add(_mapper.Map<Outgoing>(outgoingForRepo));
                        await _budget.SaveAll();
                        rec.DateAdded = DateTime.Now;
                    }
                }
                else if(rec.Interval == "Monthly")
                {
                    if(outgoings.Where(x => x.Description == rec.Description).OrderByDescending(x => x.DateAdded).FirstOrDefault().DateAdded.AddMonths(1) < rec.DateAdded)
                    {
                        userFromRepo.Outgoings.Add(_mapper.Map<Outgoing>(outgoingForRepo));
                        await _budget.SaveAll();
                        rec.DateAdded = DateTime.Now;
                    }
                }
                else if(rec.Interval == "Yearly")
                {
                    if(outgoings.Where(x => x.Description == rec.Description).OrderByDescending(x => x.DateAdded).FirstOrDefault().DateAdded.AddYears(1) < rec.DateAdded)
                    {
                        userFromRepo.Outgoings.Add(_mapper.Map<Outgoing>(outgoingForRepo));
                        await _budget.SaveAll();
                        rec.DateAdded = DateTime.Now;
                    }
                }
            }

            List<RecurringIncome> recurringIncomes = await _budget.GetRecurringIncomes(userFromRepo.Id);
            var incomes = await _budget.GetIncomes(userFromRepo.Id);

            foreach(var inc in recurringIncomes)
            {
                var incomeForRepo = _mapper.Map<IncomeForCreationDto>(inc);
                incomeForRepo.DateAdded = DateTime.Now;
                inc.DateAdded = DateTime.Now;

                if(inc.Interval == "Daily")
                {
                    if(incomes.Where(x => x.Description == inc.Description).OrderByDescending(x => x.DateAdded).FirstOrDefault().DateAdded.AddDays(1) < inc.DateAdded)
                    {
                        userFromRepo.Incomes.Add(_mapper.Map<Income>(incomeForRepo));
                        await _budget.SaveAll();
                        inc.DateAdded = incomeForRepo.DateAdded;
                    }
                }
                else if(inc.Interval == "Weekly")
                {
                    if(incomes.Where(x => x.Description == inc.Description)
                    .OrderByDescending(x => x.DateAdded)
                    .FirstOrDefault().DateAdded
                    .AddDays(7) < inc.DateAdded)
                    {
                        userFromRepo.Incomes.Add(_mapper.Map<Income>(incomeForRepo));
                        await _budget.SaveAll();
                        inc.DateAdded = DateTime.Now;
                    }
                }
                else if(inc.Interval == "Monthly")
                {
                    if(incomes.Where(x => x.Description == inc.Description).OrderByDescending(x => x.DateAdded).FirstOrDefault().DateAdded.AddMonths(1) < inc.DateAdded)
                    {
                        userFromRepo.Incomes.Add(_mapper.Map<Income>(incomeForRepo));
                        await _budget.SaveAll();
                        inc.DateAdded = DateTime.Now;
                    }
                }
                else if(inc.Interval == "Yearly")
                {
                    if(incomes.Where(x => x.Description == inc.Description).OrderByDescending(x => x.DateAdded).FirstOrDefault().DateAdded.AddYears(1) < inc.DateAdded)
                    {
                        userFromRepo.Incomes.Add(_mapper.Map<Income>(incomeForRepo));
                        await _budget.SaveAll();
                        inc.DateAdded = DateTime.Now;
                    }
                }
            }


        
            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user
            });


        }

    }
}