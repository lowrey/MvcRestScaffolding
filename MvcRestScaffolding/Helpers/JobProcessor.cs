using System;
using System.IO;
using System.Threading;
using log4net;
using MvcRestScaffolding.Models;

namespace MvcRestScaffolding.Helpers
{
    public class JobProcessor
    {
        private long id;
        private ILog log;
        public JobProcessor(long id)
        {
            this.id = id;
            log = LogManager.GetLogger(typeof(JobProcessor)); ;
        }

        public void StartProcessing()
        {
            Thread thread1 = new Thread(new ThreadStart(DoWork));
            thread1.Start();
        }

        private void DoWork()
        {
            JobAction action = new JobAction();
            JobViewModel job = action.Get(id);
            //Set status to processing
            job.Status = JobStatus.Processing;
            action.Update(job.Id, status:(short)job.Status);
            log.DebugFormat("Processing job: {0}", job.ToString());

            //Do processing

            //When finished, set to processed (or failed)
            job.Status = JobStatus.Processed;
            action.Update(job.Id, status:(short)job.Status);
            log.DebugFormat("Job processed: {0}", job.ToString());
        } 
    }
}