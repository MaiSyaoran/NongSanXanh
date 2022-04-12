using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NongSanAPI.Controllers
{
    public class CartController : ApiController
    {
        [HttpGet]
        public IEnumerable<order> LoadCart()
        {
            using (NongSanXanhEntities entities = new NongSanXanhEntities())
            {
                return entities.orders.ToList();
            }
        }
        /*====================================================================================================*/
        [HttpGet]
        public HttpResponseMessage LoadCartById(int id)
        {
            using (NongSanXanhEntities entities = new NongSanXanhEntities())
            {
                var entity = entities.orders.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Order with Id " + id.ToString() + " not found");
                }
            }
        }
        /*====================================================================================================*/
        [HttpPost]
        public HttpResponseMessage AddCart([FromBody] order order)
        {
            try
            {
                using (NongSanXanhEntities entities = new NongSanXanhEntities())
                {
                    entities.orders.Add(order);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, order);
                    message.Headers.Location = new Uri(Request.RequestUri + order.ID.ToString());

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
        public HttpResponseMessage DeleteCart(int id)
        {
            try
            {
                using (NongSanXanhEntities entities = new NongSanXanhEntities())
                {
                    var entity = entities.orders.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Order with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.orders.Remove(entity);
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
        public HttpResponseMessage EditCart([FromBody] int id, [FromUri] order order)
        {
            try
            {
                using (NongSanXanhEntities entities = new NongSanXanhEntities())
                {
                    var entity = entities.orders.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Order with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.code = order.code;
                        entity.userid = order.userid;
                        entity.created_ate = order.created_ate;
                        entity.exportdate = order.exportdate;
                        entity.deliveryaddress = order.deliveryaddress;
                        entity.deliveryname = order.deliveryname;
                        entity.deliveryphone = order.deliveryphone;
                        entity.deliveryemail = order.deliveryemail;
                        entity.updated_at = order.updated_at;
                        entity.updated_by = order.updated_by;
                        entity.status = order.status;

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
