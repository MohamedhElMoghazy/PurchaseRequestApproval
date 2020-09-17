using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using PurchaseRequestApproval.DataAccess.Repository.IRepository;
using PurchaseRequestApproval.Models;
using PurchaseRequestApproval.Utility;

namespace PurchaseRequestApproval.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        // Adding Role Manager property
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;


        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            // Adding the new data for Application User

            // Access Level to be handled lower level for data entry and increase with admins
            // [Required]
            //public int AccessLevel { get; set; }

            [Required] 
            public string Name { get; set; }

            [Required] // to enable null value
            public int EmployeeUser { get; set; }  

            public IEnumerable<SelectListItem> EmployeeList { get; set; } // To show drop down

            // Role to be not mapped not to send to the data base
            public String Role { get; set; }
            public IEnumerable<SelectListItem> RoleList { get; set; } // To show drop down






        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            Input = new InputModel()
            {
                EmployeeList = _unitOfWork.Employee.GetAll().Select(i => new SelectListItem
                {
                    Text = i.EmployeeName,
                    Value = i.Id.ToString()

                }),

                RoleList = _roleManager.Roles.Where(u=>u.Name!=SD.Role_Employee_View).Select(x=>x.Name).Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i

                })


        };


            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                //var user = new IdentityUser { UserName = Input.Email, Email = Input.Email }; // Disabled to Add Application user
                // Starting for new employee Adding


                    var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    //AccessLevel = Input.AccessLevel,
                    Name = Input.Name,
                    EmployeeUser = Input.EmployeeUser,
                   // EmployeeUser = null,
                    Role = Input.Role
                };
                if (user.EmployeeUser == 0) { user.EmployeeUser = null; } // To handle some error in the data base


                // Ending of new modification for employee
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    // Adding Admin role if not exist
                    if (!await _roleManager.RoleExistsAsync(SD.Role_Admin_Modify))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin_Modify));

                        // Add logic for first user to add
                        var userInitial = new ApplicationUser
                        {
                            UserName = "admin",
                            Email = "admin@admin.com",
                            //AccessLevel = Input.AccessLevel,
                            Name = "admin",
                            EmployeeUser = null,
                            PasswordHash = "admin",
                            Role = SD.Role_Admin_Modify
                        };



                    }

                    if (!await _roleManager.RoleExistsAsync(SD.Role_Admin_View))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin_View));
                    }

                    if (!await _roleManager.RoleExistsAsync(SD.Role_Employee_Modify))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee_Modify));
                    }

                    if (!await _roleManager.RoleExistsAsync(SD.Role_Employee_View))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee_View));
                    }

                    //await _userManager.AddToRoleAsync(user, SD.Role_Admin_Modify); // only used first time of running the program
                    if(user.Role==null)
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Employee_View);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, user.Role); // assign the role from the drop list

                    }




                    // Ending Adding role 

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                   

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        if (user.Role==null)
                        { 
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }
                        else
                        {
                            /*
                            if (user.EmployeeUser > 0)
                            {
                                await _userManager.AddToRoleAsync(user, SD.Role_Employee_Modify);
                            }
                            */
                            // Admin is Registering a new user
                            return RedirectToAction("Index", "User", new { Area = "Admin" });
                        }
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
