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

namespace TaskManager.WebUI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public ForgotPasswordModel(SignInManager<AppUser> signInManager,
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
        }

        public void OnGetAsync()
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {

            if (ModelState.IsValid)
            {
                var account = await _userManager.FindByEmailAsync(Input.Email);

                if (account == null)
                    ModelState.AddModelError(string.Empty, "No account registered with this email");

                var code = await _userManager.GeneratePasswordResetTokenAsync(account);

                string url = Url.Page("/Account/ResetPassword", new
                {
                    area = "Identity",
                    userId = account.Id,
                    token = code
                });


                var emailRequest = new EmailRequest()
                {
                    /* Body = $"Click the provided <a href='https://smarttaskmanagerwebui.azurewebsites.net{url}'>link</a> to reset your password",*/
                    Body = $"Click the provided <a href='https://localhost:44329{url}'>link</a> to reset your password",
                    To = Input.Email,
                    Subject = "Reset password",
                };
                await _emailService.SendAsync(emailRequest);

                return RedirectToPage("./Login");
            }

            return Page();
        }
    }
}
