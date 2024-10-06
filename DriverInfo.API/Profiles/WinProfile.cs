using AutoMapper;
using DriverInfo.API.Services;

namespace DriverInfo.API.Profiles
{
    public class WinProfile : Profile
    {
        public WinProfile()
        {
            CreateMap<Entities.Win, Models.WinDto>();
            CreateMap<Models.WinForCreationDto, Entities.Win>();
            CreateMap<Models.WinForUpdateDto, Entities.Win>();
            CreateMap<Entities.Win, Models.WinForUpdateDto>();
        }
    }
}
