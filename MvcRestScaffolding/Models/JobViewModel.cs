using MvcRestScaffolding.Helpers;

namespace MvcRestScaffolding.Models
{
    //TODO add "date added" field?
    public class JobViewModel
    {
        public long Id { get; set; }
        public string CallbackUrl { get; set; }
        public JobStatus Status { get; set; }
        public string Data { get; set; }

        public JobViewModel()
        {

        }

        public JobViewModel(string callbackUrl = "", string data= "",
            JobStatus status = JobStatus.ToProcess, long id = 0)
        {
            Id = id;
            CallbackUrl = callbackUrl;
            Status = status;
            Data = data;
        }

        public JobViewModel(DAL.Job j)
        {
            Id = j.Id;
            CallbackUrl = j.CallbackUrl;
            Status = (JobStatus)j.Status;
            Data = j.Data;
        }

        public override string ToString()
        {
            return string.Format("[Job id] {0}", Id);
        }
    }
}