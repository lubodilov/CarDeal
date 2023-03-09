using CarDeal.Models;
using CarDeal.Models.DTOs;
using CarDeal.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDeal.Controllers
{
    public class PostController : Controller
    {
        private IPostService postService;
        private UserManager<User> userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public IFormFile MainImage { get; set; }
        public IFormFile Image1 { get; set; }

        //string tempName;
        public PostController(IPostService postService, UserManager<User> userManager)
        {
            this.postService = postService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            List<PostDTO> posts = postService.GetAll();

            return View(posts);
        }
        public async Task<IActionResult> UserPosts()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            List<PostDTO> posts = postService.GetUserPosts(user.Id);
            return View(posts);
        }

        public IActionResult ShowSearchForm()
        {
            return View();
        }

        public async Task<IActionResult> ShowSearchResultCarBrand(String SearchPhrase)
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            List<PostDTO> posts = postService.GetUserPostsBrand(user.Id, SearchPhrase);
            return View(posts);
        }

        public async Task<IActionResult> ShowSearchResultCarModel(String SearchPhrase)
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            List<PostDTO> posts = postService.GetUserPostsModel(user.Id, SearchPhrase);
            return View(posts);
        }

        public async Task<IActionResult> SortCarModel()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
 
            return View(postService.GetPostSortCarModel(user.Id));
        }
        public async Task<IActionResult> SortCarModelDesc()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);

            return View(postService.GetPostSortCarModelDesc(user.Id));
        }

        public async Task<IActionResult> SortDescribtion()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);

            return View(postService.GetPostSortDescribtion(user.Id));
        }
        public async Task<IActionResult> SortDescribtionDesc()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            
            return View(postService.GetPostSortDescribtionDesc(user.Id));
        }

        public async Task<IActionResult> SortCategory()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);

            return View(postService.GetPostSortCategory(user.Id));
        }
        public async Task<IActionResult> SortCategoryDesc()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);

            return View(postService.GetPostSortCategoryDesc(user.Id));
        }

        public async Task<IActionResult> SortRegion()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);

            return View(postService.GetPostSortRegion(user.Id));
        }
        public async Task<IActionResult> SortRegionDesc()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);

            return View(postService.GetPostSortRegionDesc(user.Id));
        }

        public async Task<IActionResult> SortPrice()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);

            return View(postService.GetPostSortPrice(user.Id));
        }
        public async Task<IActionResult> SortPriceDesc()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);

            return View(postService.GetPostSortPriceDesc(user.Id));
        }
        
        public async Task<IActionResult> SortCarBrand()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);

            return View(postService.GetPostSortCarBrand(user.Id));
        }
        public async Task<IActionResult> SortCarBrandDesc()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);

            return View(postService.GetPostSortCarBrandDesc(user.Id));
        }

        public async Task<IActionResult> SortPublish()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);

            return View(postService.GetPostSortPublish(user.Id));
        }
        public async Task<IActionResult> SortPublishDesc()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);

            return View(postService.GetPostSortPublishDesc(user.Id));
        }

        public IActionResult Details(int id)
        {
            PostDTO post = postService.GetDtoById(id);
            return View(post);
        }
        public IActionResult Edit()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditAsync(int id)
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            Post post = postService.GetById(id);
            if (user is null)
            {
                return RedirectToAction(nameof(UserPosts));
            }
            if (post.UserId != user.Id)
            {
                return RedirectToAction(nameof(UserPosts));
            }
            return View(postService.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(int id, Post post)
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            if (user is null)
            {
                return RedirectToAction(nameof(UserPosts));
            }
            if (postService.GetById(id).UserId != user.Id)
            {
                return RedirectToAction(nameof(UserPosts));
            }
            postService.Edit(post);
            return RedirectToAction(nameof(UserPosts));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Post post)
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            if (user is null)
            {
                return RedirectToAction(nameof(UserPosts));
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            postService.Create(post, user);
            return RedirectToAction(nameof(UserPosts));
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            Post post = postService.GetById(id);
            if (user is null)
            {
                return RedirectToAction(nameof(UserPosts));
            }
            if (post.UserId != user.Id)
            {
                return RedirectToAction(nameof(UserPosts));
            }
            return View(post);
        }
        public async Task<IActionResult> ConfirmDeleteAsync(int id)
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            Post post = postService.GetById(id);
            if (user is null)
            {
                return RedirectToAction(nameof(UserPosts));
            }
            if (post.UserId != user.Id)
            {
                return RedirectToAction(nameof(UserPosts));
            }
            postService.Delete(id);
            return RedirectToAction(nameof(UserPosts));
        }
        [HttpGet]
        public IActionResult ViewPost(int id)
        {
            return View(postService.GetById(id));
        }
    }
}
