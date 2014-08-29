using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using LSPOSWA.Models;

namespace LSPOSWA
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            ApplicationUser adminUser;
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = false,
            };
            
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));
            // Validating Administrators Role
            if (!RoleManager.RoleExists("Administrators"))
            {
                var roleresult = RoleManager.Create(new IdentityRole("Administrators"));
            }

            // Validating if super user exist
            adminUser = manager.FindByName("sysadmin");
            if (adminUser == null)
            {
                adminUser = new ApplicationUser();
                adminUser.UserName = "sysadmin";
                adminUser.FullName = "Administrador";
                adminUser.Email = "vlicovali@gmail.com";
                adminUser.EmailConfirmed = true;

                // Adding sysadmin user with "sysadmin" password encrypted
                var adminResult = manager.Create(adminUser, "48a365b4ce1e322a55ae9017f3daf0c0");
            }
            if (!manager.IsInRole(adminUser.Id, "Administrators"))
            {
                var roleResult = manager.AddToRole(adminUser.Id, "Administrators");
            }
            
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
