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
    public class PostServiceTests
    {
        private PostService postService;
        private ReviewService reviewService;

        private UserDbContext context;

        private Post post;
        private int userId = 1;

        [SetUp]
        public void Setup()
        {

            var options = new DbContextOptionsBuilder<UserDbContext>()
               .UseInMemoryDatabase("TestDb").Options;

            this.context = new UserDbContext(options);
            postService = new PostService(this.context);
            reviewService = new ReviewService(this.context);

            DateTime today = DateTime.Today;
            this.post = CreatePost(1, "description", "Opel", "CarModel", "Region", "MainImage", "FrontImage", 1000, today, userId);

            context.Posts.Add(post);
            context.SaveChanges();

        }

        [Test]
        public void TestCreate()
        {
            DateTime today = DateTime.Today;
            Post post = CreatePost(2, "description 2", "carbrand2", "CarModel2", "Region2", "MainImage2", "FrontImage2", 2000, today, userId);
            User user = new User();

            postService.Create(post, user);

            Post postDB = context.Posts.FirstOrDefault(p => p.Id == post.Id);

            Assert.NotNull(postDB);
        }

        [Test]
        public void TestEdit()
        {
            DateTime today = DateTime.Today;
            Post post = CreatePost(1, "description", "carbrand", "CarModel", "Region", "MainImage", "FrontImage", 2000, today, userId);

            postService.Edit(post);

            Post postDB = context.Posts.FirstOrDefault(p => p.Id == post.Id);

            Assert.NotNull(postDB);
            Assert.AreEqual(postDB.Describtion, "description");

        }

        [Test]
        public void TestDelete()
        {
            postService.Delete(post.Id);

            Post postDB = context.Posts.FirstOrDefault(p => p.Id == post.Id);

            Assert.Null(postDB);
        }


        [Test]
        public void TestGetAll()
        {
            DateTime today = DateTime.Today;
            Post post2 = CreatePost(2, "description 2", "carbrand2", "CarModel2", "Region2", "MainImage2", "FrontImage2", 2000, today, userId);
            Post post3 = CreatePost(3, "description 3", "carbrand3", "CarModel3", "Region3", "MainImage3", "FrontImage3", 3000, today, userId);
            User user = new User();

            postService.Create(post2, user);
            postService.Create(post3, user);

            List<PostDTO> posts = postService.GetAll();

            Assert.AreEqual(3, posts.Count);
            Assert.AreEqual("description 2", posts[1].Describtion);

        }


        [Test]
        public void TestGetById()
        {

            Post postDB = postService.GetById(post.Id);

            Assert.AreEqual(postDB.Describtion, "description");
        }


        [Test]
        public void TestGetUserPosts()
        {
            DateTime today = DateTime.Today;
            Post post2 = CreatePost(2, "description 2", "carbrand2", "CarModel2", "Region2", "MainImage2", "FrontImage2", 2000, today, userId);
            Post post3 = CreatePost(3, "description 3", "carbrand3", "CarModel3", "Region3", "MainImage3", "FrontImage3", 3000, today, userId);
            User user = new User();

            postService.Create(post2, user);
            postService.Create(post3, user);

            List<PostDTO> posts = postService.GetUserPosts(userId);


            Assert.AreEqual(3, posts.Count);
            Assert.AreEqual("CarModel", posts[0].CarModel);
        }

        [Test]
        public void TestGetPostsReviews()
        {
            DateTime today = DateTime.Today;
            Post post2 = CreatePost(2, "description 2", "carbrand2", "CarModel2", "Region2", "MainImage2", "FrontImage2", 2000, today, userId);
            Post post3 = CreatePost(3, "description 3", "carbrand3", "CarModel3", "Region3", "MainImage3", "FrontImage3", 3000, today, userId);
            User user = new User();
            postService.Create(post2, user);
            postService.Create(post3, user);

            Review review5 = CreateReview(5, "Name", "describtion 5", "5", today, userId, 2);
            Review review6 = CreateReview(6, "Work 6", "describtion 6", "1", today, userId, 2);
            Review review7 = CreateReview(7, "Work 7", "describtion 7", "5", today, userId, 2);
            reviewService.Create(review5, user);
            reviewService.Create(review6, user);
            reviewService.Create(review7, user);


            List<ReviewDTO> reviews = postService.GetPostsReviews(2);


            Assert.AreEqual(3, reviews.Count);
            Assert.AreEqual("Name", reviews[0].Name);
            Assert.AreEqual("Work 6", reviews[1].Name);
            Assert.AreEqual("Work 7", reviews[2].Name);
        }


        [Test]
        public void TestGetPostByCarBrand()
        {
            DateTime today = DateTime.Today;
            Post post5 = CreatePost(5, "description 5", "carbrand5", "CarModel5", "Region5", "MainImage2", "FrontImage2", 2000, today, userId);
            Post post6 = CreatePost(6, "description 6", "carbrand6", "CarModel6", "Region6", "MainImage2", "FrontImage2", 3000, today, userId);
            Post post7 = CreatePost(7, "description 7", "carbrand7", "CarModel7", "Region7", "MainImage2", "FrontImage2", 2000, today, userId);
            User user = new User();

            postService.Create(post5, user);
            postService.Create(post6, user);
            postService.Create(post7, user);
            string brand = post6.CarBrand;
            List<PostDTO> posts = postService.GetUserPostsBrand(userId, brand);


            Assert.AreEqual(1, posts.Count);
            Assert.AreEqual(3000, posts[0].Price);
        }

        [Test]
        public void TestGetPostByModel()
        {
            DateTime today = DateTime.Today;
            Post post5 = CreatePost(5, "description 5", "carbrand5", "CarModel5", "Region5", "MainImage2", "FrontImage2", 2000, today, userId);
            Post post6 = CreatePost(6, "description 6", "carbrand6", "CarModel6", "Region6", "MainImage2", "FrontImage2", 3000, today, userId);
            Post post7 = CreatePost(7, "description 7", "carbrand7", "CarModel7", "Region7", "MainImage2", "FrontImage2", 2000, today, userId);
            User user = new User();

            postService.Create(post5, user);
            postService.Create(post6, user);
            postService.Create(post7, user);
            string name = post6.CarModel;
            List<PostDTO> posts = postService.GetUserPostsModel(userId, name);


            Assert.AreEqual(1, posts.Count);
            Assert.AreEqual("Region6", posts[0].Region);
        }

        [Test]
        public void TestSortPostByCarBrand()
        {
            DateTime today = DateTime.Today;
            Post post5 = CreatePost(5, "description 5", "AAAA", "CarModel5", "Region5", "MainImage2", "FrontImage2", 5000, today, userId);
            Post post6 = CreatePost(6, "description 6", "CCCC", "CarModel6", "Region6", "MainImage2", "FrontImage2", 7000, today, userId);
            Post post7 = CreatePost(7, "description 7", "BBBB", "CarModel7", "Region7", "MainImage2", "FrontImage2", 6000, today, userId);
            User user = new User();

            postService.Create(post5, user);
            postService.Create(post6, user);
            postService.Create(post7, user);

            List<PostDTO> posts = postService.GetPostSortCarBrand(userId);


            Assert.AreEqual(5000, posts[0].Price);
            Assert.AreEqual(6000, posts[1].Price);
            Assert.AreEqual(7000, posts[2].Price);
        }

        [Test]
        public void TestSortPostByCarBrandDesc()
        {
            DateTime today = DateTime.Today;
            Post post5 = CreatePost(5, "description 5", "XXXX", "CarModel5", "Region5", "MainImage2", "FrontImage2", 1000, today, userId);
            Post post6 = CreatePost(6, "description 6", "ZZZZ", "CarModel6", "Region6", "MainImage2", "FrontImage2", 3000, today, userId);
            Post post7 = CreatePost(7, "description 7", "YYYY", "CarModel7", "Region7", "MainImage2", "FrontImage2", 2000, today, userId);
            User user = new User();

            postService.Create(post5, user);
            postService.Create(post6, user);
            postService.Create(post7, user);

            List<PostDTO> posts = postService.GetPostSortCarBrandDesc(userId);


            Assert.AreEqual("ZZZZ", posts[0].CarBrand);
            Assert.AreEqual("YYYY", posts[1].CarBrand);
            Assert.AreEqual("XXXX", posts[2].CarBrand);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Database.EnsureDeleted();
        }


        private Post CreatePost(int id, string describtion, string carBrand, string carModel, string region, string mainImage, string frontImage, int price, DateTime publish, int userId)
        {
            Post post = new Post();
            post.Id = id;
            post.Describtion = describtion;
            post.CarBrand = carBrand;
            post.CarModel = carModel;
            post.Region = region;
            post.MainImage = mainImage;
            post.FrontImage = frontImage;
            post.Price = price;
            post.Publish = publish;
            post.UserId = userId;


            return post;
        }

        private Review CreateReview(int id, string name, string describtion, string stars, DateTime publish, int userId, int postId)
        {
            Review review = new Review();
            review.Id = id;
            review.Name = name;
            review.Describtion = describtion;
            review.Stars = stars;
            review.Publish = publish;
            review.UserId = userId;
            review.PostId = postId;


            return review;
        }

    }
}