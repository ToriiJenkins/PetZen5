using Microsoft.AspNet.Identity;
using PetZen.Data;
using PetZen.Models;
using PetZen.Models.PetModels;
using PetZen.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetZen.WebMVC.Controllers
{
    public class PetController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Pet
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PetService(userId);
            var model = service.GetPets();
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
        public ActionResult Create(PetCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreatePetService();

            if (service.CreatePet(model))
            {
                TempData["SaveResult"] = "Your pet has been added.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Pet could not be added.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreatePetService();
            var model = svc.GetPetById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreatePetService();
            var detail = service.GetPetById(id);
            var model =
                new PetEdit
                {
                    PetId = detail.PetId,
                    Name = detail.Name,
                    Species = detail.Species,
                    Breed = detail.Breed,
                    Weight = detail.Weight,
                    DateOfBirth = detail.DateOfBirth,
                    MealsPerDay = detail.MealsPerDay

                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PetEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.PetId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreatePetService();

            if (service.UpdatePet(model))
            {
                TempData["SaveResult"] = "Your pet was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your pet could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreatePetService();
            var model = svc.GetPetById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePet(int id)
        {
            var service = CreatePetService();

            service.DeletePet(id);

            TempData["SaveResult"] = "Your pet has been removed.";

            return RedirectToAction("Index");
        }
        private PetService CreatePetService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PetService(userId);
            return service;
        }
    }
}