using AutoMapper;
using Inventory.API.Models.Domain;
using Inventory.API.Models.Dtos.RequestDtos;
using Inventory.API.Models.Dtos.ResponseDtos;

namespace Inventory.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RawMaterials, AddRawMaterialsRequestDto>().ReverseMap();
            CreateMap<RawMaterials, RawMaterialResponseDto>().ReverseMap();
            CreateMap<RawMaterials, UpdateRawMaterialsRequestDto>().ReverseMap();
        }
    }
}
