using AutoMapper;
using PowerConsumptionAPI.Models.DTOs.Computer;
using PowerConsumptionAPI.Models.DTOs.Limit;
using PowerConsumptionAPI.Models.DTOs.PowerConsumption;
using PowerConsumptionAPI.Models.DTOs.User;
using PowerConsumptionAPI.Repository;

namespace PowerConsumptionAPI.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Computer, ComputerDto>();
            CreateMap<ComputerCreationDto, Computer>();
            CreateMap<ComputerUpdateDto, Computer>();

            CreateMap<PowerConsumption, PowerConsumptionDto>();
            CreateMap<PowerConsumptionCreationDto, PowerConsumption>();

            CreateMap<UserRegistrationDto, User>();

            CreateMap<Limit, LimitDto>();
            CreateMap<LimitCreationDto, Limit>();
        }
    }
}
