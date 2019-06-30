using Identityvedio.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Identityvedio.Controllers
{
    public class ClientController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Client
        public ActionResult Index()
        {
            return View();
            
        }

        public ActionResult ViewProfile(string id)
        {
            id = User.Identity.GetUserId();

            
            Client client = db.Clients.Where(j => j.ClientId == id).FirstOrDefault();
            if (client == null)
            {
                return HttpNotFound();

            }
            var id1 = User.Identity.GetUserId();
            var jobs = (from d in db.Jobs
                        where d.ClientId == id1
                        select d).Count();
            ViewBag.totalJobs = jobs;
            return View(client);
        }

        public ActionResult CreateProfile()
        {
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            if (user == null)
            {
                return HttpNotFound();

            }
            //client.ClientId = User.Identity.GetUserName();
            ViewBag.user = user;
            return View();

        }


        [HttpPost]
        public ActionResult CreateProfile( ClientViewModel clientVM)
        {
            
            if (User.Identity.GetUserId() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (ApplicationDbContext entity = new ApplicationDbContext())
            {
                var client = new Client()
                {
                    //ID = propVM.ID,
                    Description = clientVM.Description,
                    ImagePath = SaveToPhysicalLocation(clientVM.ImagePath),
                    ClientId = User.Identity.GetUserId(),
                    FullName=clientVM.FullName,
                    Profesion=clientVM.Profesion,
                    SSN=clientVM.SSN,
                    
                };
                entity.Clients.Add(client);
                entity.SaveChanges();
                return RedirectToAction("ViewProfile", new RouteValueDictionary(new { Controller="Client", Action= "ViewProfile", id= client.ID}));
            }

        }

        private string SaveToPhysicalLocation(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return string.Empty;
            }
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                //var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                var path = Path.Combine(Server.MapPath("/Content/images"), fileName);
                //path = fileName;

                file.SaveAs(path);
                path = "/Content/images/" + fileName;
                return path;
            }
            return string.Empty;
        }

        public ActionResult ClientJobs()
        {
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());

            List<Job> ClientJobs = db.Jobs.Where(j => j.ClientId == user.Id).ToList();
            return View(ClientJobs);
        }

        public ActionResult EndJob(int id)
        {
            Job job = db.Jobs.Where(j => j.ID == id).FirstOrDefault();
            job.Ended = true;
            db.SaveChanges();
            return RedirectToAction("ClientJobs");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client client = db.Clients.Where(s => s.ID == id).FirstOrDefault();
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
        [HttpPost]
        public ActionResult Edit( Client client)
        {
            if (ModelState.IsValid)
            {
                client.ClientId=  User.Identity.GetUserId();
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ClientJobs");
            }
            return View(client);
        }

    }
}