using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWeb.Shared.Model
{
    public class SuccessMessage
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }    
    }
}
