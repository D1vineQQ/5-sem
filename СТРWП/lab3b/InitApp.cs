using lab3b.Data;
using Microsoft.AspNetCore.Identity;

namespace lab3b
{
    public class InitApp
    {
        UserManager<IdentityUser> um;
        RoleManager<IdentityRole> rm;
        ApplicationDbContext db;
        //IPasswordHasher<IdentityUser> hs;
        ILogger<Program> lg;
        const string adminpass = "qweqwe";
        const string adminemail = "admin2@gmail.com";
        const string adminname = "admin1";
        const string adminrole = "Administrator";
        public InitApp(WebApplication? app)
        {
            var scope = app?.Services.CreateScope();
            if(scope != null)
            {
                var services = scope.ServiceProvider;
                this.um = (UserManager<IdentityUser>?)services?.GetRequiredService<UserManager<IdentityUser>>();
                this.rm = (RoleManager<IdentityRole>?)services?.GetRequiredService<RoleManager<IdentityRole>>();
                this.db = (ApplicationDbContext?)services?.GetRequiredService<ApplicationDbContext>();
                //this.hs = (IPasswordHasher<IdentityUser>?)services?.GetRequiredService<IPasswordHasher<IdentityUser>>();
                this.lg = (ILogger<Program>?)services?.GetRequiredService<ILogger<Program>>();

            }
        }
        public async Task initIdentity()
        {
            try
            {
                if (this.db == null) throw new Exception("bad ApplicationDbContext");
                //if (this.hs == null) throw new Exception("bad IPasswordHasher");
                if (this.um == null) throw new Exception("bad UserManager");
                if (this.rm == null) throw new Exception("bad RoleManager");
                if (this.lg == null) throw new Exception("bad ILogger");
                //IdentityUser user1 = await um.FindByNameAsync(adminname);
                //if (user1 != null) await um.AddToRoleAsync(user1, adminrole);
                var userSearch = await this.um.FindByNameAsync(adminname);
                if (userSearch == null)
                {
                    IdentityUser newuser = new IdentityUser { UserName = adminname, Email = adminemail };
                    //string passhash = this.hs.HashPassword(newuser, adminpass);
                    if ((await um.CreateAsync(newuser, adminpass)).Succeeded)
                        this.lg.LogInformation(string.Format($"{adminname}/{adminpass}"));
                    if (!await rm.RoleExistsAsync(adminrole))
                    {
                        IdentityRole newrole = new IdentityRole { Name = adminrole };
                        if ((await rm.CreateAsync(newrole)).Succeeded)
                        {
                            this.lg.LogInformation(string.Format($"Create role: {adminrole}"));
                        }
                    }
                    IdentityUser user = await um.FindByNameAsync(adminname);
                    if (user != null) await um.AddToRoleAsync(user, adminrole);
                }
            }
            catch (Exception ex) { this.lg?.LogError(ex, "Error INIT"); }
        }
    }
}
