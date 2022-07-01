using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parks.API.Data;
using Parks.API.Models;
using Parks.API.Repositories.Abstractions;

namespace Parks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NationalParkController : ControllerBase
    {
        private readonly IUnitOfWork<ParksDbContext> _unitOfWork;

        public NationalParkController(IUnitOfWork<ParksDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet(Name = "GetAllParks")]
        // GET: NationalParkController
        public async Task<ActionResult<IEnumerable<NationalPark>>>GetAllParksAsync()
        {
            var parks = await _unitOfWork.NationalParkRepository.GetAllAsync();

            return Ok(parks);
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
