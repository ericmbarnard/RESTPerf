using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using RESTPerf.Web.Models;

namespace RESTPerf.Web.Controllers
{
    public class ProductAsyncController : AsyncController
    {
        public void IndexAsync(Guid id)
        {
            AsyncManager.OutstandingOperations.Increment();

            Action asyncProductMaker = () =>
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

                AsyncManager.Parameters["p"] = p;
                AsyncManager.OutstandingOperations.Decrement();
            };

            asyncProductMaker.BeginInvoke(null, null);
        }

        public JsonResult IndexCompleted(Product p)
        {
            return Json(p);
        }
    }
}
