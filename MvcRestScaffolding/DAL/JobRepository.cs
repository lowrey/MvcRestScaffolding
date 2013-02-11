using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcRestScaffolding.DAL
{
    public class JobRepository : IRepository<Job>
    {
        private JobEntityContainer db = new JobEntityContainer();

        public IQueryable<Job> FindAll()
        {
            return db.Jobs;
        }

        public Job Get(long id)
        {
            return db.Jobs.FirstOrDefault(j => j.Id == id);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public Job Add(Job job)
        {
            db.Jobs.AddObject(job);
            return job;
        }

        public void Delete(Job job)
        {
            db.Jobs.DeleteObject(job);
        }

    }
}