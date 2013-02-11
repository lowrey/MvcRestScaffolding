
namespace MvcRestScaffolding.Models
{
    public class StatusResponse
    {
        public StatusCode statusCode { get; set; }
        public string statusString { get; set; }

        public StatusResponse(StatusCode sc)
        {
            statusCode = sc;
            switch(sc)
            {
                case(StatusCode.Success):
                    statusString = "The operation has been completed successfully";
                    break;
                case(StatusCode.Failure):
                    statusString = "The operation did not complete successfully";
                    break;
                case(StatusCode.NotFound):
                    statusString = "The record could not be found to perform the operation";
                    break;
                default:
                    statusString = "";
                    break;
            }
        }
    }

    public enum StatusCode
    {
        Success = 0,
        Failure,
        NotFound,
    }
}