using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWeb.Shared.Model
{
    public class EventsFileUpload
    {
        public string? FileName { get; set; }
        public byte[]? FileContent { get; set; }
        public string? FilePath { get; set; }
    }
}
