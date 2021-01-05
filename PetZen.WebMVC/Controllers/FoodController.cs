using System;
using PetZen.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetZen.Models.FoodModels;
using Microsoft.AspNet.Identity;
using PetZen.Services;

namespace PetZen.WebMVC.Controllers
{
    public class FoodController : Controller
    {
        // GET: Food
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new FoodService(userId);
            var model = service.GetFoods();
            return View(model);
        }

        //GET: PET/CREATE
        public ActionResult Create()
        {
            return View();

        }

        //POST: Pet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateFoodService();

            if (service.CreateFood(model))
            {
                TempData["SaveResult"] = "Your food has been added.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "food could not be added.");

            return View(model);
        }

        private FoodService CreateFoodService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new FoodService(userId);
            return service;
        }
    }
}