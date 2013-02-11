using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;
using System.IO;

namespace MvcRestScaffoldingLib.Calls
{
    public abstract class RestCall<T>
    {
        protected RestClient Client;
        protected Method ActionMethod;
        protected string ActionPath;
        protected string RequestContent;
        protected List<Parameter> Parameters;
        protected string File;

        protected RestCall(string addr, string actionPath, Method actionMethod = Method.GET, 
            List<Parameter> parameters = null, string file = "", string requestContent = "")
        {
            Client = new RestClient(addr);
            ActionPath = actionPath;
            RequestContent = requestContent;
            ActionMethod = actionMethod;
            File = file;
            if (parameters != null)
                Parameters = parameters;
            else
                Parameters = new List<Parameter>();
        }

        public string GetResponseString()
        {
            RestRequest request = MakeRequest();
            IRestResponse response = Client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var statusDesc = "Error connecting to server. " + response.StatusDescription;
                //todo, may need to provide different exceptions based on statusCode for localization.
                var exception = response.ErrorException;
                if (exception == null)
                {
                    exception = new Exception(statusDesc); 
                }

                //var msg = (response.ErrorMessage != null && response.ErrorMessage.Length > 0) ? response.ErrorMessage : statusDesc;
                var msg = response.Content;
                throw new CallException(String.Format("{0}", msg), exception);
            }
            return response.Content;
        }

        public byte[] GetResponseRaw()
        {
            RestRequest request = MakeRequest();
            var response = Client.Execute(request);
            return response.RawBytes;
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

        public abstract T Execute();
    }

}
