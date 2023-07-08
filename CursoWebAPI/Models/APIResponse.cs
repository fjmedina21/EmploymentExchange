namespace EmploymentExchange.Models
{
    public class APIResponse
    {
        public bool Ok { get; set; } = true;
        public int StatusCode { get; set; } = 200;
        public int Total { get; set; } = 0;
        public object Data { get; set; } = new();

        public APIResponse(object Data, int Total)
        {
            this.Total = Total;
            this.Data = Data;
        }

        public APIResponse( object Data)
        {
            this.Total = 1;
            this.Data = Data;
        }

        public APIResponse(int StatusCode, bool Ok)
        {
            this.Ok = Ok;
            this.StatusCode = StatusCode;
        }

        public APIResponse(object Data, int StatusCode, bool Ok)
        {
            this.Ok = Ok;
            this.StatusCode = StatusCode;
            this.Data = Data;
        }
    }
}
