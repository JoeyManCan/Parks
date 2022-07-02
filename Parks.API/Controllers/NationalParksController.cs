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
        public async Task<IActionResult> PostAsync(NationalParkDTO nationalPark)
        {
            try
            {
                if (nationalPark == null)
                {
                    return BadRequest("No National Park requested");
                }
                if (ModelState.IsValid)
                {
                    var dbNationalPark = Mapper.Map<NationalPark>(nationalPark);
                    var result = await _unitOfWork.NationalParkRepository.CreateAsync(dbNationalPark);

                    if (result < 1)
                    {
                        throw new Exception($"National Park {nationalPark.Name} could not be created, please try again later");
                    }
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return WriteExceptionMessage(ex);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var dbPark = await _unitOfWork.NationalParkRepository.GetByIdAsync(id);
                if (dbPark == null)
                {
                    return BadRequest("The requested Park was not found");
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

        [HttpPut]
        public async Task<IActionResult> PutAsync(NationalParkDTO nationalParkDTO)
        {
            try
            {
                if (nationalParkDTO == null)
                {
                    return BadRequest("No National Park requested");
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

                    return Ok();
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return WriteExceptionMessage(ex);
            }
        }

        /*
        // GET: NationalParkController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NationalParkController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NationalParkController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NationalParkController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NationalParkController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NationalParkController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NationalParkController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
