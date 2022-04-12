using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Xml.Linq;

namespace NongSanAPI.Controllers
{
    public class UserController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage LoadUser(int selectgruop = 4)
        {
            using (NongSanXanhEntities entities = new NongSanXanhEntities())
            {
                switch (selectgruop.ToString())
                {
                    case "4":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.users.ToList());
                    case "0":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.users.Where(e => e.access.ToString() == "0").ToList());
                    case "1":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.users.Where(e => e.access.ToString() == "1").ToList());
                    case "2":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.users.Where(e => e.access.ToString() == "2").ToList());
                    case "3":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.users.Where(e => e.access.ToString() == "3").ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Value for Select must be ADMIN, CUSTOMER, ACCOUNTANT, SALESMAN, True, False or All. " + selectgruop + " is invalid.");
                }
            }
        }
        /*====================================================================================================*/
        [HttpGet]
        public HttpResponseMessage LoadUserById(int id)
        {
            using (NongSanXanhEntities entities = new NongSanXanhEntities())
            {
                var entity = entities.users.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with Id " + id.ToString() + " not found");
                }
            }
        }
        /*====================================================================================================*/
        [HttpPost]
        public HttpResponseMessage AddUser([FromBody] user user)
        {
            try
            {
                using (NongSanXanhEntities entities = new NongSanXanhEntities())
                {
                    entities.users.Add(user);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, user);
                    message.Headers.Location = new Uri(Request.RequestUri + user.ID.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        /*====================================================================================================*/
        [HttpDelete]
        public HttpResponseMessage DeleteUser(int id)
        {
            try
            {
                using (NongSanXanhEntities entities = new NongSanXanhEntities())
                {
                    var entity = entities.users.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.users.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        /*====================================================================================================*/
        [HttpPut]
        public HttpResponseMessage EditUser([FromBody] int id, [FromUri] user user)
        {
            try
            {
                using (NongSanXanhEntities entities = new NongSanXanhEntities())
                {
                    var entity = entities.users.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.fullname = user.fullname;
                        entity.username = user.username;
                        entity.password = user.password;
                        entity.email = user.email;
                        entity.gender = user.gender;
                        entity.phone = user.phone;
                        entity.img = user.img;
                        entity.access = user.access;
                        entity.created_at = user.created_at;
                        entity.created_by = user.created_by;
                        entity.updated_at = user.updated_at;
                        entity.updated_by = user.updated_by;
                        entity.status = user.status;

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        /*====================================================================================================*/
        
    }
}
