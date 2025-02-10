using lab3b.Data;
using lab3b.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace lab3b.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext context;
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;
        //IPasswordHasher<IdentityUser> hasher;
        SignInManager<IdentityUser> signInManager;
        public AdminController(ApplicationDbContext context, UserManager<IdentityUser> userManager, /*IPasswordHasher<IdentityUser> hasher,*/ SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            //this.hasher = hasher;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            ViewBag.users = context.Users.ToList<IdentityUser>();
            ViewBag.roles = context.Roles.ToList<IdentityRole>();
            ViewBag.users_roles = context.UserRoles.ToList<IdentityUserRole<string>>();


            ViewBag.UserName = User.Identity?.Name;
            var roles = User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            ViewBag.current_roles = roles;

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser { UserName = model.UserName };
                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (!await roleManager.RoleExistsAsync("User"))
                    {
                        IdentityRole newrole = new IdentityRole { Name = "User" };
                        await roleManager.CreateAsync(newrole);
                    }
                    await userManager.AddToRoleAsync(user, "User");
                    await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ChangePassword(ChangePassModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await userManager.GetUserAsync(User);
            var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                await signInManager.RefreshSignInAsync(user);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public /*async*/ IActionResult SignIn()
        {
            //if ((ViewBag.currentUser = this.HttpContext.User.Identity?.Name) != null)
            //{
            //    IdentityUser? user = await userManager.FindByNameAsync(ViewBag.currentUser);
            //    if (user != null)
            //    {
            //        ViewBag.currentRoles = userManager.GetRolesAsync(user).Result;
            //    }
            //}
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(string? name, string? pass)
        {
            //IdentityUser? user = null;
            //if(name != null)
            //{
            //    user = await userManager.FindByNameAsync(name);
            //    if(user != null && (await this.userManager.CheckPasswordAsync(user, pass)))
            //    {
            //        await this.signInManager.SignInAsync(user, isPersistent: false);
            //    }
            //}
            //return RedirectToAction("Users", "Admin");
            if (name != null && pass != null)
            {
                var result = await signInManager.PasswordSignInAsync(name, pass, false, false);
                if (result.Succeeded)
                {
                    if ((ViewBag.currentUser = this.HttpContext.User.Identity?.Name) != null)
                    {
                        IdentityUser? user = await userManager.FindByNameAsync(ViewBag.currentUser);
                        if (user != null)
                        {
                            ViewBag.currentRoles = userManager.GetRolesAsync(user).Result;
                        }
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        //[HttpGet]
        //[Authorize]
        //public IActionResult SignOutUser()
        //{
        //    return View();
        //}

        //[HttpPost]
        [Authorize]
        public async Task<IActionResult> SignOutUser()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        ///IsSignOut







        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateUser( )
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateUser(RegisterModel model)
        {
            IdentityUser newuser = new IdentityUser { UserName = model.UserName };
            //string passhash = this.hasher.HashPassword(newuser, pass);
            //newuser.PasswordHash = passhash;
            IdentityResult result = await userManager.CreateAsync(newuser, model.Password);
            if (result.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync("User"))
                {
                    IdentityRole newrole = new IdentityRole { Name = "User" };
                    await roleManager.CreateAsync(newrole);
                }
                await userManager.AddToRoleAsync(newuser, "User");
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }


        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteUser()
        {
            ViewBag.users = context.Users.ToList<IdentityUser>();
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteUser(string UserName)
        {
            IdentityUser? user = await userManager.FindByNameAsync(UserName);
            
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult AssignRole()
        {
            ViewBag.users = context.Users.ToList<IdentityUser>();
            ViewBag.roles = context.Roles.ToList<IdentityRole>();
            ViewBag.users_roles = context.UserRoles.ToList<IdentityUserRole<string>>();


            ViewBag.UserName = User.Identity?.Name;
            var roles = User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            ViewBag.current_roles = roles;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AssignRole(string UserName, string RoleName)
        {
            IdentityUser? user = await userManager.FindByNameAsync(UserName);
            if (user != null)
            {
                //IdentityResult result = await userManager.DeleteAsync(user);
                //if (result.Succeeded)
                //{
                if (!await roleManager.RoleExistsAsync(RoleName))
                {
                    return View();
                    //IdentityRole newrole = new IdentityRole { Name = RoleName };
                    //await roleManager.CreateAsync(newrole);
                }
                else
                {
                    await userManager.AddToRoleAsync(user, RoleName);
                    return RedirectToAction("Index", "Admin");
                }
                    
                //}
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateRole()
        {
            return View();
        }


        [HttpGet]
        [Authorize]
        public IActionResult DeleteOwn()
        {
            ViewBag.UserName = User.Identity?.Name;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteOwn(string UserName)
        {
            IdentityUser? user = await userManager.FindByNameAsync(UserName);
            if (user != null)
            {
                var isAdmin = await userManager.IsInRoleAsync(user, "Administrator");
                if (isAdmin)
                {
                    return RedirectToAction("Error", "Admin", new { message = "Админ не может удалить свою запись здесь" });
                    //return RedirectToAction("Index", "Admin");
                }
                else
                {
                    IdentityResult result = await userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        await signInManager.SignOutAsync();
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateRole(string RoleName)
        {
            IdentityResult result;
            if (!await roleManager.RoleExistsAsync(RoleName))
            {
                IdentityRole newrole = new IdentityRole { Name = RoleName };
                result = await roleManager.CreateAsync(newrole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View();
        }


        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteRole()
        {
            ViewBag.users = context.Users.ToList<IdentityUser>();
            ViewBag.roles = context.Roles.ToList<IdentityRole>();
            ViewBag.users_roles = context.UserRoles.ToList<IdentityUserRole<string>>();


            ViewBag.UserName = User.Identity?.Name;
            var roles = User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            ViewBag.current_roles = roles;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteRole(string RoleName)
        {
            IdentityResult result;
            if (await roleManager.RoleExistsAsync(RoleName))
            {
                IdentityRole role = await roleManager.FindByNameAsync(RoleName);
                var usersInRole = await userManager.GetUsersInRoleAsync(RoleName);
                foreach (var user in usersInRole)
                {
                    await userManager.RemoveFromRoleAsync(user, RoleName);
                }

                result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View();
        }

        [HttpGet("error")]
        public IActionResult Error(string message)
        {
            // Сохранение сообщения в TempData для отображения на странице ошибки
            TempData["ErrorMessage"] = message;

            // Переадресация на указанный контроллер и действие
            return View();
        }

        //public IActionResult Users()
        //{
        //    ViewBag.currentUser = this.HttpContext.User.Identity?.Name;
        //    ViewBag.users = userManager.Users.ToList<IdentityUser>();
        //    return View();
        //}
        //public async Task<IActionResult> Roles()
        //{
        //    //ViewBag.currentUser = this.HttpContext.User.Identity?.Name;
        //    ViewBag.roles = context.Roles.ToList<IdentityRole>();
        //    ViewBag.isAuth = this.HttpContext.User.Identity?.IsAuthenticated;
        //    if ((ViewBag.currentUser = this.HttpContext.User.Identity?.Name) != null)
        //    {
        //        IdentityUser? user = await userManager.FindByNameAsync(ViewBag.currentUser);
        //        if (user != null)
        //        {
        //            ViewBag.currentRoles = await userManager.GetRolesAsync(user);
        //        }
        //    }
        //    return View();
        //}
    }
}
