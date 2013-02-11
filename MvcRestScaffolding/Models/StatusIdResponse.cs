using System.ComponentModel;
using Newtonsoft.Json;

namespace MvcRestScaffolding.Models
{
    public class StatusIdResponse : StatusResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(-1)]
        public long AffectedId { get; set; }

        public StatusIdResponse(StatusCode sc, long affectedId = -1) : base(sc)
        {
            AffectedId = affectedId;
        }
    }
}