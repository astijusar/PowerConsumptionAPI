using AutoMapper;
using PowerConsumptionAPI.Models.DTOs.Computer;
using PowerConsumptionAPI.Models.DTOs.PowerConsumption;

namespace PowerConsumptionAPI.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Computer, ComputerDto>()
                .ForMember(dest => dest.Inactivity,
                    opt => opt.MapFrom(src => src.PowerConsumptionData.FirstOrDefault().Inactivity));
            CreateMap<ComputerCreationDto, Computer>();
            CreateMap<ComputerUpdateDto, Computer>();

            CreateMap<PowerConsumption, PowerConsumptionDto>();
            CreateMap<PowerConsumptionCreationDto, PowerConsumption>();
        }
    }
}
