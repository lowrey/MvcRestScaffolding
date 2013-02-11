using Newtonsoft.Json;
using MvcRestScaffoldingLib.Models;
using RestSharp;

namespace MvcRestScaffoldingLib.Calls
{
    public class JobCreate : RestCall<StatusIdResponse>
    {
        private const string ACTION_PATH = "Job/Create";
        public JobCreate(string addr, string file, string filetype, string callback, Method method = Method.POST) :
            base(addr, ACTION_PATH, method, file:file)
        {
            //RequestContent = JsonConvert.SerializeObject(job); 
            var param = new Parameter { Name="callbackUrl", Value=callback, Type = ParameterType.GetOrPost};
            Parameters.Add(param);
            param = new Parameter { Name="filetype", Value=filetype, Type = ParameterType.GetOrPost};
            Parameters.Add(param);
        }
        /*public JobCreate(string addr, JobViewModel job, Method method = Method.POST) :
            base(addr, ACTION_PATH, method)
        {
            RequestContent = JsonConvert.SerializeObject(job); 
        }*/

        public override StatusIdResponse Execute()
        {
            string responseContent = GetResponseString();
            var response = JsonConvert.DeserializeObject<StatusIdResponse>(responseContent);
            return response;
        }
    }
}
