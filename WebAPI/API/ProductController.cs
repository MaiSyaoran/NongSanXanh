using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NongSanAPI.Controllers
{
    public class ProductController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage LoadProduct(int selectcategory = 0)
        {
            using (NongSanXanhEntities entities = new NongSanXanhEntities())
            {
                switch (selectcategory.ToString())
                {
                    case "0":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.products.ToList());
                    case "9":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.products.Where(e => e.catid.ToString() == "9").ToList());
                    case "10":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.products.Where(e => e.catid.ToString() == "10").ToList());
                    case "4":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.products.Where(e => e.catid.ToString() == "4").ToList());
                    case "8":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.products.Where(e => e.catid.ToString() == "8").ToList());
                    case "3":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.products.Where(e => e.catid.ToString() == "3").ToList());
                    case "13":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.products.Where(e => e.catid.ToString() == "13").ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Value for Select must be CategoryID or All. " + selectcategory + " is invalid.");
                }
            }
        }
        /*====================================================================================================*/
        [HttpGet]
        public HttpResponseMessage LoadProductById(int id)
        {
            using (NongSanXanhEntities entities = new NongSanXanhEntities())
            {
                var entity = entities.products.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with Id " + id.ToString() + " not found");
                }
            }
        }
        /*====================================================================================================*/
        [HttpPost]
        public HttpResponseMessage AddProduct([FromBody] product product)
        {
            try
            {
                using (NongSanXanhEntities entities = new NongSanXanhEntities())
                {
                    entities.products.Add(product);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, product);
                    message.Headers.Location = new Uri(Request.RequestUri + product.ID.ToString());

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
        public HttpResponseMessage DeleteProduct(int id)
        {
            try
            {
                using (NongSanXanhEntities entities = new NongSanXanhEntities())
                {
                    var entity = entities.products.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.products.Remove(entity);
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
        public HttpResponseMessage EditProduct([FromBody] int id, [FromUri] product product)
        {
            try
            {
                using (NongSanXanhEntities entities = new NongSanXanhEntities())
                {
                    var entity = entities.products.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.catid = product.catid;
                        entity.Submenu = product.Submenu;
                        entity.name = product.name;
                        entity.slug = product.slug;
                        entity.img = product.img;
                        entity.detail = product.detail;
                        entity.number = product.number;
                        entity.price = product.price;
                        entity.pricesale = product.pricesale;
                        entity.metakey = product.metakey;
                        entity.metadesc = product.metadesc;
                        entity.created_at = product.created_at;
                        entity.created_by = product.created_by;
                        entity.updated_at = product.updated_at;
                        entity.updated_by = product.updated_by;
                        entity.status = product.status;
                        entity.sold = product.sold;

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
