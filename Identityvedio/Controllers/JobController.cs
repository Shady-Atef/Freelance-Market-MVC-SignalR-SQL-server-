using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Identityvedio.Models;
using Microsoft.AspNet.Identity;
using PagedList;

namespace Identityvedio.Controllers
{
    [Authorize]
    public class JobController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Job
        public ActionResult Index(int? page)
        {
            var jobs = db.Jobs.Include(j => j.JobCategory).Include(j => j.JobExperienceLevel).Where(j=>j.Ended==false);
            jobs = jobs.OrderByDescending(j => j.ID);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            ViewBag.fromsearch = false;
            return View(jobs.ToPagedList(pageNumber, pageSize));
        }

        // GET: Job/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

       
        public ActionResult Create()
        {
            //ViewBag.Skills = db.Skills;
            List<SkillModel> skillModels = new List<SkillModel>();
            var skills = db.Skills.ToList();
            JobVM JobObj = new JobVM();
            foreach (var item in skills)
            {
                //SkillModel skillModel = new SkillModel() { Text = item.SkillsName, IsChecked = true,SkillId=item.ID,Value=1 };
                SkillModel skillModel = new SkillModel() { Text = item.SkillsName, IsChecked = false, SkillId = item.ID };
                skillModels.Add(skillModel);
            }
            JobObj.SkillList = skillModels;

            ViewBag.CatID = new SelectList(db.JobCategory, "ID", "Name");
            ViewBag.ExperienceLevelId = new SelectList(db.JobExperienceLevel, "ID", "ExperienceLevel");
            return View(JobObj);
        }

        // POST: Job/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "SkillList")] JobVM jobVM, List<int> SkillList)
        {
            if (ModelState.IsValid)
            {
                //SkillList_@i
                Job job = new Job()
                {
                    JobTitle = jobVM.JobTitle,
                    CatID = jobVM.CatID,
                    ClientId = User.Identity.GetUserId(),
                    Desc = jobVM.Desc,
                    Price = jobVM.Price,
                    ExperienceLevelId = jobVM.ExperienceLevelId

                };

                db.Jobs.Add(job);
                db.SaveChanges();
                foreach (var item in SkillList)
                {
                    JobSkills JobSkill = new JobSkills();
                    JobSkill.JobId = job.ID;
                    JobSkill.SkillId = item;
                    db.JobSkills.Add(JobSkill);
                    db.SaveChanges();

                }
                return RedirectToAction("ClientJobs","Client");
            }
            var skills = db.Skills.ToList();
            List<SkillModel> skillModels = new List<SkillModel>();
            foreach (var item in skills)
            {
                //SkillModel skillModel = new SkillModel() { Text = item.SkillsName, IsChecked = true,SkillId=item.ID,Value=1 };
                SkillModel skillModel = new SkillModel() { Text = item.SkillsName, IsChecked = false, SkillId = item.ID };
                skillModels.Add(skillModel);
            }


            jobVM.SkillList = skillModels;
            ViewBag.CatID = new SelectList(db.JobCategory, "ID", "Name", jobVM.CatID);
            ViewBag.ExperienceLevelId = new SelectList(db.JobExperienceLevel, "ID", "ExperienceLevel", jobVM.ExperienceLevelId);
            return View(jobVM);
        }

        // GET: Job/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatID = new SelectList(db.JobCategory, "ID", "Name", job.CatID);
            ViewBag.ExperienceLevelId = new SelectList(db.JobExperienceLevel, "ID", "ExperienceLevel", job.ExperienceLevelId);
            return View(job);
        }
        [HttpGet][Authorize]
        public ActionResult Search(string keyword,int? page)
        {
            var jobs = db.Jobs.Where(j => j.Desc.Contains(keyword) ||
            j.JobTitle.Contains(keyword) ||
            j.JobSkills.Select(s => s.Skills.SkillsName.Contains(keyword)).FirstOrDefault());
           // var jobs = db.Job.Include(j => j.JobCategory).Include(j => j.JobExperienceLevel);
            jobs = jobs.OrderByDescending(j => j.ID);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            ViewBag.fromsearch = true;
            return View("Index",jobs.ToPagedList(pageNumber, pageSize));
           // return View("Index",jobs);
        }
        // POST: Job/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,JobTitle,CatID,Desc,Price,ExperienceLevelId")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CatID = new SelectList(db.JobCategory, "ID", "Name", job.CatID);
            ViewBag.ExperienceLevelId = new SelectList(db.JobExperienceLevel, "ID", "ExperienceLevel", job.ExperienceLevelId);
            return View(job);
        }

        // GET: Job/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Job/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
