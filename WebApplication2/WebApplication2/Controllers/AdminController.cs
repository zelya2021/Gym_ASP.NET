using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public List<Trainer> GetTrainers()
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                List<Trainer> trainers = new List<Trainer>();
                var allTrainers = ctx.Trainers;
                foreach (var item in allTrainers)
                {
                    trainers.Add(new Trainer
                    {
                        Id = item.Id,
                        Fio = item.Fio,
                        DateOfBirth = item.DateOfBirth,
                        PhoneNumber = item.PhoneNumber,
                        Status = item.Status,
                        Login = item.Login
                    });
                }
                return trainers;
            }
        }
        [Authorize(Roles = "admin")]
        public ActionResult AdminPanel()
        {
            return View(GetTrainers());
        }
        public ActionResult Detail()
        {
            return View(GetTrainers());
        }
        public ActionResult Add()
        {
            return View(GetTrainers());
        }
        public ActionResult Delete()
        {
            return View(GetTrainers());
        }
        public ActionResult Edit()
        {
            return View(GetTrainers());
        }
    }
}