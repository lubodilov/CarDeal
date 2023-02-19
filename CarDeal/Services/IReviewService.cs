using CarDeal.Models;
using CarDeal.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDeal.Services
{
    public interface IReviewService
    {
        void Edit(Review review);
        void Delete(int id);
        Review GetById(int id);
        ReviewDTO GetDtoById(int id);
        void Create(Review actor, User user);
        List<ReviewDTO> GetAll();
        List<ReviewDTO> GetUserReviews(int id);
        List<ReviewDTO> GetUserReviewsName(int id, string SearchPrase);
        List<ReviewDTO> GetUserReviewsStars(int id, string SearchPrase);
        List<ReviewDTO> GetReviewSortName(int id);
        List<ReviewDTO> GetReviewSortNameDesc(int id);
        List<ReviewDTO> GetReviewSortDescribtion(int id);
        List<ReviewDTO> GetReviewSortDescribtionDesc(int id);
        List<ReviewDTO> GetReviewSortStars(int id);
        List<ReviewDTO> GetReviewSortStarsDesc(int id);
        List<ReviewDTO> GetReviewSortPublish(int id);
        List<ReviewDTO> GetReviewSortPublishDesc(int id);
    }
}
