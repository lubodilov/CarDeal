using CarDeal.Models;
using CarDeal.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDeal.Services
{
    public interface IPostService
    {
        void Edit(Post post);
        void Delete(int id);
        Post GetById(int id);
        PostDTO GetDtoById(int id);
        void Create(Post actor, User user);
        List<PostDTO> GetAll();
        List<PostDTO> GetUserPosts(int id);
        List<ReviewDTO> GetPostsReviews(int id);
        List<PostDTO> GetUserPostsBrand(int id, string SearchPrase);
        List<PostDTO> GetUserPostsModel(int id, string SearchPrase);
        List<PostDTO> GetPostSortCarBrand(int id);
        List<PostDTO> GetPostSortCarBrandDesc(int id);
        List<PostDTO> GetPostSortCarModel(int id);
        List<PostDTO> GetPostSortCarModelDesc(int id);
        List<PostDTO> GetPostSortCategory(int id);
        List<PostDTO> GetPostSortCategoryDesc(int id);
        List<PostDTO> GetPostSortRegion(int id);
        List<PostDTO> GetPostSortRegionDesc(int id);
        List<PostDTO> GetPostSortDescribtion(int id);
        List<PostDTO> GetPostSortDescribtionDesc(int id);
        List<PostDTO> GetPostSortFrontImage(int id);
        List<PostDTO> GetPostSortFrontImageDesc(int id);
        List<PostDTO> GetPostSortPrice(int id);
        List<PostDTO> GetPostSortPriceDesc(int id);
        List<PostDTO> GetPostSortMainImage(int id);
        List<PostDTO> GetPostSortMainImageDesc(int id);
        List<PostDTO> GetPostSortPublish(int id);
        List<PostDTO> GetPostSortPublishDesc(int id);
    }
}
