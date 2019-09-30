using IdentityCheck.Models;
using IdentityCheck.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCheck.Services
{
    public interface IPostService
    {
        Task<Post> SaveAsync(PostRequest postRequest, ApplicationUser user);
        Task DeleteAsync(long id);
        Task<Post> FindByIdAsync(long postId);
        Task<Post> EditAsync(long id, PostRequest request);
    }
}
