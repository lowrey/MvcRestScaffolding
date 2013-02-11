using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcRestScaffoldingLib.Models
{
    public class StatusResponse
    {
        public StatusCode statusCode { get; set; }
        public string statusString { get; set; }

        public StatusResponse(StatusCode sc)
        {
            statusCode = sc;
            switch (sc)
            {
                case (StatusCode.Success):
                    statusString = "The operation has been completed successfully";
                    break;
                case (StatusCode.Failure):
                    statusString = "The operation did not complete successfully";
                    break;
                default:
                    statusString = "";
                    break;
            }
        }
    }

    public enum StatusCode
    {
        Success = 0,
        Failure,
        NotFound,
    }
}