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
    public class StatesController : ApiController
    {
        private ShareSmileDatabaseEntities db = new ShareSmileDatabaseEntities();

        // GET: api/States
        public List<DTO_State> GetStates()
        {
            var states = db.States;

            var dstates = new List<DTO_State>();

            foreach(State state in states)
            {
                DTO_State dstate = new DTO_State();
                state.ConvertToDTO(dstate);
                dstates.Add(dstate);
            }
            return dstates;
        }

        // GET: api/States/5
        [ResponseType(typeof(DTO_State))]
        public IHttpActionResult GetState(int id)
        {
            State state = db.States.Find(id);

            if (state == null)
            {
                return NotFound();
            }

            DTO_State dstate = new DTO_State() { State_Id = state.State_Id, State_Name = state.State_Name };

            return Ok(dstate);
        }

        // PUT: api/States/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutState(int id, State state)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != state.State_Id)
            {
                return BadRequest();
            }

            db.Entry(state).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateExists(id))
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

        // POST: api/States
        [ResponseType(typeof(State))]
        public IHttpActionResult PostState(State state)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.States.Add(state);
            db.SaveChanges();

            return CreatedAtRoute("route1", new { id = state.State_Id }, state);
        }

        // DELETE: api/States/5
        [ResponseType(typeof(State))]
        public IHttpActionResult DeleteState(int id)
        {
            State state = db.States.Find(id);
            if (state == null)
            {
                return NotFound();
            }

            db.States.Remove(state);
            db.SaveChanges();

            return Ok(state);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StateExists(int id)
        {
            return db.States.Count(e => e.State_Id == id) > 0;
        }

        [ResponseType(typeof(List<DTO_City>))]
        public List<DTO_City> GetCityByStateId(int id)
        {
            List<DTO_City> dcities = new List<DTO_City>();

            List<City> cities = db.Cities.Where(x => x.State_Id == id).ToList();

            foreach(City city in cities)
            {
                DTO_City dcity = new DTO_City();

                city.ConvertToDTO(dcity);

                dcities.Add(dcity);
            }

            return dcities;
        }
    }
}