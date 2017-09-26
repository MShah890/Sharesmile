using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SS_DB_API.Models;
using SS_DB_API.DTO_Models;

namespace SS_DB_API.Controllers
{
    public class CitiesController : ApiController
    {
        private ShareSmileDatabaseEntities db = new ShareSmileDatabaseEntities();

        // GET: api/Cities
        public List<DTO_City> GetCities()
        {
            var cities = db.Cities;

            List<DTO_City> dcities = new List<DTO_City>();
            
            foreach(City city in cities)
            {
                DTO_City dcity = new DTO_City();
                city.ConvertToDTO(dcity);
                dcities.Add(dcity);
            } 

            return dcities;
        }

        // GET: api/Cities/5
        [ResponseType(typeof(DTO_City))]
        public IHttpActionResult GetCity(int id)
        {
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return NotFound();
            }
            DTO_City dcity = new DTO_City();
            city.ConvertToDTO(dcity);

            return Ok(dcity);
        }

        // PUT: api/Cities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCity(int id, City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != city.City_Id)
            {
                return BadRequest();
            }

            db.Entry(city).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Cities
        [ResponseType(typeof(City))]
        public IHttpActionResult PostCity(City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cities.Add(city);
            db.SaveChanges();

            return CreatedAtRoute("route1", new { id = city.City_Id }, city);
        }

        // DELETE: api/Cities/5
        [ResponseType(typeof(City))]
        public IHttpActionResult DeleteCity(int id)
        {
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return NotFound();
            }

            db.Cities.Remove(city);
            db.SaveChanges();

            return Ok(city);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CityExists(int id)
        {
            return db.Cities.Count(e => e.City_Id == id) > 0;
        }

        [ResponseType(typeof(List<DTO_Area>))]
        public List<DTO_Area> GetAreaByCityId(int id)
        {
            List<DTO_Area> dareas = new List<DTO_Area>();

            List<Area> areas = db.Areas.Where(x => x.City_Id == id).ToList();

            foreach (Area area in areas)
            {
                DTO_Area darea = new DTO_Area();

                area.ConvertToDTO(darea);

                dareas.Add(darea);
            }

            return dareas;
        }
    }
}