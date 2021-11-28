using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TaskManager.Persistence.Identity;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Common.Models;
using System;

namespace TaskManager.WebUI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public ResetPasswordModel(SignInManager<AppUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<AppUser> userManager,
            IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }


        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            public string Token { get; set; }
            [Required]
            [MinLength(6)]
            public string Password { get; set; }

            [Required]
            [Compare("Password")]
            public string ConfirmPassword { get; set; }
        }

        public IActionResult OnGetAsync(string userId, string token)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Input = new InputModel { Token = token };

            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (ModelState.IsValid)
            {
                var account = await _userManager.FindByEmailAsync(Input.Email);
                if (account == null) ModelState.AddModelError(string.Empty, $"No Accounts Registered with {Input.Email}.");
                var result = await _userManager.ResetPasswordAsync(account, Input.Token, Input.Password);
                if (result.Succeeded)
                {
                    return RedirectToPage("./Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error occured while reseting the password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
