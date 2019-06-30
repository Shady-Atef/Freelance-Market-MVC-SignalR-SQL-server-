using Identityvedio.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.AspNet.Identity;

namespace Identityvedio.Controllers
{
    public class ProposalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        public ActionResult Index(int id)
        {
            List<Proposal> Proposals = db.Proposals.Where(p => p.JobId == id).ToList();
            JobProposals jP = new JobProposals();
            jP.Proposals = Proposals;
            jP.JobID = id;

            return View(jP);
        }
        [Authorize(Roles = "Client")]
        public ActionResult Details(int id)
        {
            var proposal = db.Proposals.FirstOrDefault(p => p.ID == id);
            return View(proposal);
        }
        public ActionResult ViewAllProposalByJobId(int? id)
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

            List<Proposal> proplist = db.Proposals.Where(s => s.JobId == id).ToList();

            //return View("index", crslist);

            return View(proplist);
        }
        [Authorize(Roles = "Freelancer")]
        public ActionResult ViewPropsals()
        {
            var freelancerId = User.Identity.GetUserId();
            var proposals = db.Proposals.Where(p => p.FreelancerId == freelancerId).ToList();
            return View(proposals);
        }
        public ActionResult PropsalDetails(int id)
        {
            var proposal = db.Proposals.FirstOrDefault(p => p.ID == id);
            return View(proposal);
        }

        public ActionResult CreateProposal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public ActionResult CreateProposal(ProposalViewModel propVM, int id)
        {

            if (User.Identity.GetUserId() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (ApplicationDbContext entity = new ApplicationDbContext())
            {
                var Proposal = new Proposal()
                {
                    //ID = propVM.ID,
                    proposal = propVM.proposal,
                    proposaldate = DateTime.Now,
                    Duration = propVM.Duration,
                    JobId = id,
                    FilePath = SaveToPhysicalLocation(propVM.FilePath),
                    FreelancerId = User.Identity.GetUserId(),
                };
                entity.Proposals.Add(Proposal);
                entity.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            //return View();
        }
        public ActionResult CreateChat(int proposalId, string freelanceId, string messageText)
        {
            var propsal = db.Proposals.FirstOrDefault(p => p.ID == proposalId);
            propsal.status = 1;
            Message msg = new Message();
            msg.ManagerId = User.Identity.GetUserId();
            msg.FreelanceId = freelanceId;
            msg.MessageText = messageText;
            msg.MessageTime = DateTime.Now;
            msg.ProposalId = proposalId;
            msg.Sender=User.Identity.GetUserId();
            db.Messages.Add(msg);
            db.SaveChanges();

            return RedirectToAction("Index", "Message", new { id = proposalId });
        }
        public ActionResult ConfirmPropsal(int proposalId, string freelanceId)
        {
            var propsal = db.Proposals.FirstOrDefault(p => p.ID == proposalId);
            propsal.status = 2;
            Message msg = new Message();
            msg.ManagerId = User.Identity.GetUserId();
            msg.FreelanceId = freelanceId;
            msg.MessageText = "Confirm";
            msg.MessageTime = DateTime.Now;
            msg.ProposalId = proposalId;
            msg.Sender = freelanceId;
            db.Messages.Add(msg);
            db.SaveChanges();

            return RedirectToAction("Index", "Message", new { id = proposalId });
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
                var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                file.SaveAs(path);
                return path;
            }
            return string.Empty;
        }

    }
}