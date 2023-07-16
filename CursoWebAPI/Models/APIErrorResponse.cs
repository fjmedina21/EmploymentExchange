namespace EmploymentExchangeAPI.Models
{
    public class APIErrorResponse
    {
        public int StatusCode { get; set; }
        public object Error { get; set; } 

        public APIErrorResponse(int StatusCode, Guid? TraceId = null, string? ErrorMessage = null)
        {
            this.StatusCode = StatusCode;
            this.Error = new { TraceId, ErrorMessage };
        }
    }
}
