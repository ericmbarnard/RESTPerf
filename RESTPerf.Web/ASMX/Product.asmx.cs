using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace RESTPerf.Web.ASMX
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Product : System.Web.Services.WebService
    {
        [WebMethod]
        public Models.Product GetById(Guid id)
        {
            Models.Product p = new Models.Product()
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

            return p;
        }
    }
}
