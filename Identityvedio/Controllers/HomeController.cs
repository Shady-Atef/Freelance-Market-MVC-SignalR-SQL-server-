using Identityvedio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.Entity;
namespace Identityvedio.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? page)
        {
            if (User.IsInRole("Client"))
            {
                return RedirectToAction("ClientJobs", "Client");

            }
            else if (User.IsInRole("Freelancer"))
            {
                return RedirectToAction("Index", "Job");

            }
            else
            {
                var jobs = db.Jobs.Include(j => j.JobCategory).Include(j => j.JobExperienceLevel);
                jobs = jobs.OrderByDescending(j => j.ID);
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                ViewBag.fromsearch = false;
                return View(jobs.ToPagedList(pageNumber, pageSize));
            }

        }

        public ActionResult Search(string keyword, int? page)
        {
            var jobs = db.Jobs.Where(j => j.Desc.Contains(keyword) ||
            j.JobTitle.Contains(keyword) ||
            j.JobSkills.Select(s => s.Skills.SkillsName.Contains(keyword)).FirstOrDefault());
            // var jobs = db.Job.Include(j => j.JobCategory).Include(j => j.JobExperienceLevel);
            jobs = jobs.OrderByDescending(j => j.ID);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            ViewBag.fromsearch = true;
            return View("Index", jobs.ToPagedList(pageNumber, pageSize));
            // return View("Index",jobs);
        }

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

        public ActionResult HowItWork()
        {
            return View();
        }
    }
}