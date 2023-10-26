using AutoMapper;
using Inventory_API.Models.Domain;
using Inventory_API.Models.Dtos.RequestDtos;
using Inventory_API.Models.Dtos.ResponseDtos;

namespace Inventory_API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<RawMaterials, AddRawMaterialsRequestDto>().ReverseMap();
            CreateMap<RawMaterials, RawMaterialResponseDto>().ReverseMap();
            CreateMap<RawMaterials, UpdateRawMaterialsRequestDto>().ReverseMap();
            CreateMap<PurchasedStocks, AddPurchasedStocksRequestDto>().ReverseMap();
            CreateMap<PurchasedStocks, PurchasedStocksResponseDto>()
               /* .ForMember(x => x.AvailableStocks, opt => opt.MapFrom(x => x.RawMaterials.AvailableStocks))*/
                .ReverseMap();
            CreateMap<MovedStocks, AddMovedStocksRequestDto>().ReverseMap();
            CreateMap<MovedStocks, MovedStocksResponseDto>().ReverseMap();
        }
    }
}
