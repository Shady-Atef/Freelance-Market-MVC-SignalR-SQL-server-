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


namespace Identityvedio.Controllers
{
    public class ContractController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contract
        public ActionResult Index()
        {
            var contracts = db.Contracts.Include(c => c.ApplicationClient).Include(c => c.ApplicationFreelancer).Include(c => c.Job).Include(c => c.Proposal);
            return View(contracts.ToList());
        }

        // GET: Contract/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }
        [Authorize(Roles = "Client")]
        // GET: Contract/Create
        public ActionResult Create(int id)
        {

            Contract cont = new Contract();
            cont.ClientId = User.Identity.GetUserId();
            cont.FreelanceId = db.Proposals.Where(p => p.JobId == id).Select(p => p.FreelancerId).FirstOrDefault();
            cont.JobId = id;
            cont.FinalPrice = db.Jobs.Where(j => j.ID == id).Select(j => j.Price).FirstOrDefault();
            cont.ProposalId = db.Proposals.Where(p => p.FreelancerId == cont.FreelanceId && p.JobId == cont.JobId).Select(p=>p.ID).FirstOrDefault();
            //ViewBag.ProposalId = new SelectList(db.Proposals, "ID", "FreelanceId");
            return View(cont);
        }

        // POST: Contract/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contract contract)
        {
            if (ModelState.IsValid)
            {
                db.Contracts.Add(contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.ClientId = new SelectList(user, "Id", "Email", contract.ClientId);
            //ViewBag.FreelanceId = new SelectList(db.ApplicationUsers, "Id", "Email", contract.FreelanceId);


            ViewBag.JobId = new SelectList(db.Jobs, "ID", "JobTitle", contract.JobId);
            ViewBag.ProposalId = new SelectList(db.Proposals, "ID", "FreelanceId", contract.ProposalId);
            return View(contract);

        }

        // GET: Contract/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobId = new SelectList(db.Jobs, "ID", "JobTitle", contract.JobId);
            ViewBag.ProposalId = new SelectList(db.Proposals, "ID", "FreelanceId", contract.ProposalId);
            return View(contract);
        }

        // POST: Contract/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProposalId,JobId,FreelanceId,ClientId,StartTime,EndTime")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JobId = new SelectList(db.Jobs, "ID", "JobTitle", contract.JobId);
            ViewBag.ProposalId = new SelectList(db.Proposals, "ID", "FreelanceId", contract.ProposalId);
            return View(contract);
        }

        // GET: Contract/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // POST: Contract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contract contract = db.Contracts.Find(id);
            db.Contracts.Remove(contract);
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
