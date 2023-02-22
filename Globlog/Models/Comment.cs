using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Globlog.Models
{
    public class Comment
    {
        //uuid v1
        [Key]
        [DefaultValue("uuid_generate_v1()")]
        public Guid CommentId { get; set; }
        //parent comment foreign key of Comment
        [ForeignKey("ParentComment")]
        public Guid? ParentCommentId { get; set; }
        public Comment? ParentComment { get; set; }
        //story update foreign key of StoryUpdate
        [Required]
        [ForeignKey("CommentedUpdate")]
        public Guid StoryUpdateId { get; set; }
        public StoryUpdate CommentedUpdate { get; set; }
        //owner foreign key of AppUser
        [Required]
        [ForeignKey("Owner")]
        public Guid OwnerId { get; set; }
        public AppUser Owner { get; set; }
        //comment body
        [Required]
        [MaxLength(1000)]
        public string CommentBody { get; set; }

    }
}
