using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityCheck.Data;
using IdentityCheck.Models;
using IdentityCheck.Models.RequestModels.Account;
using IdentityCheck.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityCheck.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext applicationDbContext;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
            , ApplicationDbContext applicationDbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<List<Post>> GetPostsAsync(ApplicationUser user)
        {
            var cuser = await applicationDbContext.Users.Include(u => u.Posts).FirstAsync(u => u.Email == user.Email);
            return cuser.Posts.ToList();
        }

        public async Task<List<string>> LoginAsync(LoginRequest model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                model.ErrorMessages.Add("User with the given Email does not exist");
                return model.ErrorMessages;
            }
            var result = await signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);
            model.ErrorMessages = checkLoginErrors(result, model.ErrorMessages);
            return model.ErrorMessages;
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<List<string>> RegisterAsync(RegisterRequest model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "RegularUser");
                await signInManager.SignInAsync(user, isPersistent: false);
            }
            return result.Errors
                .Select(e => e.Description)
                .ToList();
        }

        private List<string> checkLoginErrors(SignInResult result, List<string> errors)
        {
            if (result.IsLockedOut)
            {
                errors.Add("User account locked out.");
            }
            if (result.IsNotAllowed)
            {
                errors.Add("User is not allowed to login.");
            }
            if (result.RequiresTwoFactor)
            {
                errors.Add("Two factor authentication is required.");
            }
            if (!result.Succeeded)
            {
                errors.Add("Invalid login attempt");
            }
            return errors;
        }

    }
}
