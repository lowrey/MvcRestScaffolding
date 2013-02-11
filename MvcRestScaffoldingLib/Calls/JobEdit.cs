using Newtonsoft.Json;
using MvcRestScaffoldingLib.Models;
using RestSharp;

namespace MvcRestScaffoldingLib.Calls
{
    public class JobEdit : RestCall<StatusIdResponse>
    {
        private const string ACTION_PATH = "Job/Edit";

        public JobEdit(string addr, JobViewModel update, Method method = Method.PUT) :
            base(addr, ACTION_PATH, method)
        {
            RequestContent = JsonConvert.SerializeObject(update);
            //var param = new Parameter { Name = "id", Value = id, Type = ParameterType.UrlSegment };
            //var param2 = new Parameter { Name = "editJob", Value = data, Type = ParameterType.RequestBody };
            //Parameters.Add(param);
            //Parameters.Add(param2);
        }

        public override StatusIdResponse Execute()
        {
            string responseContent = GetResponseString();
            var response = JsonConvert.DeserializeObject<StatusIdResponse>(responseContent);
            return response;
        }
    }
}
