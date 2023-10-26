using AutoMapper;
using Inventory_API.Data;
using Inventory_API.IRepository;
using Inventory_API.Models.Domain;
using Inventory_API.Models.Dtos.RequestDtos;
using Inventory_API.Models.Dtos.ResponseDtos;
using Inventory_API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class MovedStocksController : ControllerBase
    {
        private readonly InventoryDbContext dbContext;
        private readonly IRawMaterialsRepository rawMaterialsRepository;
        private readonly IMovedStocksRepository movedStocksRepository;
        private readonly IMapper mapper;

        public MovedStocksController(InventoryDbContext dbContext,
            IRawMaterialsRepository rawMaterialsRepository,
            IMovedStocksRepository movedStocksRepository,
            IMapper mapper)
            
        {
            this.dbContext = dbContext;
            this.rawMaterialsRepository = rawMaterialsRepository;
            this.movedStocksRepository = movedStocksRepository;
            this.mapper = mapper;
        }

  
        [HttpGet]
        [Route("SPGetMovedStocksAll")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> SpGetMovedStocksAll()
        {
            var movedStocksDomainModel = await movedStocksRepository.GetSPMovedStocksAllAsync();
            return Ok(movedStocksDomainModel);
        }

     
        [HttpPost]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> Create([FromBody] AddMovedStocksRequestDto addMovedStocksRequestDto )
        {
            var MovedStocksDomainModel = mapper.Map<MovedStocks>(addMovedStocksRequestDto);
            MovedStocksDomainModel.ExitDate = DateTime.Now;

            var rawMaterials = await rawMaterialsRepository.GetRawMaterialsByID(addMovedStocksRequestDto.RawMaterialId);
            rawMaterials.AvailableStocks = rawMaterials.AvailableStocks - addMovedStocksRequestDto.Quantity;

            MovedStocksDomainModel = await movedStocksRepository.CreateAsync(MovedStocksDomainModel);

            var movedStocksResponseDto = mapper.Map<MovedStocksResponseDto>(MovedStocksDomainModel);
            movedStocksResponseDto.AvailableStocks = rawMaterials.AvailableStocks;

            return Ok(movedStocksResponseDto);
        }

    }
}
