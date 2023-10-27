using AutoMapper;
using Inventory_API.Data;
using Inventory_API.IRepository;
using Inventory_API.Models.Domain;
using Inventory_API.Models.Dtos.RequestDtos;
using Inventory_API.Models.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_API.Controllers
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
        [Authorize(Roles = "Admin,Staff")]
        [Route("SPGetAll")]
        public async Task<IActionResult> SPGetAllResults()
        {
            try
            {
                var rawMaterials = await rawMaterialsRepository.GetSPAllAsync();
                return Ok(rawMaterials);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRawMaterialsByID(Guid id)
        {
            try
            {
                var rawMaterialsDomainModel = await rawMaterialsRepository.GetRawMaterialsByID(id);
                if (rawMaterialsDomainModel == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<RawMaterialResponseDto>(rawMaterialsDomainModel));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] AddRawMaterialsRequestDto addRawMaterialsRequestDto)
        {
            try
            {
                var rawMaterialsDomainModel = mapper.Map<RawMaterials>(addRawMaterialsRequestDto);
                rawMaterialsDomainModel.createdOn = DateTime.UtcNow;

                rawMaterialsDomainModel = await rawMaterialsRepository.CreateAsync(rawMaterialsDomainModel);

                var rawMaterialResponseDto = mapper.Map<RawMaterialResponseDto>(rawMaterialsDomainModel);

                return Ok(rawMaterialResponseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRawMaterials([FromRoute] Guid id, [FromBody] UpdateRawMaterialsRequestDto updateRawMaterialsRequestDto)
        {
            try
            {
                //DTO to Domain Model
                var rawMaterialsDomainModel = mapper.Map<RawMaterials>(updateRawMaterialsRequestDto);
                if (rawMaterialsDomainModel == null)
                {
                    return NotFound();
                }

                rawMaterialsDomainModel = await rawMaterialsRepository.UpdateAsync(id, rawMaterialsDomainModel);

                var rawMaterialResponseDto = mapper.Map<RawMaterialResponseDto>(rawMaterialsDomainModel);

                return Ok(rawMaterialResponseDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var rawMaterialsDomainModel = await rawMaterialsRepository.DeleteAsync(id);
                if (rawMaterialsDomainModel == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<RawMaterialResponseDto>(rawMaterialsDomainModel));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
