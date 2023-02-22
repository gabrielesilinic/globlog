using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Globlog.Models
{
    public class Follow
    {
        //serial id
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FollowId { get; set; }
        //owner foreign key of AppUser
        [Required]
        [ForeignKey("Owner")]
        public Guid OwnerId { get; set; }
        public AppUser Owner { get; set; }
        //followed foreign key of AppUser
        [Required]
        [ForeignKey("Followed")]
        public Guid FollowedId { get; set; }
        public AppUser Followed { get; set; }
        //since value
        [DefaultValue("NOW()")]
        public DateTime FollowedAt { get; set; }
    }
}
