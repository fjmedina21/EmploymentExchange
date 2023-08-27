﻿using EmploymentExchangeAPI.Models;

namespace EmploymentExchangeAPI.Repositories
{
    public interface IJWT
    {
        public Task<string> CreateJWTAsync(User user);
        public (Guid, List<string>) DecodeJWT(string token);
    }
}
