using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RESTPerf.Web.Models;

namespace RESTPerf.Web.Controllers
{
    public class ProductController : Controller
    {
        [HttpPost]
        public JsonResult Index(Guid id)
        {
            Product p = new Product()
            {
                ProductId = id,
                Sku = "MyTestSku",
                CreatedOn = DateTime.Now,
                TimeStamp = id.ToByteArray()
            };

            var goTo = DateTime.Now.AddMilliseconds(300);
            var time = DateTime.Now;
            while (time < goTo)
            {
                time = DateTime.Now;
            }
            return Json(p);
        }

    }
}
