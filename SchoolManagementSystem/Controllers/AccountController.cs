using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.ViewModels;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _context; // Add ApplicationDbContext

    public AccountController(SignInManager<IdentityUser> signInManager,
                             UserManager<IdentityUser> userManager,
                             ApplicationDbContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _context = context; // Inject ApplicationDbContext
    }

    // GET: /Account/Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Account/Login
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "StudentDashboard");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View(model);
    }

    // GET: /Account/Register
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // POST: /Account/Register
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Create Identity user
        var user = new IdentityUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            // Map RegisterViewModel to Student entity
            var student = new Student
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                DateOfBirth = model.DateOfBirth,
                PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password)
            };

            // Save Student entity to the database
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "StudentDashboard");
        }

        // Add errors if user creation fails
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    // POST: /Account/Logout
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
