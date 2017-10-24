using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWebApi2
{
    public class DataResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public object Data;
    }
}