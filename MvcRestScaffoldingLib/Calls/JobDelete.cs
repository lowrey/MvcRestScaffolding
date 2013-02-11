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
    public class JobDelete : RestCall<StatusResponse>
    {
        private const string ACTION_PATH = "Job/Delete";

        public JobDelete(string addr, long id, Method method = Method.DELETE) :
            base(addr, ACTION_PATH, method)
        {
            var param = new Parameter { Name="id", Value=id, Type= ParameterType.GetOrPost };
            Parameters.Add(param);
        }

        public override StatusResponse Execute()
        {
            string responseContent = GetResponseString();
            var response = JsonConvert.DeserializeObject<StatusResponse>(responseContent);
            return response;
        }
    }
}
