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
            this.Message = Message;
            this.Total = Total;
            this.Data = Data;
        }
    }
}
