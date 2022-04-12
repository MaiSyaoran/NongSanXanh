using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.API
{
    public class SlideController : ApiController
    {
        [HttpGet]
        public IEnumerable<slider> LoadSlide()
        {
            using (NongSanXanhEntities entities = new NongSanXanhEntities())
            {
                return entities.sliders.ToList();
            }
        }
    }
}
