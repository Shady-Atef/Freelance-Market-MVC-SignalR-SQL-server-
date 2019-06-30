using Identityvedio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Identityvedio.ViewModels;

namespace Identityvedio.Controllers
{
    public class MessageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Message
        public ActionResult Index(int id)
        {
            if (User.Identity.GetUserId() == db.Proposals.Where(p => p.ID == id).Select(p => p.FreelancerId).FirstOrDefault())
            {
                var jobId = db.Proposals.Where(p => p.ID == id).Select(p => p.JobId).FirstOrDefault();
                var clientId = db.Jobs.Where(j => j.ID == jobId).Select(j => j.ClientId).FirstOrDefault();
                ViewBag.SecondPerson = db.Users.Where(j => j.Id == clientId).Select(p => p.UserName).FirstOrDefault();

            }
            else if (User.Identity.GetUserId() == db.Proposals.Where(p => p.ID == id).Select(p => p.Job.ClientId).FirstOrDefault())
            {
                var freelanceId = db.Proposals.Where(p => p.ID == id).Select(p => p.FreelancerId).FirstOrDefault();

                ViewBag.SecondPerson = db.Users.Where(p => p.Id == freelanceId).Select(p => p.UserName).FirstOrDefault();

            }
            ViewData["ProposalId"] = id;
            if (User.Identity.GetUserId() == db.Proposals.Where(p => p.ID == id).Select(p => p.FreelancerId).FirstOrDefault() || User.Identity.GetUserId() == db.Proposals.Where(p => p.ID == id).Select(p => p.Job.ClientId).FirstOrDefault())
            {
                var messages = (from m in db.Messages
                                where m.ProposalId == id
                                select m).ToList();
                return View(messages);
            }


            return View();
        }
        [Authorize(Roles = "Freelancer")]
        public ActionResult Chats()
        {
            var freelancerId = User.Identity.GetUserId();
            var proposals = db.Proposals.Where(p => p.FreelancerId == freelancerId && p.status == 2).ToList();
            return View(proposals);
        }
        [Authorize(Roles = "Client")]
        public ActionResult ClientChats()
        {
            var clientId = User.Identity.GetUserId();
            var proposals = db.Proposals.Where(p => p.Job.ClientId == clientId && (p.status == 1||p.status==2)).ToList();
            return View(proposals);
        }
        // GET: Message/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Message/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Message/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Message/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Message/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Message/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Message/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
