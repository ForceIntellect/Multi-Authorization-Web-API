using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    class Response
    {
        public int StatusNo { get; set; }
        public string Message { get; set; }

        public object Data { get; set; }

    }
}
