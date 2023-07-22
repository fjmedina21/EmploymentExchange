namespace EmploymentExchangeAPI.Models
{
    public class APIResponse
    {
        public bool Ok { get; set; } 
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public int? Total { get; set; }
        public object? Data { get; set; }

        public APIResponse(bool Ok = true, int StatusCode = 200, string? Message = null, int? Total = null, object? Data = null)
        {
            this.Ok = Ok;
            this.StatusCode = StatusCode;
            this.Message = Message ?? DefaultMessage(StatusCode);
            this.Total = Total;
            this.Data = Data;
        }

        private static string? DefaultMessage(int statusCode) => (statusCode) switch
        {
            200 => "action completed successfully",
            201 => "resource created",
            204 => "no content",
            400 => "bad request",
            401 => "unauthorized",
            403 => "forbidden",
            404 => "resource not found",
            500 => "internal server error",
            _ => null
        };
    }
}
