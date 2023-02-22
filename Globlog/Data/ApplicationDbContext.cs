using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Globlog.Models;
using Microsoft.AspNetCore.Identity;

namespace Globlog.Data
{
    //postgresql database context
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        //tables
        public DbSet<Globlog.Models.AppUser> AppUsers { get; set; }
        public DbSet<Globlog.Models.Story> Stories { get; set; }
        public DbSet<Globlog.Models.StoryUpdate> StoryUpdates { get; set; }
        public DbSet<Globlog.Models.UpdateLike> UpdateLikes { get; set; }
        public DbSet<Globlog.Models.StoryLike> StoryLikes { get; set; }
        public DbSet<Globlog.Models.Follow> Follows { get; set; }
        //on model creating
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql("Host=localhost;Database=globlog;Username=postgres;Password=root");
        }
        //OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.HasPostgresExtension("pg_trgm");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUser>().ToTable("AppUsers");
            modelBuilder.Entity<AppUser>().HasIndex(u => u.Id).IsUnique();
            //id is also required
            modelBuilder.Entity<AppUser>().Property(u => u.Id).IsRequired();
            //id has also default sql value of uuid_generate_v1()
            modelBuilder.Entity<AppUser>().Property(u => u.Id).HasDefaultValueSql("uuid_generate_v1()");
            modelBuilder.Entity<AppUser>().HasIndex(u => u.UserName).IsUnique();
            modelBuilder.Entity<AppUser>().Property(u => u.UserName).IsRequired();
            //make appuser's email required
            modelBuilder.Entity<AppUser>().Property(u => u.Email).IsRequired();
            //storyupdate's id has the same requirements as appuser's id (unique, required, default value uuid_generate_v1)
            modelBuilder.Entity<StoryUpdate>().HasIndex(s => s.StoryUpdateId).IsUnique();
            modelBuilder.Entity<StoryUpdate>().Property(s => s.StoryUpdateId).IsRequired();
            modelBuilder.Entity<StoryUpdate>().Property(s => s.StoryUpdateId).HasDefaultValueSql("uuid_generate_v1()");
            //set the likes table to be a composite unique on the owner and liked id
            modelBuilder.Entity<StoryLike>().HasIndex(ul => new { ul.OwnerId, ul.LikedId }).IsUnique();
            modelBuilder.Entity<UpdateLike>().HasIndex(ul => new { ul.OwnerId, ul.LikedId }).IsUnique();
            modelBuilder.Entity<Follow>().HasIndex(ul => new { ul.OwnerId, ul.FollowedId }).IsUnique();
            
        }
    }
    //seeder class
    public static class DbSeeder
    {
        //seed the database
        public static void Seed(ApplicationDbContext context)
        {
            //create the admin user
            var admin = new AppUser
            {
                UserName = "admin",
                Email = "admin@localhost",
                DisplayName = "Admin"
            };
            //create the admin password
            var password = new PasswordHasher<AppUser>();
            //hash the password
            var hashed = password.HashPassword(admin, "admin");
            //set the password
            admin.PasswordHash = hashed;
            //add the admin to the database
            context.AppUsers.Add(admin);
            //add a Story
            context.Stories.Add(new Story
            {
                Title = "My first story",
                Owner = admin
            });
            //add a StoryUpdate to the story
            context.StoryUpdates.Add(new StoryUpdate
            {
                StoryTitle = "My first story",
                StoryBody = """
                Once upon a time, there was a brave and curious little girl named Misfortune who lived with her unstable and neglectful mother. One day, Misfortune came across a mysterious voice in the forest that claimed to be the voice of her own personal narrator. This narrator promised her eternal happiness and a prize of a visit to a magical land called "the Land of the Dead."

                Determined to win this prize, Misfortune embarked on a journey filled with riddles, challenges, and creepy creatures. She even encountered a talking fox who promised to help her reach her goal. However, as Misfortune made her way through the obstacles, she slowly began to realize that the voice in the forest may not have had her best interests at heart.

                As Misfortune came closer to the Land of the Dead, she discovered some disturbing truths about her own life and her family. She had to face difficult choices and consequences in order to protect herself and the people she loved. Ultimately, Misfortune learned that true happiness comes from within and that the most important thing in life is to be kind and true to oneself.
                """,
                FromStory = context.Stories.First()
            });
            //save the changes
            context.SaveChanges();
        }
    }
}
