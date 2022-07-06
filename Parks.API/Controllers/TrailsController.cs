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
    public class TrailsController : ControllerSuper
    {
        private readonly IUnitOfWork<ParksDbContext> _unitOfWork;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        public TrailsController(IUnitOfWork<ParksDbContext> unitOfWork,
            Serilog.ILogger logger, IMapper mapper)
            : base(logger, mapper)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Gets selected Trail
        /// </summary>
        /// <param name="id">Trail ID</param>
        /// <returns>Trail object</returns>
        [HttpGet]
        [Route("GetTrailById")]
        public IActionResult GetTrailById(int id)
        {
            try
            {
                var trail = _unitOfWork.TrailRepository.GetTrailById(id);
                if(trail == null)
                {
                    return NotFound("The requested trail was not found");
                }
                var trailDTO = Mapper.Map<TrailDTO>(trail);
                return Ok(trailDTO);
            }
            catch (Exception ex)
            {
                WriteExceptionMessage(ex);
            }

            return NotFound();
        } 

        [HttpGet]
        [Route("GetAllTrails")]
        public IActionResult GetTrails()
        {
            try
            {
                var trails = _unitOfWork.TrailRepository.GetTrails();
                var trailsDTO = Mapper.Map<IEnumerable<TrailDTO>>(trails);

                return Ok(trailsDTO);
            }
            catch (Exception ex)
            {
                return WriteExceptionMessage(ex);
            }
        }

        [HttpPost]
        [Route("CreateTrail")]
        public async Task<IActionResult> CreateAsync(TrailCreateDTO trailDTO)
        {
            try
            {
                if (trailDTO == null)
                {
                    return BadRequest("No National Trail requested");
                }
                if (ModelState.IsValid)
                {
                    var trailExists = await _unitOfWork.TrailRepository.IsTrailThere(trailDTO.Name);
                    if (trailExists)
                    {
                        return Problem($"Trail '{trailDTO.Name}' already exists");
                    }
                    var trail = Mapper.Map<Trail>(trailDTO);
                    var result = await _unitOfWork.TrailRepository.CreateAsync(trail);

                    if (result < 1)
                    {
                        throw new Exception($"Trail {trailDTO.Name} could not be created, please try again later");
                    }
                    return Created("CreateTrail",$"Trail {trail.Name} created successfully.");
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return WriteExceptionMessage(ex);
            }
        }

        [HttpDelete]
        [Route("DeleteTrail")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var dbTrail = await _unitOfWork.TrailRepository.GetByIdAsync(id);
                if (dbTrail == null)
                {
                    return NotFound("The requested Trail was not found");
                }
                var result = _unitOfWork.TrailRepository.Remove(dbTrail);
                if (result == 0)
                {
                    return Problem($"The Trail {dbTrail.Name} could not be deleted");
                }

                return NoContent();

            }
            catch (Exception ex)
            {
                return WriteExceptionMessage(ex);
            }
        }

        //[HttpPatch("{id:int}", Name = "UpdateTrail")] this makes the id parameter required in swagger
        [HttpPatch]
        [Route("UpdateTrail")]
        public IActionResult UpdateAsync(int id, TrailUpdateDTO trailDTO)
        {
            try
            {
                if (trailDTO == null || id != trailDTO.Id)
                {
                    return BadRequest("No Trail requested");
                }
                if (ModelState.IsValid)
                {
                    
                    var trail = Mapper.Map<Trail>(trailDTO);
                    var result = _unitOfWork.TrailRepository.Update(trail);

                    if(result == 0)
                    {
                        return Problem($"The Trail {trail.Name} could not be updated");
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
