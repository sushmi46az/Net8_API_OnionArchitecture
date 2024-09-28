using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace ExtModule.API.Core.ERP
{
    public  class TestAPI
    {
       

        
        public class loginRes
        {
            public List<Data> data { get; set; }

            public int result { get; set; }

            public string message { get; set; }

            public string url { get; set; }
        }
        public class PostResponse
        {
            public string url { get; set; }

            public List<Hashtable> data { get; set; }

            public int result { get; set; }

            public string message { get; set; }
        }
        public class Data
        {
            public int AltLanguageId { get; set; }

            public int EmployeeId { get; set; }

            public string fSessionId { get; set; }
        }
        public class PostingData
        {
            public List<Hashtable> data { get; set; }

            public PostingData()
            {
                data = new List<Hashtable>();
            }
        }
    }
    public class FocusPostResponse<T>
    {
        public int status { get; set; }
        public string sMessage { get; set; }
        public T F8APIPost { get; set; }

    }

}
