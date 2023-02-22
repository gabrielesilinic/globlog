using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Globlog.Models
{
    public class GenericLike<TLiked>
    {
        //serial id
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UpdateLikeId { get; set; }
        //story update foreign key of StoryUpdate
        [ForeignKey("Liked")]
        public Guid LikedId { get; set; }
        public TLiked Liked { get; set; }
        //owner foreign key of AppUser
        [ForeignKey("Owner")]
        public Guid OwnerId { get; set; }
        public AppUser Owner { get; set; }


    }
}
