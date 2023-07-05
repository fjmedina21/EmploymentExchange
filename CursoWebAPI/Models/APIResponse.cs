﻿namespace EmploymentExchange.Models
{
    public class APIResponse
    {
        public bool Ok { get; set; }
        public int StatusCode { get; set; }
        public object Data { get; set; }

        public APIResponse(object Data, int StatusCode = 200 , bool Ok = true)
        {
            this.Ok = Ok;
            this.StatusCode = StatusCode;
            this.Data = Data;
        }

        public APIResponse(int StatusCode, bool Ok)
        {
            this.Ok = Ok;
            this.StatusCode = StatusCode;
            this.Data = new();
        }
    }
}
