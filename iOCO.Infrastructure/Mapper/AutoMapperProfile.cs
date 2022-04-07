using AutoMapper;
using iOCO.Core.Domain;
using iOCO.Core.Model;

namespace iOCO.Infrastructure.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Inventory, InventoryDetails>().ReverseMap();
            CreateMap<Survivor, SurvivorDetails>().ReverseMap();
        }
    }
}
