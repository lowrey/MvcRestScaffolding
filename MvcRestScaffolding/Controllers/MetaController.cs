using System.Collections.Generic;
using System.Web.Mvc;
using log4net;
using MvcRestScaffolding.DAL;
using MvcRestScaffolding.Models;

namespace MvcRestScaffolding.Controllers
{
    public class MetaController : Controller
    {
        private IRepository<Job> repository = null;
        private ILog log;

        public MetaController()
        {
            repository = new JobRepository();
            log = LogManager.GetLogger(this.GetType());
        }

        public MetaController(IRepository<Job> repository)
        {
            this.repository = repository;
        }
        //
        // GET: /Meta/

        public ActionResult Index()
        {
            //return View();
            return RedirectToAction("Jobs");
        }

        public ActionResult Jobs()
        {
            log.Debug("In Meta/Jobs");
            var jobs = repository.FindAll();
            var list = new List<JobViewModel>();
            foreach (Job j in jobs)
                list.Add(new JobViewModel(j));
            return View(list);
        }

        public ActionResult About()
        {
            return View();
        }

    }
}
