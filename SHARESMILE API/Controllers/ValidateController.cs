using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SS_DB_API.Models;
using SS_DB_API.Custom_Models;
using System.Web.Http.Description;
using SS_DB_API.DTO_Models;

namespace SS_DB_API.Controllers
{
    public class ValidateController : ApiController
    {
        ShareSmileDatabaseEntities db = new ShareSmileDatabaseEntities();

        [ResponseType(typeof(object))]
        public IHttpActionResult GetLogin(string id,string id2)
        {
            Custom_Validation cval = new Custom_Validation();

            cval.Volunteers = db.Volunteers.ToList();
            cval.Users = db.Users.ToList();
            cval.Drivers = db.Drivers.ToList();

            var obj = cval.Login_Validation(id,id2);

            if (obj is Volunteer)
            {
                Volunteer vol = obj as Volunteer;
                DTO_Volunteer dto_vol = new DTO_Volunteer();
                vol.ConvertToDTO(dto_vol);
                return Ok(new { obj = dto_vol, type = "volunteer" });
            }
            else if (obj is User)
            {
                User vol = obj as User;
                DTO_User dto_user = new DTO_User();
                vol.ConvertToDTO(dto_user);
                return Ok(new { obj = dto_user, type = "user" });
            }
            else if (obj is Driver)
            {
                Driver dri = obj as Driver;
                DTO_Driver dto_dri = new DTO_Driver();
                dri.ConvertToDTO(dto_dri);
                return Ok(new { obj = dto_dri, type = "driver" });
            }
            else
                return Ok("Error");
        }
        [HttpGet]
        [ResponseType(typeof(bool))]
        public IHttpActionResult CheckMobileDuplicate(string id)
        {
            List<Volunteer> vols = db.Volunteers.ToList();
            List<User> users = db.Users.ToList();
            List<Driver> drivers = db.Drivers.ToList();
            List<NGO> ngos = db.NGOes.ToList();


            Predicate<Volunteer> pvols = x => x.Mobile_Number == id || x.Mobile_Alternate == id;
            Predicate<User> pusers= x => x.Mobile_Number == id || x.Mobile_Alternate == id;
            Predicate<Driver> pdrivers= x => x.Mobile_Number == id || x.Mobile_Alternate == id;
            Predicate<NGO> pngos= x => x.Mobile_Number == id || x.Mobile_Alternate == id;

            if (vols.Exists(x => pvols(x)) || users.Exists(x => pusers(x)) || drivers.Exists(x => pdrivers(x)) || ngos.Exists(x => pngos(x)))
                return Ok(true);
            else
                return Ok(false);
        }


        [HttpGet]
        [ResponseType(typeof(bool))]
        public IHttpActionResult CheckMobileDuplicateUserEdit(int id,string id2) //id is User_Id and id2 is Mobile Number
        {
            List<Volunteer> vols = db.Volunteers.ToList();
            List<User> users = db.Users.ToList();
            List<Driver> drivers = db.Drivers.ToList();
            List<NGO> ngos = db.NGOes.ToList();


            Predicate<Volunteer> pvols = x => x.Mobile_Number == id2 || x.Mobile_Alternate == id2;
            Predicate<User> pusers = x => (x.Mobile_Number == id2 || x.Mobile_Alternate == id2)&&(x.User_Id!= id);
            Predicate<Driver> pdrivers = x => x.Mobile_Number == id2 || x.Mobile_Alternate == id2;
            Predicate<NGO> pngos = x => x.Mobile_Number == id2 || x.Mobile_Alternate == id2;

            if (vols.Exists(x => pvols(x)) || users.Exists(x => pusers(x)) || drivers.Exists(x => pdrivers(x)) || ngos.Exists(x => pngos(x)))
                return Ok(true);
            else
                return Ok(false);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        public IHttpActionResult CheckMobileDuplicateVolunteerEdit(int id, string id2) //id is Volunteer_Id and id2 is Mobile Number
        {
            List<Volunteer> vols = db.Volunteers.ToList();
            List<User> users = db.Users.ToList();
            List<Driver> drivers = db.Drivers.ToList();
            List<NGO> ngos = db.NGOes.ToList();


            Predicate<Volunteer> pvols = x => (x.Mobile_Number == id2 || x.Mobile_Alternate == id2)&& (x.Volunteer_Id != id);
            Predicate<User> pusers = x => x.Mobile_Number == id2 || x.Mobile_Alternate == id2;
            Predicate<Driver> pdrivers = x => x.Mobile_Number == id2 || x.Mobile_Alternate == id2;
            Predicate<NGO> pngos = x => x.Mobile_Number == id2 || x.Mobile_Alternate == id2;

            if (vols.Exists(x => pvols(x)) || users.Exists(x => pusers(x)) || drivers.Exists(x => pdrivers(x)) || ngos.Exists(x => pngos(x)))
                return Ok(true);
            else
                return Ok(false);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        public IHttpActionResult CheckMobileDuplicateDriverEdit(int id, string id2) //id is Driver_Id and id2 is Mobile Number
        {
            List<Volunteer> vols = db.Volunteers.ToList();
            List<User> users = db.Users.ToList();
            List<Driver> drivers = db.Drivers.ToList();
            List<NGO> ngos = db.NGOes.ToList();


            Predicate<Volunteer> pvols = x => x.Mobile_Number == id2 || x.Mobile_Alternate == id2;
            Predicate<User> pusers = x => x.Mobile_Number == id2 || x.Mobile_Alternate == id2;
            Predicate<Driver> pdrivers = x => (x.Mobile_Number == id2 || x.Mobile_Alternate == id2)&&x.Driver_Id!=id;
            Predicate<NGO> pngos = x => x.Mobile_Number == id2 || x.Mobile_Alternate == id2;

            if (vols.Exists(x => pvols(x)) || users.Exists(x => pusers(x)) || drivers.Exists(x => pdrivers(x)) || ngos.Exists(x => pngos(x)))
                return Ok(true);
            else
                return Ok(false);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        public IHttpActionResult CheckEmailDuplicate(string id)
        {
            id += ".com";

            List<Volunteer> vols = db.Volunteers.ToList();
            List<User> users = db.Users.ToList();
            List<Driver> drivers = db.Drivers.ToList();
            List<NGO> ngos = db.NGOes.ToList();


            Predicate<Volunteer> pvols = x => x.Email == id;
            Predicate<User> pusers = x => x.Email == id;
            Predicate<Driver> pdrivers = x => x.Email == id;
            Predicate<NGO> pngos = x => x.Email == id;

            if (vols.Exists(x => pvols(x)) || users.Exists(x => pusers(x)) || drivers.Exists(x => pdrivers(x)) || ngos.Exists(x => pngos(x)))
                return Ok(true);
            else
                return Ok(false);

        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        public IHttpActionResult CheckEmailDuplicateEditUser(int id,string id2)
        {
            id2 += ".com";

            List<Volunteer> vols = db.Volunteers.ToList();
            List<User> users = db.Users.ToList();
            List<Driver> drivers = db.Drivers.ToList();
            List<NGO> ngos = db.NGOes.ToList();


            Predicate<Volunteer> pvols = x => x.Email == id2;
            Predicate<User> pusers = x => x.Email == id2 && x.User_Id != id;
            Predicate<Driver> pdrivers = x => x.Email == id2;
            Predicate<NGO> pngos = x => x.Email == id2;

            if (vols.Exists(x => pvols(x)) || users.Exists(x => pusers(x)) || drivers.Exists(x => pdrivers(x)) || ngos.Exists(x => pngos(x)))
                return Ok(true);
            else
                return Ok(false);

        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        public IHttpActionResult CheckEmailDuplicateEditVolunteer(int id, string id2)
        {
            id2 += ".com";

            List<Volunteer> vols = db.Volunteers.ToList();
            List<User> users = db.Users.ToList();
            List<Driver> drivers = db.Drivers.ToList();
            List<NGO> ngos = db.NGOes.ToList();


            Predicate<Volunteer> pvols = x => x.Email == id2 && x.Volunteer_Id != id;
            Predicate<User> pusers = x => x.Email == id2;
            Predicate<Driver> pdrivers = x => x.Email == id2;
            Predicate<NGO> pngos = x => x.Email == id2;

            if (vols.Exists(x => pvols(x)) || users.Exists(x => pusers(x)) || drivers.Exists(x => pdrivers(x)) || ngos.Exists(x => pngos(x)))
                return Ok(true);
            else
                return Ok(false);

        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        public IHttpActionResult CheckEmailDuplicateEditDriver(int id, string id2)
        {
            id2 += ".com";

            List<Volunteer> vols = db.Volunteers.ToList();
            List<User> users = db.Users.ToList();
            List<Driver> drivers = db.Drivers.ToList();
            List<NGO> ngos = db.NGOes.ToList();


            Predicate<Volunteer> pvols = x => x.Email == id2;
            Predicate<User> pusers = x => x.Email == id2;
            Predicate<Driver> pdrivers = x => x.Email == id2 && x.Driver_Id != id;
            Predicate<NGO> pngos = x => x.Email == id2;

            if (vols.Exists(x => pvols(x)) || users.Exists(x => pusers(x)) || drivers.Exists(x => pdrivers(x)) || ngos.Exists(x => pngos(x)))
                return Ok(true);
            else
                return Ok(false);

        }
    }
}
