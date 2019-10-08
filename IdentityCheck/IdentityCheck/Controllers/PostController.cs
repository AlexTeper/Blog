using AutoMapper;
using IdentityCheck.Models;
using IdentityCheck.Models.RequestModels;
using IdentityCheck.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IImageService imageService;
        private readonly IMapper mapper;

        public PostController(IPostService postService, UserManager<ApplicationUser> userManager, IMapper mapper, IImageService imageService)
        {
            this.postService = postService;
            this.userManager = userManager;
            this.imageService = imageService;
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
                postRequest.Author = currentUser;
                postRequest.AuthorId = currentUser.Id;
                await postService.SaveAsync(postRequest);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(postRequest);
        }


        [HttpPost("/delete")]
        public async Task<IActionResult> Delete(long id)
        {
            await postService.DeleteAsync(id);
            await imageService.DeleteAllFileAsync(id);
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
                var currentUser = await userManager.GetUserAsync(HttpContext.User);
                postRequest = mapper.Map<ApplicationUser, PostRequest>(currentUser, postRequest);
                await postService.EditAsync(id, postRequest);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(postRequest);
        }

        [Authorize(Roles = "SuperUser")]
        [HttpGet("/addimage/{postid}")]
        public IActionResult AddImage(long postId)
        {
            return View(postId);
        }

        [HttpPost("/addimage/{postid}")]
        public async Task<IActionResult> AddImage(List<IFormFile> imageList, long postId)
        {
            var wrongFiles = await imageService.UploadImagesAsync(imageList, postId);
            return RedirectToAction(nameof(Post), postId);
        }

        [HttpGet("/post/{postId}")]
        public async Task<IActionResult> Post(long postId)
        {
            var images = await imageService.GetImageListAsync(postId);
            return View(images);
        }
    }
}
