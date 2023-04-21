using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using CarDeal.Data;
using CarDeal.Models;
using CarDeal.Services;
using CarDeal.Models.DTOs;

namespace Tests
{
    [TestFixture]
    public class ReviewServiceTests
    {
        private ReviewService reviewService;

        private UserDbContext context;

        private Review review;
        private int userId = 1;

        [SetUp]
        public void Setup()
        {

            var options = new DbContextOptionsBuilder<UserDbContext>()
               .UseInMemoryDatabase("TestDb").Options;

            this.context = new UserDbContext(options);
            reviewService = new ReviewService(this.context);

            DateTime today = DateTime.Today;
            this.review = CreateReview(1, "Work", "description", "1", today, userId);

            context.Reviews.Add(review);
            context.SaveChanges();

        }

        [Test]
        public void TestCreate()
        {
            DateTime today = DateTime.Today;
            Review review = CreateReview(2, "Work 2", "description 2", "2", today, userId);
            User user = new User();

            reviewService.Create(review, user);

            Review reviewDB = context.Reviews.FirstOrDefault(p => p.Id == review.Id);

            Assert.NotNull(reviewDB);
        }

        [Test]
        public void TestEdit()
        {
            DateTime today = DateTime.Today;
            Review review = CreateReview(1, "Work 1", "description 1", "1", today, userId);

            reviewService.Edit(review);

            Review reviewDB = context.Reviews.FirstOrDefault(p => p.Id == review.Id);

            Assert.NotNull(reviewDB);
            Assert.AreEqual(reviewDB.Describtion, "description 1");

        }

        [Test]
        public void TestDelete()
        {
            reviewService.Delete(review.Id);

            Review reviewDB = context.Reviews.FirstOrDefault(p => p.Id == review.Id);

            Assert.Null(reviewDB);
        }


        [Test]
        public void TestGetAll()
        {
            DateTime today = DateTime.Today;
            Review review2 = CreateReview(2, "Work 2", "description 2", "2", today, userId);
            Review review3 = CreateReview(3, "Work 3", "description 3", "3", today, userId);
            User user = new User();

            reviewService.Create(review2, user);
            reviewService.Create(review3, user);

            List<ReviewDTO> reviews = reviewService.GetAll();

            Assert.AreEqual(3, reviews.Count);
            Assert.AreEqual("Work 2", reviews[1].Name);

        }


        [Test]
        public void TestGetById()
        {

            Review reviewDB = reviewService.GetById(review.Id);

            Assert.AreEqual(reviewDB.Describtion, "description");
        }


        [Test]
        public void TestGetUserReviews()
        {
            DateTime today = DateTime.Today;
            Review review2 = CreateReview(2, "Work 2", "description 2", "2", today, 2);
            Review review3 = CreateReview(3, "Work 3", "description 3", "3", today, userId);
            User user = new User();

            reviewService.Create(review2, user);
            reviewService.Create(review3, user);

            List<ReviewDTO> reviews = reviewService.GetUserReviews(userId);


            Assert.AreEqual(3, reviews.Count);
            Assert.AreEqual("Work", reviews[0].Name);
        }


        [Test]
        public void TestGetReviewByName()
        {
            DateTime today = DateTime.Today;
            Review review5 = CreateReview(5, "Work 5", "describtion 5", "5", today, userId);
            Review review6 = CreateReview(6, "Work 6", "describtion 6", "1", today, userId);
            Review review7 = CreateReview(7, "Work 7", "describtion 7", "5", today, userId);
            User user = new User();

            reviewService.Create(review5, user);
            reviewService.Create(review6, user);
            reviewService.Create(review7, user);
            string name = review6.Name;
            List<ReviewDTO> reviews = reviewService.GetUserReviewsName(userId, name);


            Assert.AreEqual(1, reviews.Count);
            Assert.AreEqual("1", reviews[0].Stars);
        }

        [Test]
        public void TestGetReviewByStars()
        {
            DateTime today = DateTime.Today;
            Review review5 = CreateReview(5, "Work 5", "describtion 5", "5", today, userId);
            Review review6 = CreateReview(6, "Work 6", "describtion 6", "4", today, userId);
            Review review7 = CreateReview(7, "Work 7", "describtion 7", "3", today, userId);
            User user = new User();

            reviewService.Create(review5, user);
            reviewService.Create(review6, user);
            reviewService.Create(review7, user);
            string name = review6.Stars;
            List<ReviewDTO> reviews = reviewService.GetUserReviewsStars(userId, name);


            Assert.AreEqual(1, reviews.Count);
            Assert.AreEqual("Work 6", reviews[0].Name);
        }

        [Test]
        public void TestSortReviewByName()
        {
            DateTime today = DateTime.Today;
            Review review5 = CreateReview(5, "AAA", "describtion 5", "1", today, userId);
            Review review6 = CreateReview(6, "CCC", "describtion 6", "3", today, userId);
            Review review7 = CreateReview(7, "BBB", "describtion 7", "2", today, userId);
            User user = new User();

            reviewService.Create(review5, user);
            reviewService.Create(review6, user);
            reviewService.Create(review7, user);

            List<ReviewDTO> reviews = reviewService.GetReviewSortName(userId);


            Assert.AreEqual("1", reviews[0].Stars);
            Assert.AreEqual("2", reviews[1].Stars);
            Assert.AreEqual("3", reviews[2].Stars);
        }

        [Test]
        public void TestSortReviewByNameDesc()
        {
            DateTime today = DateTime.Today;
            Review review5 = CreateReview(5, "XXX", "describtion 5", "1", today, userId);
            Review review6 = CreateReview(6, "ZZZ", "describtion 6", "3", today, userId);
            Review review7 = CreateReview(7, "YYY", "describtion 7", "2", today, userId);
            User user = new User();

            reviewService.Create(review5, user);
            reviewService.Create(review6, user);
            reviewService.Create(review7, user);

            List<ReviewDTO> reviews = reviewService.GetReviewSortNameDesc(userId);


            //Assert.AreEqual(3, reviews.Count);
            Assert.AreEqual("ZZZ", reviews[0].Name);
            Assert.AreEqual("YYY", reviews[1].Name);
            Assert.AreEqual("XXX", reviews[2].Name);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Database.EnsureDeleted();
        }


        private Review CreateReview(int id, string name, string describtion, string stars, DateTime publish, int userId)
        {
            Review review = new Review();
            review.Id = id;
            review.Name = name;
            review.Describtion = describtion;
            review.Stars = stars;
            review.Publish = publish;
            review.UserId = userId;


            return review;
        }

    }
}