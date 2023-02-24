using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarDeal.Models.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The describtion is required.")]
        public string Describtion { get; set; }

        public string CarBrand { get; set; }

        public string CarModel { get; set; }

        public string Category { get; set; }

        public string Region { get; set; }

        public DateTime Publish { get; set; }

        public string MainImage { get; set; }

        public string FrontImage { get; set; }

        public int Price { get; set; }

        public string CreatedBy { get; set; }

        public string UserEmail { get; set; }

        public PostDTO()
        {
                
        }

        public PostDTO(int id, string carBrand, string carModel, string region, string mainImage, string frontImage, int price, string describtion, DateTime publish, string createdBy, string userEmail)
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
            CreatedBy = createdBy;
            UserEmail = userEmail;
        }
    }
}
