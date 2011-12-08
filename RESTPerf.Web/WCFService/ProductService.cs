using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using RESTPerf.Web.Models;

namespace RESTPerf.Web.WCFService
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ProductService
    {
        [WebInvoke(UriTemplate = "/{id}", Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        public Product GetProduct(string id)
        {
            Guid gId = Guid.Parse(id);
            Product p = new Product()
            {
                ProductId = gId,
                Sku = "MyTestSku",
                CreatedOn = DateTime.Now,
                TimeStamp = gId.ToByteArray()
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
