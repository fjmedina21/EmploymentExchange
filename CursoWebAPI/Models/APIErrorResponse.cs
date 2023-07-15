namespace EmploymentExchangeAPI.Models
{
    public class APIErrorResponse
    {
        public int StatusCode { get; set; }
        public object Error { get; set; }

        public APIErrorResponse() { }

        public APIErrorResponse(int StatusCode, object Error)
        {
            this.StatusCode = StatusCode;
            this.Error = Error;
        }
    }
}
