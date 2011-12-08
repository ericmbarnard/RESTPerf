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
    public class ProductAsync : System.Web.Services.WebService
    {
        [WebMethod]
        public IAsyncResult BeginGetProductById(Guid id, AsyncCallback asyncCallBack, object asyncState)
        {
            ProductAsyncResult asyncResult = new ProductAsyncResult();
            asyncResult.AsyncState = asyncState;

            Action asyncProductMaker = () =>
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

                asyncResult.Data = p;
                asyncResult.IsCompleted = true;
                asyncCallBack(asyncResult);
            };

            //Kick the method off asynchronously
            asyncProductMaker.BeginInvoke(null, null);
            return asyncResult;
        }

        [WebMethod]
        public Models.Product EndGetProductById(IAsyncResult result)
        {
            ProductAsyncResult pResult = (ProductAsyncResult)result;
            return pResult.Data;
        }

        public class ProductAsyncResult : IAsyncResult
        {

            public Models.Product Data
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
