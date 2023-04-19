using CarDeal.Data;
using CarDeal.Models;
using CarDeal.Models.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CarDeal.Services
{
    //
    //Summary:
    //  Implements CRUD operations with the DB for the Class Post
    //
    public class PostService : IPostService
    {
        private UserDbContext dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public IFormFile MainImage { get; set; }
        public IFormFile Image1 { get; set; }

        string tempName;
        public PostService(UserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //
        //Summary:
        //  Creates a new post and ads it to the DB
        //
        public void Create(Post post, User user)
        {
            post.User = user;
            dbContext.Posts.Add(post);
            dbContext.SaveChanges();
        }
        //
        //Summary:
        //  Deletes a post found by its id and removes it from the DB
        //
        public void Delete(int id)
        {
            dbContext.Posts.Remove(GetById(id));
            dbContext.SaveChanges();
        }
        //
        //Summary:
        //  Edits a post and updates the DB
        //
        public void Edit(Post post)
        {
            Post oldPost = GetById(post.Id);
            oldPost.CarBrand = post.CarBrand;
            oldPost.CarModel = post.CarModel;
            oldPost.Category = post.Category;
            oldPost.Region = post.Region;
            oldPost.MainImage = post.MainImage;
            oldPost.FrontImage = post.FrontImage;
            oldPost.Price = post.Price;
            oldPost.Describtion= post.Describtion;
            oldPost.Publish = post.Publish;

            dbContext.SaveChanges();
        }
        //
        //Summary:
        //  Finds a post by Id
        //
        public Post GetById(int id)
        {
            return dbContext.Posts
                .FirstOrDefault(p => p.Id == id);
        }

        //
        //Summary:
        //  Finds a postDTO by Id
        //
        public PostDTO GetDtoById(int id)
        {
            return ToDto(dbContext.Posts
                .FirstOrDefault(p => p.Id == id));
        }

        //
        //Summary:
        //  Returns all posts in the DB
        //
        public List<PostDTO> GetAll()
        {
            return dbContext.Posts
                .Select(a => ToDto(a))
                .ToList();
        }
        //
        //Summary:
        //  Returns all posts having the given userId
        //
        public List<PostDTO> GetUserPosts(int id)
        {
            return dbContext.Posts
                .Where(p => p.UserId == id)
                .Select(p => ToDto(p))
                .ToList<PostDTO>();
        }

        public List<ReviewDTO> GetPostsReviews(int id)
        {
            List<ReviewDTO> ans = dbContext.Reviews
                .Where(p => p.PostId == id)
                .Select(p => ToDto(p))
                .ToList<ReviewDTO>();
            return ans;
        }
        //
        //Summary:
        //  Returns all posts having the given userId and Name
        //
        public List<PostDTO> GetUserPostsBrand(int id, string SearchPrase)
        {
            return dbContext.Posts
                .Where(p => p.UserId == id && p.CarBrand.Contains(SearchPrase))
                .Select(p => ToDto(p))
                .ToList<PostDTO>();
        }
        //
        //Summary:
        //  Returns all posts having the given userId and Stars
        //
        public List<PostDTO> GetUserPostsModel(int id, string SearchPrase)
        {
            return dbContext.Posts
                .Where(p => p.UserId == id && p.CarModel == SearchPrase)
                .Select(p => ToDto(p))
                .ToList<PostDTO>();
        }
        //
        //Summary:
        //  Returns list of all posts which are sorted in ascending order by Name
        //
        public List<PostDTO> GetPostSortCarBrand(int id)
        {
            return dbContext.Posts
                .Where(p => p.UserId == id)
                .OrderBy(p => p.CarBrand)
                .Select(p => ToDto(p))
                .ToList<PostDTO>();
        }
        //
        //Summary:
        //  Returns list of all posts which are sorted in descending order by Name
        //
        public List<PostDTO> GetPostSortCarBrandDesc(int id)
        {
            return dbContext.Posts
                .Where(p => p.UserId == id)
                .OrderByDescending(p => p.CarBrand)
                .Select(p => ToDto(p))
                .ToList<PostDTO>();
        }

        public List<PostDTO> GetPostSortCategory(int id)
        {
            return dbContext.Posts
                .Where(p => p.UserId == id)
                .OrderBy(p => p.Category)
                .Select(p => ToDto(p))
                .ToList<PostDTO>();
        }
        //
        //Summary:
        //  Returns list of all posts which are sorted in descending order by Name
        //
        public List<PostDTO> GetPostSortCategoryDesc(int id)
        {
            return dbContext.Posts
                .Where(p => p.UserId == id)
                .OrderByDescending(p => p.Category)
                .Select(p => ToDto(p))
                .ToList<PostDTO>();
        }

        public List<PostDTO> GetPostSortRegion(int id)
        {
            return dbContext.Posts
                .Where(p => p.UserId == id)
                .OrderBy(p => p.Region)
                .Select(p => ToDto(p))
                .ToList<PostDTO>();
        }
        //
        //Summary:
        //  Returns list of all posts which are sorted in descending order by Name
        //
        public List<PostDTO> GetPostSortRegionDesc(int id)
        {
            return dbContext.Posts
                .Where(p => p.UserId == id)
                .OrderByDescending(p => p.Region)
                .Select(p => ToDto(p))
                .ToList<PostDTO>();
        }

        public List<PostDTO> GetPostSortMainImage(int id)
        {
            return dbContext.Posts
                .Where(p => p.UserId == id)
                .OrderBy(p => p.MainImage)
                .Select(p => ToDto(p))
                .ToList<PostDTO>();
        }
        //
        //Summary:
        //  Returns list of all posts which are sorted in descending order by Name
        //
        public List<PostDTO> GetPostSortMainImageDesc(int id)
        {
            return dbContext.Posts
                .Where(p => p.UserId == id)
                .OrderByDescending(p => p.MainImage)
                .Select(p => ToDto(p))
                .ToList<PostDTO>();
        }

        public List<PostDTO> GetPostSortDescribtion(int id)
        {
            return dbContext.Posts
                .Where(p => p.UserId == id)
                .OrderBy(p => p.Describtion)
                .Select(p => ToDto(p))
                .ToList<PostDTO>();
        }
        public List<PostDTO> GetPostSortDescribtionDesc(int id)
        {
            return dbContext.Posts
                .Where(p => p.UserId == id)
                .OrderByDescending(p => p.Describtion)
                .Select(p => ToDto(p))
                .ToList<PostDTO>();
        }
        public List<PostDTO> GetPostSortCarModel(int id)
        {
            return dbContext.Posts
                .Where(p => p.UserId == id)
                .OrderBy(p => p.CarModel)
                .Select(p => ToDto(p))
                .ToList<PostDTO>();
        }
        public List<PostDTO> GetPostSortCarModelDesc(int id)
        {
            return dbContext.Posts
                .Where(p => p.UserId == id)
                .OrderByDescending(p => p.CarModel)
                .Select(p => ToDto(p))
                .ToList<PostDTO>();
        }
        public List<PostDTO> GetPostSortFrontImage(int id)
        {
            return dbContext.Posts
                .Where(p => p.UserId == id)
                .OrderBy(p => p.FrontImage)
                .Select(p => ToDto(p))
                .ToList<PostDTO>();
        }
        public List<PostDTO> GetPostSortFrontImageDesc(int id)
        {
            return dbContext.Posts
                .Where(p => p.UserId == id)
                .OrderByDescending(p => p.FrontImage)
                .Select(p => ToDto(p))
                .ToList<PostDTO>();
        }
        public List<PostDTO> GetPostSortPrice(int id)
        {
            return dbContext.Posts
                .Where(p => p.UserId == id)
                .OrderBy(p => p.Price)
                .Select(p => ToDto(p))
                .ToList<PostDTO>();
        }
        public List<PostDTO> GetPostSortPriceDesc(int id)
        {
            return dbContext.Posts
                .Where(p => p.UserId == id)
                .OrderByDescending(p => p.Price)
                .Select(p => ToDto(p))
                .ToList<PostDTO>();
        }
        public List<PostDTO> GetPostSortPublish(int id)
        {
            return dbContext.Posts
                .Where(p => p.UserId == id)
                .OrderBy(p => p.Publish)
                .Select(p => ToDto(p))
                .ToList<PostDTO>();
        }
        public List<PostDTO> GetPostSortPublishDesc(int id)
        {
            return dbContext.Posts
                .Where(p => p.UserId == id)
                .OrderByDescending(p => p.Publish)
                .Select(p => ToDto(p))
                .ToList<PostDTO>();
        }

        
        private static PostDTO ToDto(Post t)
        {
            PostDTO post = new PostDTO();

            post.Id = t.Id;
            post.CarBrand = t.CarBrand;
            post.CarModel = t.CarModel;
            post.Category = t.Category;
            post.Region = t.Region;
            post.MainImage = t.MainImage;
            post.FrontImage = t.FrontImage;
            post.Price = t.Price;
            post.Describtion = t.Describtion;
            post.Publish = t.Publish;
            if (t.User != null)
            {
                post.CreatedBy = $"{t.User.FirstName} {t.User.LastName}";
                post.UserEmail = t.User.Email;
            }

            return post;
        }
        private static ReviewDTO ToDto(Review t)
        {
            ReviewDTO review = new ReviewDTO();

            review.Id = t.Id;
            review.Name = t.Name;
            review.Describtion = t.Describtion;
            review.Stars = t.Stars;
            review.Publish = t.Publish;
            if (t.User != null)
            {
                review.CreatedBy = $"{t.User.FirstName} {t.User.LastName}";
                review.UserEmail = t.User.Email;
            }
            if(t.Post != null)
            {
                review.Post = t.Post;
            }

            return review;
        }
    }
}
