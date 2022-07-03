using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parks.API.Data;
using Parks.API.Models;
using Parks.API.Models.DTOs;
using Parks.API.Repositories.Abstractions;

namespace Parks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NationalParksController : ControllerSuper
    {
        private readonly IUnitOfWork<ParksDbContext> _unitOfWork;

        public NationalParksController(IUnitOfWork<ParksDbContext> unitOfWork,
            Serilog.ILogger logger, IMapper mapper)
            : base(logger, mapper)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("GetById")]
        public async Task<ActionResult<NationalPark>> GetParkById(int id)
        {
            try
            {
                var park = await _unitOfWork.NationalParkRepository.GetByIdAsync(id);
                if(park == null)
                {
                    return NotFound("The requested park was not found");
                }
                return Ok(park);
            }
            catch (Exception ex)
            {
                WriteExceptionMessage(ex);
            }

            return NotFound();
        } 

        [HttpGet]
        [Route("GetAllParks")]
        public async Task<ActionResult<IEnumerable<NationalPark>>> GetAllParksAsync()
        {
            try
            {
                var parks = await _unitOfWork.NationalParkRepository.GetAllAsync();
                var parksDTO = Mapper.Map<IEnumerable<NationalParkDTO>>(parks);

                return Ok(parksDTO);
            }
            catch (Exception ex)
            {
                return WriteExceptionMessage(ex);
            }
        }

        [HttpPost]
        [Route("CreatePark")]
        public async Task<IActionResult> CreateAsync(NationalParkDTO nationalParkDTO)
        {
            try
            {
                if (nationalParkDTO == null)
                {
                    return NotFound("No National Park requested");
                }
                if (ModelState.IsValid)
                {
                    var parkExists = await _unitOfWork.NationalParkRepository.IsParkThere(nationalParkDTO.Name);
                    if (parkExists)
                    {
                        return Problem($"Park '{nationalParkDTO.Name}' already exists");
                    }
                    var nationalPark = Mapper.Map<NationalPark>(nationalParkDTO);
                    var result = await _unitOfWork.NationalParkRepository.CreateAsync(nationalPark);

                    if (result < 1)
                    {
                        throw new Exception($"National Park {nationalParkDTO.Name} could not be created, please try again later");
                    }
                    return Created("CreatePark",$"Park {nationalPark.Name} created successfully.");
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return WriteExceptionMessage(ex);
            }

        }

        [HttpDelete]
        [Route("DeletePark")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var dbPark = await _unitOfWork.NationalParkRepository.GetByIdAsync(id);
                if (dbPark == null)
                {
                    return NotFound("The requested Park was not found");
                }
                var result = _unitOfWork.NationalParkRepository.Remove(dbPark);
                if (result == 0)
                {
                    return Problem($"The park {dbPark.Name} could not be deleted");
                }

                return Ok();

            }
            catch (Exception ex)
            {
                return WriteExceptionMessage(ex);
            }
        }

        [HttpPatch]
        [Route("UpdatePark")]
        public async Task<IActionResult> UpdateAsync(int id, NationalParkUpdateDTO nationalParkDTO)
        {
            try
            {
                if (nationalParkDTO == null || id != nationalParkDTO.Id)
                {
                    return NotFound("No National Park requested");
                }
                if (ModelState.IsValid)
                {
                    var dbNationalPark = await _unitOfWork.NationalParkRepository.GetByIdAsync(nationalParkDTO.Id);
                    if(dbNationalPark == null)
                    {
                        return Problem($"The Park {nationalParkDTO.Name} was not found");
                    }
                    var result = _unitOfWork.NationalParkRepository.Update(dbNationalPark);

                    if(result == 0)
                    {
                        return Problem($"The park {dbNationalPark.Name} could not be updated");
                    }

                    return NoContent();
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return WriteExceptionMessage(ex);
            }
        }

    }
}
