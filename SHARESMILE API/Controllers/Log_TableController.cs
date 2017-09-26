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
    public class Log_TableController : ApiController
    {
        private ShareSmileDatabaseEntities db = new ShareSmileDatabaseEntities();

        // GET: api/Log_Table
        public List<DTO_Log_Table> GetLog_Table()
        {
            List<DTO_Log_Table> dtts = new List<DTO_Log_Table>();
            List<Log_Table> tts = db.Log_Table.ToList();

            foreach (Log_Table tt in tts)
            {
                DTO_Log_Table dtt = new DTO_Log_Table();
                tt.ConvertToDTO(dtt);

                dtts.Add(dtt);
            }

            return dtts;
        }

        // GET: api/Log_Table/5
        [ResponseType(typeof(DTO_Log_Table))]
        public IHttpActionResult GetLog_Table(int id)
        {
            Log_Table log_Table = db.Log_Table.Find(id);
            if (log_Table == null)
            {
                return NotFound();
            }
            DTO_Log_Table dlt = new DTO_Log_Table();
            log_Table.ConvertToDTO(dlt);

            return Ok(dlt);
        }

        // PUT: api/Log_Table/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLog_Table(int id, Log_Table log_Table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != log_Table.Log_Table_Id)
            {
                return BadRequest();
            }

            db.Entry(log_Table).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Log_TableExists(id))
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

        // POST: api/Log_Table
        [ResponseType(typeof(Log_Table))]
        public IHttpActionResult PostLog_Table(Log_Table log_Table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Log_Table.Add(log_Table);
            db.SaveChanges();

            return CreatedAtRoute("route1", new { id = log_Table.Log_Table_Id }, log_Table);
        }

        // DELETE: api/Log_Table/5
        [ResponseType(typeof(Log_Table))]
        public IHttpActionResult DeleteLog_Table(int id)
        {
            Log_Table log_Table = db.Log_Table.Find(id);
            if (log_Table == null)
            {
                return NotFound();
            }

            db.Log_Table.Remove(log_Table);
            db.SaveChanges();

            return Ok(log_Table);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Log_TableExists(int id)
        {
            return db.Log_Table.Count(e => e.Log_Table_Id == id) > 0;
        }
    }
}