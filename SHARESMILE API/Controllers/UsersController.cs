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
    public class UsersController : ApiController
    {
        private ShareSmileDatabaseEntities db = new ShareSmileDatabaseEntities();

        // GET: api/Users
        public IList<DTO_User> GetUsers()
        {
            var dusers = new List<DTO_User>();

            var users = db.Users;

            foreach (User user in users)
            {
                DTO_User duser = new DTO_User();
                user.ConvertToDTO(duser);
                dusers.Add(duser);
            }

            return dusers;
        }

        // GET: api/Users/5
        [ResponseType(typeof(DTO_User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            DTO_User duser = new DTO_User();
            user.ConvertToDTO(duser);

            return Ok(duser);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.User_Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("route1", new { id = user.User_Id }, user);
        }
    
        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.User_Id == id) > 0;
        }
    }
}