using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class TrainerController : Controller
    {
        // GET: Trainer
        static TrainerController()
        {
            //using (ApplicationDbContext ctx = new ApplicationDbContext())
            //{
            //    ctx.IndividualTrainings.Add(new IndividualTraining
            //    {
            //        Time = "14:30 - 15:30",
            //        Day = Convert.ToDateTime("14.03.2020")
            //    });
            //    ctx.SaveChanges();
            //}
        }
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
                            Trainer = ctx.Trainers.FirstOrDefault(t => t.Fio == User.Identity.Name),
                            Client=item.Client
                        });
                }
                return individualTrainings;
            }
        }
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "moderator")]
        public ActionResult TrainerPanel()
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var trainer = ctx.Trainers.FirstOrDefault(t => t.Fio == User.Identity.Name);
                ViewData["trainer"] = trainer;
            }
            return View(GetIndividualTraining());
        }
        public ActionResult EditIndivTr(int? id)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                if (id is null)
                {
                    return new HttpNotFoundResult();
                }
                var training = ctx.IndividualTrainings.FirstOrDefault(p => p.Id == id);
                if (training is null)
                {
                    return new HttpNotFoundResult();
                }
                // ViewBag.Trainer = trainer;
                ViewData["training"] = training;
                return View();
            }
        }
        [HttpPost]
        public ActionResult EditIndivTr(IndividualTraining training)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var editTraining = ctx.IndividualTrainings.FirstOrDefault(p => p.Id == training.Id);
                if (editTraining != null)
                {
                    editTraining.Day = training.Day;
                    editTraining.Time = training.Time;
                    ctx.SaveChanges();
                }
                return RedirectToAction("TrainerPanel");
            }
        }
        public ActionResult AddIndividTraining()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddIndividTraining(IndividualTraining training)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var client = ctx.Clients.FirstOrDefault(p => p.Fio == training.Client.Fio);
                var trainer = ctx.Trainers.FirstOrDefault(p => p.Fio == User.Identity.Name);
                training.Client = client;
                training.Trainer = trainer;
                ctx.IndividualTrainings.Add(training);
                ctx.SaveChanges();
                return RedirectToAction("TrainerPanel");
            }
        }
        public ActionResult Delete(int? id)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext()) 
            {
                if (id is null)
                {
                    return new HttpNotFoundResult();
                }
                var training = ctx.IndividualTrainings.FirstOrDefault(a => a.Id == id);
                if (training is null)
                {
                    return new HttpNotFoundResult();
                }
                ctx.IndividualTrainings.Remove(training);
                ctx.SaveChanges();
                return RedirectToAction("TrainerPanel");
            }
               
        }
        public ActionResult DetailIndividTraining(int? id)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                if (id is null)
                {
                    return new HttpNotFoundResult();
                }
                var trainings = ctx.IndividualTrainings.FirstOrDefault(a => a.Id == id);
                if (trainings is null)
                {
                    return new HttpNotFoundResult();
                }
                //ViewData["abonent"] = abonent;
                //ViewBag.Abonent = abonent;
                return View(trainings);
            }
        }
    }
}