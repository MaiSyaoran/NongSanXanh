using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NongSanAPI.Controllers
{
    public class PostController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage LoadPost(int selectcategory = 0)
        {
            using (NongSanXanhEntities entities = new NongSanXanhEntities())
            {
                switch (selectcategory.ToString())
                {
                    case "0":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.posts.ToList());
                    case "2":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.posts.Where(e => e.topid.ToString() == "2").ToList());
                    case "3":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.posts.Where(e => e.topid.ToString() == "3").ToList());
                    case "4":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.posts.Where(e => e.topid.ToString() == "4").ToList());
                    case "5":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.posts.Where(e => e.topid.ToString() == "5").ToList());
                    case "6":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.posts.Where(e => e.topid.ToString() == "6").ToList());
                    case "7":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.posts.Where(e => e.topid.ToString() == "7").ToList());
                    case "8":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.posts.Where(e => e.topid.ToString() == "8").ToList());
                    case "9":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.posts.Where(e => e.topid.ToString() == "9").ToList());
                    case "10":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.posts.Where(e => e.topid.ToString() == "10").ToList());
                    case "11":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.posts.Where(e => e.topid.ToString() == "11").ToList());
                    case "12":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.posts.Where(e => e.topid.ToString() == "12").ToList());
                    case "13":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.posts.Where(e => e.topid.ToString() == "13").ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Value for Select must be TopicID or All. " + selectcategory + " is invalid.");
                }
            }
        }
        /*====================================================================================================*/
        [HttpGet]
        public HttpResponseMessage LoadPostById(int id)
        {
            using (NongSanXanhEntities entities = new NongSanXanhEntities())
            {
                var entity = entities.posts.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Post with Id " + id.ToString() + " not found");
                }
            }
        }
        /*====================================================================================================*/
        [HttpPost]
        public HttpResponseMessage AddPost([FromBody] post post)
        {
            try
            {
                using (NongSanXanhEntities entities = new NongSanXanhEntities())
                {
                    entities.posts.Add(post);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, post);
                    message.Headers.Location = new Uri(Request.RequestUri + post.ID.ToString());

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
        public HttpResponseMessage DeletePost(int id)
        {
            try
            {
                using (NongSanXanhEntities entities = new NongSanXanhEntities())
                {
                    var entity = entities.posts.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Post with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.posts.Remove(entity);
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
        public HttpResponseMessage EditPost([FromBody] int id, [FromUri] post post)
        {
            try
            {
                using (NongSanXanhEntities entities = new NongSanXanhEntities())
                {
                    var entity = entities.posts.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Post with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.topid = post.topid;
                        entity.title = post.title;
                        entity.slug = post.slug;
                        entity.detail = post.detail;
                        entity.img = post.img;
                        entity.type = post.type;
                        entity.metakey = post.metakey;
                        entity.metadesc = post.metadesc;
                        entity.created_at = post.created_at;
                        entity.created_by = post.created_by;
                        entity.updated_at = post.updated_at;
                        entity.updated_by = post.updated_by;
                        entity.status = post.status;

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
