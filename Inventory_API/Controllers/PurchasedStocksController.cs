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
    public class PurchasedStocksController : ControllerBase
    {
        private readonly InventoryDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IPurchasedStocksRepository purchasedStocksRepository;
        private readonly IRawMaterialsRepository rawMaterialsRepository;

        public PurchasedStocksController(InventoryDbContext dbContext,
            IMapper mapper,
            IPurchasedStocksRepository purchasedStocksRepository,
            IRawMaterialsRepository rawMaterialsRepository)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.purchasedStocksRepository = purchasedStocksRepository;
            this.rawMaterialsRepository = rawMaterialsRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("SPGetAllPurchasedStocks")]
        public async Task<IActionResult> GetAllPurchasedStocks()
        {
            try
            {
                var purchasedStocks = await purchasedStocksRepository.GetAllPurchasedStocksAsync();
                return Ok(purchasedStocks);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddPurchasedStocksRequestDto addPurchasedStocksRequestDto)
        {
            var purchasedStocksDomainModel = mapper.Map<PurchasedStocks>(addPurchasedStocksRequestDto);
            purchasedStocksDomainModel.EntryDate = DateTime.Now;

            var rawMaterials = await rawMaterialsRepository.GetRawMaterialsByID(addPurchasedStocksRequestDto.RawMaterialId);
            rawMaterials.AvailableStocks = rawMaterials.AvailableStocks + addPurchasedStocksRequestDto.Quantity;

            purchasedStocksDomainModel = await purchasedStocksRepository.CreateAsync(purchasedStocksDomainModel); 

            var purchasedStocksResponseDto = mapper.Map<PurchasedStocksResponseDto>(purchasedStocksDomainModel);
            purchasedStocksResponseDto.AvailableStocks = rawMaterials.AvailableStocks;

            return Ok(purchasedStocksResponseDto);
        }

    }
}
