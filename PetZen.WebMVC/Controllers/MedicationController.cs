using PetZen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetZen.WebMVC.Controllers
{
    public class MedicationController : Controller
    {
        // GET: Medication
        public ActionResult Index()
        {
            var model = new MedicationListItem[0];
            return View(model);
        }
    }
}