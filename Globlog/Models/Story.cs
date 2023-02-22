using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Globlog.Models
{
    public class Story
    {
        [Key]
        [DefaultValue("uuid_generate_v1()")]
        public Guid StoryId { get; set; }
        //owner foreign key of AppUser
        [Required]
        [ForeignKey("Owner")]
        public Guid OwnerId { get; set; }
        public AppUser Owner { get; set; }
        //title of the story
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        //updates of the story
        public ICollection<StoryUpdate> Updates { get; set; }
    }
}
