using CarDeal.Models;
using CarDeal.Models.DTOs;
using CarDeal.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDeal.Controllers
{
    public class ReviewController : Controller
    {
        private IReviewService reviewService;
        private UserManager<User> userManager;
        public ReviewController(IReviewService reviewService, UserManager<User> userManager)
        {
            this.reviewService = reviewService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            List<ReviewDTO> reviews = reviewService.GetAll();

            return View(reviews);
        }
        public async Task<IActionResult> UserReviews()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            List<ReviewDTO> reviews = reviewService.GetUserReviews(user.Id);
            return View(reviews);
        }

        public IActionResult ShowSearchForm()
        {
            return View();
        }

        public async Task<IActionResult> ShowSearchResultName(String SearchPhrase)
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            List<ReviewDTO> reviews = reviewService.GetUserReviewsName(user.Id, SearchPhrase);
            return View(reviews);
        }

        public async Task<IActionResult> ShowSearchResultStars(String SearchPhrase)
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            List<ReviewDTO> reviews = reviewService.GetUserReviewsStars(user.Id, SearchPhrase);
            return View(reviews);
        }

        public async Task<IActionResult> SortName()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
 
            return View(reviewService.GetReviewSortName(user.Id));
        }

        public async Task<IActionResult> SortNameDesc()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);

            return View(reviewService.GetReviewSortNameDesc(user.Id));
        }
        public async Task<IActionResult> SortDescribtion()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);

            return View(reviewService.GetReviewSortDescribtion(user.Id));
        }
        public async Task<IActionResult> SortDescribtionDesc()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            
            return View(reviewService.GetReviewSortDescribtionDesc(user.Id));
        }

        public async Task<IActionResult> SortStars()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);

            return View(reviewService.GetReviewSortStars(user.Id));
        }
        public async Task<IActionResult> SortStarsDesc()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);

            return View(reviewService.GetReviewSortStarsDesc(user.Id));
        }

        public async Task<IActionResult> SortPublish()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);

            return View(reviewService.GetReviewSortPublish(user.Id));
        }
        public async Task<IActionResult> SortPublishDesc()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);

            return View(reviewService.GetReviewSortPublishDesc(user.Id));
        }

        public IActionResult Details(int id)
        {
            ReviewDTO review = reviewService.GetDtoById(id);
            return View(review);
        }
        public IActionResult Edit()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditAsync(int id)
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            Review review = reviewService.GetById(id);
            if (user is null)
            {
                return RedirectToAction(nameof(UserReviews));
            }
            if (review.UserId != user.Id)
            {
                return RedirectToAction(nameof(UserReviews));
            }
            return View(reviewService.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(int id, Review review)
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            if (user is null)
            {
                return RedirectToAction(nameof(UserReviews));
            }
            if (reviewService.GetById(id).UserId != user.Id)
            {
                return RedirectToAction(nameof(UserReviews));
            }
            reviewService.Edit(review);
            return RedirectToAction(nameof(UserReviews));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Review review)
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            if (user is null)
            {
                return RedirectToAction(nameof(UserReviews));
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            reviewService.Create(review, user);
            return RedirectToAction(nameof(UserReviews));
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            Review review = reviewService.GetById(id);
            if (user is null)
            {
                return RedirectToAction(nameof(UserReviews));
            }
            if (review.UserId != user.Id)
            {
                return RedirectToAction(nameof(UserReviews));
            }
            return View(review);
        }
        public async Task<IActionResult> ConfirmDeleteAsync(int id)
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            Review review = reviewService.GetById(id);
            if (user is null)
            {
                return RedirectToAction(nameof(UserReviews));
            }
            if (review.UserId != user.Id)
            {
                return RedirectToAction(nameof(UserReviews));
            }
            reviewService.Delete(id);
            return RedirectToAction(nameof(UserReviews));
        }
        [HttpGet]
        public IActionResult ViewReview(int id)
        {
            return View(reviewService.GetById(id));
        }
    }
}
