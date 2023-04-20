using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarDeal.Models.DTOs
{
    public class ReviewDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The describtion is required.")]
        public string Describtion { get; set; }

        public string Stars { get; set; }

        public DateTime Publish { get; set; }

        public string CreatedBy { get; set; }

        public string UserEmail { get; set; }

        public Post Post { get; set; }

        //public int PostId { get; set; }


        public ReviewDTO()
        {
                
        }

        public ReviewDTO(int id, string name, string describtion, string stars, DateTime publish, string createdBy, string userEmail, Post post)
        {
            Id = id;
            Name = name;
            Describtion = describtion;
            Stars = stars;
            Publish = publish;
            CreatedBy = createdBy;
            UserEmail = userEmail;
            Post = post;
        }
    }
}
