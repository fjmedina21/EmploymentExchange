namespace API.Models
{
    public class APIErrorResponse
    {
        public int StatusCode { get; set; }
        public object Error { get; set; }

        public APIErrorResponse(int StatusCode, Guid? TraceId = null, string? Message = null)
        {
            this.StatusCode = StatusCode;
            Error = new { TraceId, Message };
        }
    }
}
