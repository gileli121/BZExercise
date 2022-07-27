using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceCommon.Models
{
    public class PipeData
    {
        public PipeData()
        {
        }

        public PipeData(Commands command, object payload)
        {
            this.CommandName = command;
            this.Payload = JsonSerializer.Serialize(payload);
        }

        public Commands CommandName { get; set; }
        public string Payload { get; set; }
    }
}
