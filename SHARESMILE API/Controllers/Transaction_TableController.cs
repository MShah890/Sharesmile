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
    public class Transaction_TableController : ApiController
    {
        private ShareSmileDatabaseEntities db = new ShareSmileDatabaseEntities();

        // GET: api/Transaction_Table
        public List<DTO_Transaction_Table> GetTransaction_TableVolunteers()
        {
            List<DTO_Transaction_Table> dtts = new List<DTO_Transaction_Table>();
            List<Transaction_Table> tts = db.Transaction_Table.Where(x=>x.Status=="Volunteer Requested").ToList();

            foreach(Transaction_Table tt in tts)
            {
                DTO_Transaction_Table dtt = new DTO_Transaction_Table();
                tt.ConvertToDTO(dtt);

                if (tt.User_Id != null)
                {
                    User user = db.Users.Where(x => x.User_Id == dtt.User_Id).Single();
                    dtt.User_Name = user.F_Name + " " + user.L_Name;
                    dtt.User_Mobile_Number = user.Mobile_Number;
                }
                if (tt.Volunteer_Id != null)
                {
                    Volunteer volunteer = db.Volunteers.Where(x => x.Volunteer_Id == dtt.Volunteer_Id).Single();
                    dtt.Volunteer_Name = volunteer.F_Name + " " + volunteer.L_Name;
                    dtt.Volunteer_Mobile = volunteer.Mobile_Number;
                }
                if (tt.Driver_Id != null)
                {
                    Driver driver = db.Drivers.Where(x => x.Driver_Id == dtt.Driver_Id).Single();
                    dtt.Driver_Name = driver.F_Name + " " + driver.L_Name;
                    dtt.Driver_Mobile = driver.Mobile_Number;
                }
                dtts.Add(dtt); 
            }

            return dtts;
        }

        public List<DTO_Transaction_Table> GetTransaction_TableDrivers()
        {
            List<DTO_Transaction_Table> dtts = new List<DTO_Transaction_Table>();
            List<Transaction_Table> tts = db.Transaction_Table.Where(x => x.Status == "Driver Requested").ToList();

            foreach (Transaction_Table tt in tts)
            {
                DTO_Transaction_Table dtt = new DTO_Transaction_Table();
                tt.ConvertToDTO(dtt);

                if (tt.User_Id != null)
                {
                    User user = db.Users.Where(x => x.User_Id == dtt.User_Id).Single();
                    dtt.User_Name = user.F_Name + " " + user.L_Name;
                    dtt.User_Mobile_Number = user.Mobile_Number;
                }
                if (tt.Volunteer_Id != null)
                {
                    Volunteer volunteer = db.Volunteers.Where(x => x.Volunteer_Id == dtt.Volunteer_Id).Single();
                    dtt.Volunteer_Name = volunteer.F_Name + " " + volunteer.L_Name;
                    dtt.Volunteer_Mobile = volunteer.Mobile_Number;
                }
                if (tt.Driver_Id != null)
                {
                    Driver driver = db.Drivers.Where(x => x.Driver_Id == dtt.Driver_Id).Single();
                    dtt.Driver_Name = driver.F_Name + " " + driver.L_Name;
                    dtt.Driver_Mobile = driver.Mobile_Number;
                }
                dtts.Add(dtt);
            }

            return dtts;
        }

        public List<DTO_Transaction_Table> GetTransaction_Table()
        {
            List<DTO_Transaction_Table> dtts = new List<DTO_Transaction_Table>();
            List<Transaction_Table> tts = db.Transaction_Table.ToList();

            foreach (Transaction_Table tt in tts)
            {
                DTO_Transaction_Table dtt = new DTO_Transaction_Table();
                tt.ConvertToDTO(dtt);

                if (tt.User_Id != null)
                {
                    User user = db.Users.Where(x => x.User_Id == dtt.User_Id).Single();
                    dtt.User_Name = user.F_Name + " " + user.L_Name;
                    dtt.User_Mobile_Number = user.Mobile_Number;
                }
                if (tt.Volunteer_Id != null)
                {
                    Volunteer volunteer = db.Volunteers.Where(x => x.Volunteer_Id == dtt.Volunteer_Id).Single();
                    dtt.Volunteer_Name = volunteer.F_Name + " " + volunteer.L_Name;
                    dtt.Volunteer_Mobile = volunteer.Mobile_Number;
                }
                if (tt.Driver_Id != null)
                {
                    Driver driver = db.Drivers.Where(x => x.Driver_Id == dtt.Driver_Id).Single();
                    dtt.Driver_Name = driver.F_Name + " " + driver.L_Name;
                    dtt.Driver_Mobile = driver.Mobile_Number;
                }
                dtts.Add(dtt);
            }

            return dtts;
        }

        // GET: api/Transaction_Table/5
        [ResponseType(typeof(Transaction_Table))]
        public IHttpActionResult GetTransaction_Table(int id)
        {
            Transaction_Table transaction_Table = db.Transaction_Table.Find(id);

            if (transaction_Table == null)
            {
                return NotFound();
            }

            DTO_Transaction_Table dtt = new DTO_Transaction_Table();
            transaction_Table.ConvertToDTO(dtt);

            if(transaction_Table.User_Id!=null)
            {
                User user = db.Users.Where(x => x.User_Id == dtt.User_Id).Single();
                dtt.User_Name = user.F_Name + " " + user.L_Name;
                dtt.User_Mobile_Number = user.Mobile_Number;
            }
            if(transaction_Table.Volunteer_Id!=null)
            {
                Volunteer volunteer = db.Volunteers.Where(x => x.Volunteer_Id == dtt.Volunteer_Id).Single();
                dtt.Volunteer_Name = volunteer.F_Name + " " + volunteer.L_Name;
                dtt.Volunteer_Mobile = volunteer.Mobile_Number;
            }
            if (transaction_Table.Driver_Id!=null)
            {
                Driver driver = db.Drivers.Where(x => x.Driver_Id == dtt.Driver_Id).Single();
                dtt.Driver_Name = driver.F_Name + " " + driver.L_Name;
                dtt.Driver_Mobile = driver.Mobile_Number;
            }
            return Ok(dtt);
        }

        // PUT: api/Transaction_Table/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTransaction_Table(int id, Transaction_Table transaction_Table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transaction_Table.Transaction_Id)
            {
                return BadRequest();
            }

            //List<Transaction_Table> list = db.Transaction_Table.ToList();

            //if (list.Exists(x => x.Transaction_Id == transaction_Table.Transaction_Id && x.Status != "Active"))
            //{
                db.Entry(transaction_Table).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Transaction_TableExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            //}
            return StatusCode(HttpStatusCode.NoContent);

        }

        // POST: api/Transaction_Table
        [ResponseType(typeof(Transaction_Table))]
        public IHttpActionResult PostTransaction_Table(Transaction_Table transaction_Table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Transaction_Table.Add(transaction_Table);
            db.SaveChanges();

            return CreatedAtRoute("route1", new { id = transaction_Table.Transaction_Id }, transaction_Table);
        }

        // DELETE: api/Transaction_Table/5
        [ResponseType(typeof(Transaction_Table))]
        public IHttpActionResult DeleteTransaction_Table(int id)
        {
            Transaction_Table transaction_Table = db.Transaction_Table.Find(id);
            if (transaction_Table == null)
            {
                return NotFound();
            }

            db.Transaction_Table.Remove(transaction_Table);
            db.SaveChanges();

            return Ok(transaction_Table);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Transaction_TableExists(int id)
        {
            return db.Transaction_Table.Count(e => e.Transaction_Id == id) > 0;
        }

        [ResponseType(typeof(Transaction_Table))]
        public IHttpActionResult GetTransaction_TableByUser(int id)
        {
            Transaction_Table transaction_Table = db.Transaction_Table.Where(x => x.User_Id == id && (x.Status == "Volunteer Requested"||x.Status=="Active"|| x.Status=="Driver Requested")).FirstOrDefault();

            if (transaction_Table == null)
            {
                return NotFound();
            }

            DTO_Transaction_Table dtt = new DTO_Transaction_Table();
            transaction_Table.ConvertToDTO(dtt);


            if (transaction_Table.User_Id != null)
            {
                User user = db.Users.Where(x => x.User_Id == dtt.User_Id).Single();
                dtt.User_Name = user.F_Name + " " + user.L_Name;
                dtt.User_Mobile_Number = user.Mobile_Number;
            }
            if (transaction_Table.Volunteer_Id != null)
            {
                Volunteer volunteer = db.Volunteers.Where(x => x.Volunteer_Id == dtt.Volunteer_Id).Single();
                dtt.Volunteer_Name = volunteer.F_Name + " " + volunteer.L_Name;
                dtt.Volunteer_Mobile = volunteer.Mobile_Number;
            }
            if (transaction_Table.Driver_Id != null)
            {
                Driver driver = db.Drivers.Where(x => x.Driver_Id == dtt.Driver_Id).Single();
                dtt.Driver_Name = driver.F_Name + " " + driver.L_Name;
                dtt.Driver_Mobile = driver.Mobile_Number;
            }
            return Ok(dtt);
        }        
    }
}