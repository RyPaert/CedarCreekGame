﻿using CedarCreek.Core.Domain;
using CedarCreek.Core.Dto.AccountsDtos;
using CedarCreek.Data;
using CedarCreek.Models.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CedarCreek.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly CedarCreekContext _context;

        public AccountsController
            (UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            CedarCreekContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> AddPassword()
        {
            var user = await _userManager.GetUserAsync(User);
            var userHasPassword = await _userManager.HasPasswordAsync(user);
            if ( userHasPassword)
            {
                RedirectToAction("ChangePassword");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPassword(AddPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var result = await _userManager.AddPasswordAsync(user, model.NewPassword);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }
                await _signInManager.RefreshSignInAsync(user);
                return View("AddPasswordConfirmation");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!result.Succeeded)
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }
                await _signInManager.RefreshSignInAsync(user);
                return View("ChangePasswordConfirmation");
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && await _userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("ResetPassword", "Accounts", new { email = model.Email, token = token }, Request.Scheme);

                    return View("ForgotPasswordConfirmation");
                }
                return View("ForgotPasswordConfirmation");
            }
        return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword()
        {
            var user = await _userManager.GetUserAsync(User);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (token == null || user.Email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            var model = new ResetPasswordViewModel()
            {
                Token = token,
                Email = user.Email,
            };
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Password, model.Token);
                    if (result.Succeeded)
                    {
                        if (await _userManager.IsLockedOutAsync(user))
                        {
                            await _userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow);
                        }
                        await _signInManager.SignOutAsync();
                        await _userManager.DeleteAsync(user);
                        return RedirectToAction("ResetPasswordConfirmation", "Accounts");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return RedirectToAction("ResetPasswordConfirmation", "Accounts");
                }
                await _userManager.DeleteAsync(user);
                return RedirectToAction("ResetPasswordConfirmation", "Accounts");
            }
            return RedirectToAction("ResetPasswordConfirmation", "Accounts");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation() { return View(); }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    City = model.City,
                    ProfileType = model.ProfileType
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Accounts", new {userId = user.Id, token = token}, Request.Scheme);
                }
                if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                {
                    return RedirectToAction("ListUsers", "Administrations");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                ViewBag.ErrorTitle = "You have successfully registered";
                ViewBag.ErrorMessage = "Before you can log in, please click on the link in your email.";
                return View("Error");
            }
            return View();
        }
        [HttpGet]
        [AllowAnonymous] 
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"the user with id of ${userId} is not valid";
                return View("NotFound");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View();
            }
            ViewBag.ErrorTitle = "Email can't be confirmed";
            return View("Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnURL)
        {
            LoginViewModel vm = new LoginViewModel()
            {
                returnURL = returnURL,
            };
                return View(vm);

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnURL)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed && (await _userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Your email hasn't been confirmed yet. Please make sure to check your Email spam folder.");
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(model.Password, model.Password, model.RememberMe, true);
                if (result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(returnURL) && Url.IsLocalUrl(returnURL))
                    {
                        return Redirect(returnURL);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                if (result.IsLockedOut)
                {
                    return View("AccountLocked");
                }
                ModelState.AddModelError("", "Invalid Login Attempt, send me 500 bucks");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
