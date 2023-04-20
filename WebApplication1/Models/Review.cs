using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarDeal.Models
{   
    public class Review
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string Describtion { get; set; }

        [Required]
        [RegularExpression("^[1-5]$", ErrorMessage = "Stars must be a single digit between 1 and 5.")]
        public string Stars { get; set; }

        public DateTime Publish { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public Review()
        {

        }

        public Review(int id, string name, string describtion, string stars, DateTime publish, int userId, User user, Post post)
        {
            Id = id;
            Name = name;
            Describtion = describtion;
            Stars = stars;
            Publish = publish;
            UserId = userId;
            User = user;
            Post = post;
        }
    }
}
