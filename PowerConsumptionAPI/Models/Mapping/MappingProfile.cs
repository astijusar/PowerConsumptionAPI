using AutoMapper;
using PowerConsumptionAPI.Models.DTOs.Computer;

namespace PowerConsumptionAPI.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Computer, ComputerDto>();
            CreateMap<ComputerCreationDto, Computer>();
            CreateMap<ComputerUpdateDto, Computer>();
        }
    }
}
