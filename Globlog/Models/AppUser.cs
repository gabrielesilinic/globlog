using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;

namespace Globlog.Models
{
    /// <summary>
    /// Default role for the application, it has no particular privileges
    /// </summary>
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole() : base()
        {
        }

        public AppRole(string name) : base(name)
        {
            base.Name = name;
        }
    }
    //admin role that inherits from AppRole
    public class AdminRole : AppRole
    {
        public AdminRole() : base("Admin")
        {
            
        }
    }

    //a AppUser that inheriths from IdentityUser, as key has a UUID v1 and not a int
    [Table("AppUsers")]
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser() : base()
        {
        }
        [Key]
        [Required]
        public override Guid Id { get; set; }
        [MaxLength(50)]
        public string? DisplayName { get; set; }
        [MaxLength(20)]
        [RegularExpression(@"^[a-zA-Z0-9._]+$")]
        [Required]
        public override string UserName { get; set; }
        //followerscount with a min of 0
        [Range(0, int.MaxValue)]
        [DefaultValue(0)]
        public long FollowersCount { get; set; }
        //followingcount with a min of 0
        [Range(0, int.MaxValue)]
        [DefaultValue(0)]
        public long FollowingCount { get; set; }
        public ICollection<Story> Stories { get; set; }
    }
}

