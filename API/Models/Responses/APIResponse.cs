namespace API.Models
{
    public class APIResponse
    {
        public bool Ok { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public int? Total { get; set; }
        public object? Data { get; set; }

        public APIResponse(int StatusCode = 200, string? Message = null, int? Total = null, object? Data = null)
        {
            Ok = StatusCode < 400;
            this.StatusCode = StatusCode;
            this.Message = Message ?? DefaultMessage(StatusCode);
            this.Total = Total;
            this.Data = Data;
        }

        private static string? DefaultMessage(int statusCode) => statusCode switch
        {
            200 => "Ok",
            201 => "Created",
            204 => "No Content",
            400 => "Bad Request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Not Found",
            _ => null
        };
    }
}
