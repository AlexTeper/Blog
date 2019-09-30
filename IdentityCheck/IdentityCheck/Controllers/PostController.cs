using AutoMapper;
using IdentityCheck.Models;
using IdentityCheck.Models.RequestModels;
using IdentityCheck.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCheck.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public PostController(IPostService postService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.postService = postService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpGet("/add")]
        public IActionResult Add()
        {
            return View(new PostRequest());
        }

        [HttpPost("/add")]
        public async Task<IActionResult> Add(PostRequest postRequest)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await userManager.GetUserAsync(HttpContext.User);
                await postService.SaveAsync(postRequest, currentUser);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(postRequest);
        }


        [HttpPost("/delete")]
        public async Task<IActionResult> Delete(long id)
        {
            await postService.DeleteAsync(id);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [Authorize(Roles = "SuperUser")]
        [HttpGet("/edit/{id}")]
        public async Task<IActionResult> Edit(long id)
        {
            var post = await postService.FindByIdAsync(id);
            var request = mapper.Map<Post, PostRequest>(post);
            return View(request);
        }

        [Authorize(Roles = "SuperUser")]
        [HttpPost("/edit/{id}")]
        public async Task<IActionResult> Edit(PostRequest postRequest, long id)
        {
            if (ModelState.IsValid)
            {
                await postService.EditAsync(id, postRequest);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(postRequest);
        }
    }
}
