using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<GroupTraining> GroupTrainings { get; set; }
        public DbSet<IndividualTraining> IndividualTrainings { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class DatabaseInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext ctx)//CreateDatabaseIfNotExists
        {
            //менеджер по работе с пользователями
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(ctx));
            //менеджер по работе с ролями
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ctx));

            //роли
            var userRole = new IdentityRole { Name = "user" };
            var trainer = new IdentityRole { Name = "moderator" };
            var adminRole = new IdentityRole { Name = "admin" };

            //добавление ролей в БД
            roleManager.Create(userRole);
            roleManager.Create(trainer);
            roleManager.Create(adminRole);

            //создание пользователей по умолчанию
            var moderator = new ApplicationUser { Email = "masha_moderator@gmail.com", UserName = "MarusyaModerator" };
            var admin = new ApplicationUser { Email = "admin@gmail.com", UserName = "AdminKolyunya" };
                
            ctx.Trainers.Add(new Trainer
                {
                    Fio = "MarusyaModerator",
                    DateOfBirth = "25.02.75",
                    PhoneNumber = "0951479853",
                    Status = "главный тренер",
                    Login = "masha_moderator@gmail.com",
                    Specialization = "танцы"
                });
                ctx.SaveChanges();
            

            //добавление пользователей в БД
            if (userManager.Create(moderator, "123456").Succeeded)
            {
                userManager.AddToRole(moderator.Id, trainer.Name);
                userManager.AddToRole(moderator.Id, userRole.Name);
            }

            if (userManager.Create(admin, "123456").Succeeded)
            {
                userManager.AddToRole(admin.Id, userRole.Name);
                userManager.AddToRole(admin.Id, trainer.Name);
                userManager.AddToRole(admin.Id, adminRole.Name);
            }

            base.Seed(ctx);
        }
    }
}