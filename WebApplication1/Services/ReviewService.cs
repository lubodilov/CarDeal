using CarDeal.Data;
using CarDeal.Models;
using CarDeal.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDeal.Services
{
    //
    //Summary:
    //  Implements CRUD operations with the DB for the Class Review
    //
    public class ReviewService : IReviewService
    {
        private UserDbContext dbContext;
        public ReviewService(UserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //
        //Summary:
        //  Creates a new review and ads it to the DB
        //
        public void Create(Review review, User user)
        {
            try
            {
                review.User = user;
                dbContext.Reviews.Add(review);
                dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Log the inner exception details
                Console.WriteLine($"Error: {ex.InnerException?.Message}");
                throw;
            }
        }
        //
        //Summary:
        //  Deletes a review found by its id and removes it from the DB
        //
        public void Delete(int id)
        {
            dbContext.Reviews.Remove(GetById(id));
            dbContext.SaveChanges();
        }
        //
        //Summary:
        //  Edits a review and updates the DB
        //
        public void Edit(Review review)
        {
            Review oldReview = GetById(review.Id);
            oldReview.Name = review.Name;
            oldReview.Describtion= review.Describtion;
            oldReview.Stars = review.Stars;
            oldReview.Publish = review.Publish;

            dbContext.SaveChanges();
        }
        //
        //Summary:
        //  Finds a review by Id
        //
        public Review GetById(int id)
        {
            return dbContext.Reviews
                .FirstOrDefault(p => p.Id == id);
        }

        //
        //Summary:
        //  Finds a reviewDTO by Id
        //
        public ReviewDTO GetDtoById(int id)
        {
            return ToDto(dbContext.Reviews
                .FirstOrDefault(p => p.Id == id));
        }

        //
        //Summary:
        //  Returns all reviews in the DB
        //
        public List<ReviewDTO> GetAll()
        {
            return dbContext.Reviews
                .Select(a => ToDto(a))
                .ToList();
        }
        //
        //Summary:
        //  Returns all reviews having the given userId
        //
        public List<ReviewDTO> GetUserReviews(int id)
        {
            return dbContext.Reviews
                .Where(p => p.UserId == id)
                .Select(p => ToDto(p))
                .ToList<ReviewDTO>();
        }
        //
        //Summary:
        //  Returns all reviews having the given userId and Name
        //
        public List<ReviewDTO> GetUserReviewsName(int id, string SearchPrase)
        {
            return dbContext.Reviews
                .Where(p => p.UserId == id && p.Name.Contains(SearchPrase))
                .Select(p => ToDto(p))
                .ToList<ReviewDTO>();
        }
        //
        //Summary:
        //  Returns all reviews having the given userId and Stars
        //
        public List<ReviewDTO> GetUserReviewsStars(int id, string SearchPrase)
        {
            return dbContext.Reviews
                .Where(p => p.UserId == id && p.Stars == SearchPrase)
                .Select(p => ToDto(p))
                .ToList<ReviewDTO>();
        }
        //
        //Summary:
        //  Returns list of all reviews which are sorted in ascending order by Name
        //
        public List<ReviewDTO> GetReviewSortName(int id)
        {
            return dbContext.Reviews
                .Where(p => p.UserId == id)
                .OrderBy(p => p.Name)
                .Select(p => ToDto(p))
                .ToList<ReviewDTO>();
        }
        //
        //Summary:
        //  Returns list of all reviews which are sorted in descending order by Name
        //
        public List<ReviewDTO> GetReviewSortNameDesc(int id)
        {
            return dbContext.Reviews
                .Where(p => p.UserId == id)
                .OrderByDescending(p => p.Name)
                .Select(p => ToDto(p))
                .ToList<ReviewDTO>();
        }
        public List<ReviewDTO> GetReviewSortDescribtion(int id)
        {
            return dbContext.Reviews
                .Where(p => p.UserId == id)
                .OrderBy(p => p.Describtion)
                .Select(p => ToDto(p))
                .ToList<ReviewDTO>();
        }
        public List<ReviewDTO> GetReviewSortDescribtionDesc(int id)
        {
            return dbContext.Reviews
                .Where(p => p.UserId == id)
                .OrderByDescending(p => p.Describtion)
                .Select(p => ToDto(p))
                .ToList<ReviewDTO>();
        }
        public List<ReviewDTO> GetReviewSortStars(int id)
        {
            return dbContext.Reviews
                .Where(p => p.UserId == id)
                .OrderBy(p => p.Stars)
                .Select(p => ToDto(p))
                .ToList<ReviewDTO>();
        }
        public List<ReviewDTO> GetReviewSortStarsDesc(int id)
        {
            return dbContext.Reviews
                .Where(p => p.UserId == id)
                .OrderByDescending(p => p.Stars)
                .Select(p => ToDto(p))
                .ToList<ReviewDTO>();
        }
        public List<ReviewDTO> GetReviewSortPublish(int id)
        {
            return dbContext.Reviews
                .Where(p => p.UserId == id)
                .OrderBy(p => p.Publish)
                .Select(p => ToDto(p))
                .ToList<ReviewDTO>();
        }
        public List<ReviewDTO> GetReviewSortPublishDesc(int id)
        {
            return dbContext.Reviews
                .Where(p => p.UserId == id)
                .OrderByDescending(p => p.Publish)
                .Select(p => ToDto(p))
                .ToList<ReviewDTO>();
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

            return review;
        }

    }
}
