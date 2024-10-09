using _2301C2WebAPIs.Data;
using _2301C2WebAPIs.Models;
using _2301C2WebAPIs.Models.DTOs;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2301C2WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly ShowroomContext db;
        public ManufacturerController(ShowroomContext _db)
        {
            db=_db;
        }

        [HttpGet]
        public IActionResult GetManufacturers()
        {
            return Ok(db.Manufacturers.ToList());
        }

        [HttpPost]
        public IActionResult AddManufacturer(ManufacturerDTO data)
        {

            Manufacturer manufacturer = new Manufacturer()
            {
                Name = data.Name,
                City = data.City,
            };

           var addedManufacturer= db.Manufacturers.Add(manufacturer);
            db.SaveChanges();
            return Ok(addedManufacturer.Entity);
        }

        [HttpPut]
        public IActionResult EditManufacturer(int id ,ManufacturerDTO data)
        {
            var manufacturerToUpdate = db.Manufacturers.Find(id);

            manufacturerToUpdate.Name = data.Name;
            manufacturerToUpdate.City = data.City;

           

            var updatedManufacturer = db.Manufacturers.Update(manufacturerToUpdate);
            db.SaveChanges();
            return Ok(updatedManufacturer.Entity);
        }


    }
}
