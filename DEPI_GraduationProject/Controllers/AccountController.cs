using AutoMapper;
using DEPI_DomainLayer.Entities;
using DEPI_GraduationProject.Helper;
using DEPI_GraduationProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace DEPI_GraduationProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                // Get the file
                var file = registerVM.PictureUrl;

                // Check if the file is valid
                if (file != null && file.Length > 0)
                {
                    // Get the wwwroot path
                    var wwwRootPath = _hostEnvironment.WebRootPath;

                    // Generate a unique file name
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var extension = Path.GetExtension(file.FileName);
                    var newFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                    // Combine the wwwroot path with the folder path to save the image
                    var path = Path.Combine(wwwRootPath, "Images/Users", newFileName);

                    // Save the image to the path
                    try
                    {
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        registerVM.ImagePath = $"/Images/Users/{newFileName}";
                    }
                    catch (Exception ex)
                    {
                        // Log the error
                        ModelState.AddModelError("", "Error uploading file. Please try again.");
                        return View(registerVM);
                    }

                    // Map the view model to the user entity
                    var user = _mapper.Map<ApplicationUser>(registerVM);
                    user.PicturalUrl = registerVM.ImagePath;
                    // Create the user
                    var result = await _userManager.CreateAsync(user, registerVM.Password); //create user in database
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, Roles.User.ToString());
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("PictureUrl", "Please upload a valid image.");
                }
            }

            return View(registerVM);

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var user =await  _userManager.FindByEmailAsync(loginVM.Email);
                if (user != null)
                {
                    await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(loginVM);
        }

        public async Task<IActionResult> LogOut()
        {
            // Sign the user out using the SignInManager
            await _signInManager.SignOutAsync();     
            // Redirect to the Login action
            return RedirectToAction("Index", "Home"); // Ensure to specify the controller if necessary
        }

        public async Task<IActionResult> Profile(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
               return NotFound();
            }
            return View(user);
        }

       public async Task<IActionResult> Team()
        {
            var teamMembers = new List<TeamMember>
        {
            new TeamMember
            {
                Name = "Eng/Mohamed Esmail",
                PictureUrl = "/Images/Team/mohamed.jpg",
                Email = "m7med.esmail22@gmail.com",
                Phone = "+201062838535"
            },
            new TeamMember
            {
                Name = "Eng/Eslam Gaballah",
                PictureUrl = "/Images/Team/eslam.jpeg",
                Email = "eslamgaballa232@gmail.com",
                Phone = "+201090781579"
            },
             new TeamMember
            {
                Name = "Eng/Ahmed AbdelMoniem",
                PictureUrl = "/Images/Team/ahmed.jpeg",
                Email = "ahmedkhalfallah001@gmail.com",
                Phone = "+201021321423"
            },
            // Add more team members as needed
        };

            return View(teamMembers);
        }
    }
}
