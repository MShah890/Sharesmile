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
    public class VolunteersController : ApiController
    {
        private ShareSmileDatabaseEntities db = new ShareSmileDatabaseEntities();

        // GET: api/Volunteers
        public IList<DTO_Volunteer> GetVolunteers()
        {
            var dvolunteers = new List<DTO_Volunteer>();

            var volunteers = db.Volunteers;

            foreach (Volunteer volunteer in volunteers)
            {
                DTO_Volunteer dvolunteer = new DTO_Volunteer();
                volunteer.ConvertToDTO(dvolunteer);
                dvolunteers.Add(dvolunteer);
            }

            return dvolunteers;
        }

        // GET: api/Volunteers/5
        [ResponseType(typeof(DTO_Volunteer))]
        public IHttpActionResult GetVolunteer(int id)
        {
            Volunteer volunteer = db.Volunteers.Find(id);
            if (volunteer == null)
            {
                return NotFound();
            }
            DTO_Volunteer dvolunteer = new DTO_Volunteer();
            volunteer.ConvertToDTO(dvolunteer);

            return Ok(dvolunteer);
        }

        // PUT: api/Volunteers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVolunteer(int id, Volunteer volunteer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != volunteer.Volunteer_Id)
            {
                return BadRequest();
            }

            db.Entry(volunteer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolunteerExists(id))
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

        // POST: api/Volunteers
        [ResponseType(typeof(Volunteer))]
        public IHttpActionResult PostVolunteer(Volunteer volunteer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Volunteers.Add(volunteer);
            db.SaveChanges();

            return CreatedAtRoute("route1", new { id = volunteer.Volunteer_Id }, volunteer);
        }

        // DELETE: api/Volunteers/5
        [ResponseType(typeof(Volunteer))]
        public IHttpActionResult DeleteVolunteer(int id)
        {
            Volunteer volunteer = db.Volunteers.Find(id);
            if (volunteer == null)
            {
                return NotFound();
            }

            db.Volunteers.Remove(volunteer);
            db.SaveChanges();

            return Ok(volunteer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VolunteerExists(int id)
        {
            return db.Volunteers.Count(e => e.Volunteer_Id == id) > 0;
        }
    }
}