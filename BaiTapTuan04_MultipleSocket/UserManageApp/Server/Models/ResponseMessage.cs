using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Server.Models
{
    public class ResponseMessage
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public JsonElement? Data { get; set; }
    }
}
