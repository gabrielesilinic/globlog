using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Globlog.Models
{
    public class StoryUpdate
    {
        [Key]
        [DefaultValue("uuid_generate_v1()")]
        public Guid StoryUpdateId { get; set; }
        //story foreign key of Story
        [Required]
        [ForeignKey("FromStory")]
        public Guid StoryId { get; set; }
        public Story FromStory { get; set; }
        //owner foreign key of AppUser
        [Required]
        [ForeignKey("Owner")]
        public Guid OwnerId { get; set; }
        public AppUser Owner { get; set; }
        //title of the story
        [Required]
        [MaxLength(100)]
        public string StoryTitle { get; set; }
        //Body of the story
        [Required]
        [MaxLength(1000)]
        public string StoryBody { get; set; }
        //like count of the story
        [Range(0, int.MaxValue)]
        [DefaultValue(0)]
        public long LikeCount { get; set; }
    }
}
