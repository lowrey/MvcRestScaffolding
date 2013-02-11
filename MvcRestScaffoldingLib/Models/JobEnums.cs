using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcRestScaffoldingLib.Models
{
    public enum JobStatus
    {
        ToProcess,
        Processing,
        Processed,
        Completed,
        Failed
    }
}