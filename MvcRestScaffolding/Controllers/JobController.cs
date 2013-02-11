using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using log4net;
using MvcRestScaffolding.DAL;
using MvcRestScaffolding.Helpers;
using MvcRestScaffolding.Models;

namespace MvcRestScaffolding.Controllers
{
    public class JobController : Controller
    {
        private IRepository<Job> repository = null;
        private ILog log;

        public JobController()
        {
            repository = new JobRepository();
            log = LogManager.GetLogger(this.GetType());
        }

        public JobController(IRepository<Job> repository)
        {
            this.repository = repository;
        }
        //
        // GET: /Job/

        public ActionResult Index()
        {
            log.Debug("In Jobs/Index");
            var jobs = repository.FindAll();
            var list = new List<JobViewModel>();
            foreach (Job j in jobs)
                list.Add(new JobViewModel(j));
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Job/Details/5

        public ActionResult Details(long id)
        {
            log.DebugFormat("In Jobs/Details {0}", id);
            var job = repository.Get(id);
            return Json(job, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Job/Create
        [HttpPost]
        public ActionResult Create(JobViewModel j)
        {
            log.Debug("In Jobs/Create");
            try
            {
                Job job = new Job();
                job.CallbackUrl = j.CallbackUrl;
                job.Status = (short)JobStatus.ToProcess;
                job.Data = j.Data;
                repository.Add(job);
                repository.Save();
                log.InfoFormat("Added new job: {0}", j.ToString());
                var jp = new JobProcessor(job.Id);
                jp.StartProcessing();
                return Json(new StatusIdResponse(StatusCode.Success, job.Id));
            }
            catch(Exception e)
            {
                log.Error("Exception occured: ",e);
                return Json(new StatusIdResponse(StatusCode.Failure));
            }
        }

        //
        // Put: /Job/Edit/5
        [HttpPut]
        public ActionResult Edit(JobViewModel editJob)
        {
            log.DebugFormat("In Jobs/Edit {0}", editJob.Id);
            try
            {
                JobAction action = new JobAction(repository);
                action.Update(editJob);
                return Json(new StatusIdResponse(StatusCode.Success, editJob.Id));
            }
            catch (KeyNotFoundException)
            {
                return Json(new StatusIdResponse(StatusCode.NotFound, editJob.Id));
            }
            catch
            {
                return Json(new StatusIdResponse(StatusCode.Failure, editJob.Id));
            }
        }

        //
        // DELETE: /Job/Delete/5
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            log.DebugFormat("In Jobs/Delete: {0}", id);
            try
            {
                Job job = repository.Get(id);
                if (job != null)
                {
                    repository.Delete(job);
                    repository.Save();
                    return Json(new StatusResponse(StatusCode.Success));
                }
                else
                {
                    return Json(new StatusResponse(StatusCode.NotFound));
                }
            }
            catch
            {
            }
            return Json(new StatusResponse(StatusCode.Failure));
        }
    }
}
