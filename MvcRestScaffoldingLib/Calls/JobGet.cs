using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;
using Newtonsoft.Json;
using MvcRestScaffoldingLib.Models;

namespace MvcRestScaffoldingLib.Calls
{
    public class JobGet : RestCall<JobViewModel>
    {
        private const string ACTION_PATH = "Job/Details";
        public JobGet(string addr, long id, Method method = Method.GET) :
            base(addr, ACTION_PATH, method)
        {
            var param = new Parameter { Name="id", Value=id, Type = ParameterType.GetOrPost};
            Parameters.Add(param);
        }

        public override JobViewModel Execute()
        {
            string responseContent = GetResponseString();
            var response = JsonConvert.DeserializeObject<JobViewModel>(responseContent);
            return response;
        }
    }
}
