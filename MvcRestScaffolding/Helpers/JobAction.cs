using System;
using System.Collections.Generic;
using System.Net;
using log4net;
using Newtonsoft.Json;
using MvcRestScaffolding.DAL;
using MvcRestScaffolding.Models;

namespace MvcRestScaffolding.Helpers
{
    public class JobAction
    {
        private IRepository<Job> repository = null;
        private ILog log;

        public JobAction()
        {
            repository = new JobRepository();
            log = LogManager.GetLogger(typeof(JobProcessor)); ;
        }

        public JobAction(IRepository<Job> repository)
        {
            this.repository = repository;
            log = LogManager.GetLogger(typeof(JobProcessor)); ;
        }

        public JobViewModel Get(long id)
        {
            return new JobViewModel(repository.Get(id));
        }

        public List<JobViewModel> Get()
        {
            var jobs = repository.FindAll();
            var list = new List<JobViewModel>();
            foreach (Job j in jobs)
                list.Add(new JobViewModel(j));
            return list;
        }

        public void Update(long id, string callbackUrl = "", string data = "", short status = -1 )
        {
            Job job = repository.Get(id);
            if (job != null)
            {
                job.CallbackUrl = string.IsNullOrEmpty(callbackUrl) ? job.CallbackUrl : callbackUrl;
                job.Data = string.IsNullOrEmpty(data) ? job.Data: data;
                job.Status = status == -1 ? job.Status : status;
                repository.Save();
                //if status has changed, send callback
                if (status != -1)
                {
                    try
                    {
                        SendCallback(new JobViewModel(job));
                    }
                    catch(Exception e)
                    {
                        log.Warn("Could not send callback: ", e);
                    }
                }
            }
            else
            {
                throw new KeyNotFoundException();
            } 
        }

        public void Update(JobViewModel editJob)
        {
            Job job = repository.Get(editJob.Id);
            if (job != null)
            {
                job.CallbackUrl = string.IsNullOrEmpty(editJob.CallbackUrl) ? job.CallbackUrl : editJob.CallbackUrl;
                job.Data = string.IsNullOrEmpty(editJob.Data) ? job.Data: editJob.Data;
                //Do not set status because status should only change internally
                //This action is only called by external facing methods
                repository.Save();
                //Callback is never issued in this update because status is never set
                //and can therefore never be changed
            }
            else
            {
                throw new KeyNotFoundException();
            } 
        }

        public HttpStatusCode SendCallback(JobViewModel job)
        {
            if (!Uri.IsWellFormedUriString(job.CallbackUrl, UriKind.Absolute))
                throw new UriFormatException();
            string jobSerialized = JsonConvert.SerializeObject(job);
            Callback callback = new Callback(job.CallbackUrl, jobSerialized);
            HttpStatusCode status = callback.Execute();
            log.DebugFormat("Sent callback: {0}", callback.ToString());
            return status; 
        }
    }
}