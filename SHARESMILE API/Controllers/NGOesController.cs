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
    public class NGOesController : ApiController
    {
        private ShareSmileDatabaseEntities db = new ShareSmileDatabaseEntities();

        // GET: api/NGOes
        public List<DTO_NGO> GetNGOes()
        {
            var dngoes = new List<DTO_NGO>();

            var ngoes = db.NGOes;

            foreach (NGO ngo in ngoes)
            {
                DTO_NGO dngo = new DTO_NGO();
                ngo.ConvertToDTO(dngo);
                dngoes.Add(dngo);
            }

            return dngoes;
        }

        // GET: api/NGOes/5
        [ResponseType(typeof(DTO_NGO))]
        public IHttpActionResult GetNGO(int id)
        {
            NGO ngo = db.NGOes.Find(id);
            if (ngo == null)
            {
                return NotFound();
            }
            DTO_NGO dngo = new DTO_NGO();

            ngo.ConvertToDTO(dngo);
            return Ok(dngo);
        }

        // PUT: api/NGOes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNGO(int id, NGO nGO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nGO.NGO_Id)
            {
                return BadRequest();
            }

            db.Entry(nGO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NGOExists(id))
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

        // POST: api/NGOes
        [ResponseType(typeof(NGO))]
        public IHttpActionResult PostNGO(NGO nGO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NGOes.Add(nGO);
            db.SaveChanges();

            return CreatedAtRoute("route1", new { id = nGO.NGO_Id }, nGO);
        }

        // DELETE: api/NGOes/5
        [ResponseType(typeof(NGO))]
        public IHttpActionResult DeleteNGO(int id)
        {
            NGO nGO = db.NGOes.Find(id);
            if (nGO == null)
            {
                return NotFound();
            }

            db.NGOes.Remove(nGO);
            db.SaveChanges();

            return Ok(nGO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NGOExists(int id)
        {
            return db.NGOes.Count(e => e.NGO_Id == id) > 0;
        }
    }
}