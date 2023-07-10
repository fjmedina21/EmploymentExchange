namespace EmploymentExchangeAPI.Models
{
    public class APIResponse
    {
        public bool Ok { get; set; } = true;
        public int StatusCode { get; set; } = 200;
        public int Total { get; set; } = 0;
        public object Data { get; set; } = new();

        public APIResponse() { }

        public APIResponse(object Data, int Total)
        {
            this.Total = Total;
            this.Data = Data;
        }

        public APIResponse(object Data)
        {
            Total = 1;
            this.Data = Data;
        }

        public APIResponse(int StatusCode, bool Ok)
        {
            this.Ok = Ok;
            this.StatusCode = StatusCode;
        }
    }
}
