using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarDeal.Models
{   
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string Describtion { get; set; }

        public string CarBrand { get; set; }

        public string CarModel { get; set; }

        public string Category { get; set; }

        public string Region { get; set; }

        public DateTime Publish { get; set; }

        public string MainImage { get; set; }

        public string FrontImage { get; set; }

        public int Price { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public Post()
        {

        }

        public Post(int id, string carBrand, string carModel, string region, string mainImage, string frontImage, int price, string describtion, DateTime publish, int userId, User user)
        {
            Id = id;
            CarBrand = carBrand;
            CarModel = carModel;
            Region = region;
            MainImage = mainImage;
            FrontImage = frontImage;
            Price = price;
            Describtion = describtion;
            Publish = publish;
            UserId = userId;
            User = user;
        }
    }
}
