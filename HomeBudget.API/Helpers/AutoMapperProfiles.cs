using System.Linq;
using AutoMapper;
using HomeBudget.API.DTOs;
using HomeBudget.API.Models;

namespace HomeBudget.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {   
        public AutoMapperProfiles()
        {   
            CreateMap<User, UserForRegisterDto>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<User, UserForLoginDto>();
            CreateMap<User, UserToReturnDto>();
            CreateMap<UserForLoginDto, User>();
            CreateMap<OutgoingForCreationDto, Outgoing>();
            CreateMap<Outgoing, OutgoingForReturnDto>();
            CreateMap<OutgoingForReturnDto, Outgoing>();
            CreateMap<RecurringIncome, RecurringIncomeForReturnDto>();
            CreateMap<RecurringIncome, RecurringIncomeForCreationDto>();
            CreateMap<RecurringIncomeForCreationDto, RecurringIncome>();
            CreateMap<RecurringIncomeForReturnDto, RecurringIncome>();
            CreateMap<RecurringIncome, RecurringIncomeForReturnDto>();
            CreateMap<RecurringOutgoing, RecurringOutgoingForReturnDto>();
            CreateMap<RecurringOutgoingForReturnDto, RecurringOutgoing>();
            CreateMap<RecurringOutgoingForCreationDto, Outgoing>();
            CreateMap<RecurringIncomeForCreationDto, Income>();
            CreateMap<IncomeForCreationDto, Income>();
            CreateMap<Income, IncomeForReturnDto>();
            CreateMap<IncomeForReturnDto, Income>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<PhotoForUpdateDto, Photo>();
            CreateMap<RecurringOutgoing, Outgoing>();
            CreateMap<RecurringIncome, Income>();
            CreateMap<RecurringOutgoing, OutgoingForCreationDto>();
            CreateMap<RecurringIncome, IncomeForCreationDto>();
            
        }
    }
}