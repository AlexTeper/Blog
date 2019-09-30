﻿using IdentityCheck.Models;
using IdentityCheck.Models.RequestModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCheck.Services
{
    public interface IUserService
    {
        Task<List<string>> LoginAsync(LoginRequest model);
        Task Logout();
        Task<List<string>> RegisterAsync(RegisterRequest model);
        Task<List<Post>> GetPostsAsync(ApplicationUser user);
    }
}