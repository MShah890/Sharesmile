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
    public class AreasController : ApiController
    {
        private ShareSmileDatabaseEntities db = new ShareSmileDatabaseEntities();

        // GET: api/Areas
        public List<DTO_Area> GetAreas()
        {
            var dareas = new List<DTO_Area>();

            var areas = db.Areas;

            foreach(Area area in areas)
            {
                DTO_Area darea = new DTO_Area();
                area.ConvertToDTO(darea);
                dareas.Add(darea);   
            }

            return dareas;
        }

        // GET: api/Areas/5
        [ResponseType(typeof(DTO_Area))]
        public IHttpActionResult GetArea(int id)
        {
            Area area = db.Areas.Find(id);
            if (area == null)
            {
                return NotFound();
            }
            DTO_Area darea = new DTO_Area();

            area.ConvertToDTO(darea);

            return Ok(darea);
        }

        // PUT: api/Areas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArea(int id, Area area)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != area.Area_Id)
            {
                return BadRequest();
            }

            db.Entry(area).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaExists(id))
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

        // POST: api/Areas
        [ResponseType(typeof(Area))]
        public IHttpActionResult PostArea(Area area)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Areas.Add(area);
            db.SaveChanges();

            return CreatedAtRoute("route1", new { id = area.Area_Id }, area);
        }

        // DELETE: api/Areas/5
        [ResponseType(typeof(Area))]
        public IHttpActionResult DeleteArea(int id)
        {
            Area area = db.Areas.Find(id);
            if (area == null)
            {
                return NotFound();
            }

            db.Areas.Remove(area);
            db.SaveChanges();

            return Ok(area);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AreaExists(int id)
        {
            return db.Areas.Count(e => e.Area_Id == id) > 0;
        }
    }
}