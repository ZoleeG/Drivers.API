using AutoMapper;

namespace DriverInfo.API.Profiles
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<Entities.Driver, Models.DriverWithoutWinsDto>();
            CreateMap<Entities.Driver, Models.DriverDto>();
        }
    }
}
