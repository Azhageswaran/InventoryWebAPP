using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory.API.Data;
using Inventory.API.Models.Domain;
using Inventory.API.IRepository;
using Inventory.API.Models.Dtos.RequestDtos;
using AutoMapper;
using Inventory.API.Models.Dtos.ResponseDtos;

namespace Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RawMaterialsController : ControllerBase
    {
        private readonly InventoryDbContext _context;
        private readonly IRawMaterialsRepository rawMaterialsRepository;
        private readonly IMapper mapper;

        public RawMaterialsController(InventoryDbContext context,
            IRawMaterialsRepository rawMaterialsRepository,
            IMapper mapper)
        {
            _context = context;
            this.rawMaterialsRepository = rawMaterialsRepository;
            this.mapper = mapper;
        }

        //Get: api/RawMatweials
        [HttpGet]
        [Route("SPGetAll")]
        public async Task<IActionResult> SPGetAllResults()
        {
            var rawMaterials = await rawMaterialsRepository.GetSPAllAsync();
            return  Ok(rawMaterials);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRawMaterialsByID(Guid id)
        {
            var rawMaterialsDomainModel = await rawMaterialsRepository.GetRawMaterialsByID(id);
            if(rawMaterialsDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RawMaterialResponseDto>(rawMaterialsDomainModel));
        }
       /* [HttpGet]
        [Route("SPGetCount")]
        public async Task<IActionResult> SpGetCount()
        {
            var rawCount = await rawMaterialsRepository.GetSpCount();
        }*/

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRawMaterialsRequestDto addRawMaterialsRequestDto)
        {
            var rawMaterialsDomainModel = mapper.Map<RawMaterials>(addRawMaterialsRequestDto);
            //rawMaterialsDomainModel.RawMaterialId = Guid.NewGuid();
            rawMaterialsDomainModel.createdOn = DateTime.UtcNow;

            rawMaterialsDomainModel = await rawMaterialsRepository.CreateAsync(rawMaterialsDomainModel);

            var rawMaterialResponseDto = mapper.Map<RawMaterialResponseDto>(rawMaterialsDomainModel);

            return Ok(rawMaterialResponseDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRawMaterials([FromRoute] Guid id, [FromBody] UpdateRawMaterialsRequestDto updateRawMaterialsRequestDto)
        {
            //DTO to Domain Model
            var rawMaterialsDomainModel = mapper.Map<RawMaterials>(updateRawMaterialsRequestDto);
            if(rawMaterialsDomainModel == null)
            {
                return NotFound();
            }

            rawMaterialsDomainModel = await rawMaterialsRepository.UpdateAsync(id, rawMaterialsDomainModel);

            var rawMaterialResponseDto = mapper.Map<RawMaterialResponseDto>(rawMaterialsDomainModel);

            return Ok(rawMaterialResponseDto);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var rawMaterialsDomainModel = await rawMaterialsRepository.DeleteAsync(id);
            if( rawMaterialsDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RawMaterialResponseDto>(rawMaterialsDomainModel));
        }

       
    }
}
