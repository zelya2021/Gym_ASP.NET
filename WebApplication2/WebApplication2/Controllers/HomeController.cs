using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public List<IndividualTraining> GetIndividualTraining()
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                List<IndividualTraining> individualTrainings = new List<IndividualTraining>();
                List<IndividualTraining> allTrainings = ctx.IndividualTrainings.Where(t => t.Trainer.Fio == User.Identity.Name).ToList();
                foreach (var item in allTrainings)
                {
                    individualTrainings.Add(new IndividualTraining
                    {
                        Id = item.Id,
                        Time = item.Time,
                        Day = item.Day,
                        Trainer = ctx.Trainers.FirstOrDefault(t => t.Fio == User.Identity.Name)
                    });

                }
                return individualTrainings;
            }
        }
        public ActionResult Index()
        {
            using (ApplicationDbContext ctx = ApplicationDbContext.Create())
            {
            //    var users = ctx.Users.ToList();
                ;
            }
            return View();
        }
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
        [Authorize(Roles = "user")]
        public ActionResult ClientPannel()
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var client = ctx.Clients.FirstOrDefault(t => t.Fio == User.Identity.Name);
                ViewData["client"] = client;
            }
            return View();
        }
        [Authorize(Roles = "user, moderator")]
        public ActionResult Chat()
        {
            return View();
        }
    }
}