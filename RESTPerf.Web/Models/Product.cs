using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTPerf.Web.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public String Sku { get; set; }
        public DateTime CreatedOn { get; set; }
        public byte[] TimeStamp { get; set; }
    }
}