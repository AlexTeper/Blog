using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityCheck.Data;
using IdentityCheck.Models;
using IdentityCheck.Models.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace IdentityCheck.Services
{
    public class PostService : IPostService
    {

        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper mapper; // still needs to be implemented

        public PostService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        public async Task DeleteAsync(long id)
        {
            var post = await FindByIdAsync(id);
            applicationDbContext.Posts.Remove(post);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<Post> EditAsync(long id, PostRequest request)
        {
            var post = await FindByIdAsync(id);

            post = mapper.Map<PostRequest, Post>(request, post);
            
            await applicationDbContext.SaveChangesAsync();
            return post;
        }

        public async Task<Post> FindByIdAsync(long postId)
        {
            return await applicationDbContext.Posts.Include(p => p.Author).FirstOrDefaultAsync(p => p.PostId == postId);
        }

        public async Task<Post> SaveAsync(PostRequest postRequest, ApplicationUser user)
        {
            var post = new Post
            {
                Title = postRequest.Title,
                Description = postRequest.Description,
                ApplicationUserId = user.Id,
                Author = user
            };
            await applicationDbContext.Posts.AddAsync(post);
            await applicationDbContext.SaveChangesAsync();
            return post;
        }

        
    }
}
