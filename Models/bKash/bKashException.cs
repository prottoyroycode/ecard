using System;
using System.Collections.Generic;
using System.Text;

namespace Models.bKash
{
   public  class bKashException
    {

        public string status { get; set; }
        public string errorCode { get; set; }
        public string reason { get; set; }
        public string timeStamp { get; set; }
        public string message { get; set; }
        public string path { get; set; }
        public string error { get; set; }
    }
}
