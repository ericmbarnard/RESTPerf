using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using RESTPerf.Web.Models;

namespace RESTPerf.Web.WCFService
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ProductAsyncService
    {
        [WebInvoke(UriTemplate = "/{id}", Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract(AsyncPattern = true)]
        public IAsyncResult BeginGetProduct(string id, AsyncCallback asyncCallBack, object asyncState)
        {
            Guid gId = Guid.Parse(id);

            ProductAsyncResult asyncResult = new ProductAsyncResult();
            asyncResult.AsyncState = asyncState;
            
            Action asyncProductMaker = () =>
            {
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

                asyncResult.Data = p;
                asyncResult.IsCompleted = true;
                asyncCallBack(asyncResult);
            };

            //Kick the method off asynchronously
            asyncProductMaker.BeginInvoke(null, null);
            return asyncResult;
        }

        public Product EndGetProduct(IAsyncResult result)
        {
            ProductAsyncResult pResult = (ProductAsyncResult)result;
            return pResult.Data;
        }

        public class ProductAsyncResult : IAsyncResult
        {

            public Product Data
            {
                get;
                set;
            }

            public object AsyncState
            {
                get;
                set;
            }

            public System.Threading.WaitHandle AsyncWaitHandle
            {
                get;
                set;
            }

            public bool CompletedSynchronously
            {
                get { return false; }
            }

            public bool IsCompleted
            {
                get;
                set;
            }
        }
    }
}