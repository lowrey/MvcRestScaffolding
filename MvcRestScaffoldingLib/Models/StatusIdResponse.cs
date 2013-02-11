using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel;

namespace MvcRestScaffoldingLib.Models
{
    public class StatusIdResponse : StatusResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(-1)]
        public long Id { get; set; }

        public StatusIdResponse(StatusCode sc, long affectedId = -1) : base(sc)
        {
            Id = affectedId;
        }
    }
}