using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;

namespace MvcRestScaffolding.Helpers
{
    public class Callback
    {
        protected RestClient Client;
        protected Method ActionMethod;
        protected string ActionPath;
        protected string RequestContent;
        protected List<Parameter> Parameters;
        protected string File;

        public Callback(string addr, string requestContent, Method actionMethod = Method.POST, 
            List<Parameter> parameters = null, string file = "" )
        {
            Uri uri = new Uri(addr);
            Client = new RestClient(uri.GetLeftPart(UriPartial.Authority));
            ActionPath = uri.AbsolutePath;
            RequestContent = requestContent;
            ActionMethod = actionMethod;
            File = file;
            if (parameters != null)
                Parameters = parameters;
            else
                Parameters = new List<Parameter>();
        }

        protected RestRequest MakeRequest()
        {
            RestRequest request = new RestRequest(ActionPath, ActionMethod);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "*/*");
            request.RequestFormat = DataFormat.Json;
            foreach (var param in Parameters)
            {
                request.AddParameter(param);
            }
            if (!string.IsNullOrEmpty(RequestContent))
            {
                request.AddParameter("application/json", RequestContent, ParameterType.RequestBody);
            }
            if (File != "")
            {
                request.AddFile("file",File);
            }
            return request;
        }

        public IRestResponse GetResponse()
        {
            RestRequest request = MakeRequest();
            IRestResponse response = Client.Execute(request);
            return response;
        }
 
        public HttpStatusCode Execute()
        {
            IRestResponse response= GetResponse();
            return response.StatusCode;
        }

        public override string ToString()
        {
            return string.Format("action: {0}, content: {1}", ActionPath, RequestContent);
        }
    }
}
