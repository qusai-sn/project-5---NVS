using E_voting_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace E_voting_System.Controllers
{
    public class AdminPostsController : Controller
    {
        private ElectionEntities db = new ElectionEntities();

        // GET: AdminPosts
        public ActionResult Index()
        {
            var posts = db.ElectionPosts.ToList();
            return View(posts);
        }

        // GET: AdminPosts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminPosts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Content,CreatedAt,Status")] ElectionPost electionPost)
        {
            if (ModelState.IsValid)
            {
                electionPost.CreatedAt = DateTime.Now;
                electionPost.Status = "pending";
                db.ElectionPosts.Add(electionPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(electionPost);
        }

        // POST: AdminPosts/Approve
        [HttpPost]
        public ActionResult Approve(int id)
        {
            var post = db.ElectionPosts.Find(id);
            if (post != null)
            {
                post.Status = "approved";
                db.SaveChanges();
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // GET: AdminPosts/Edit/5
        public ActionResult Edit(int id)
        {
            var post = db.ElectionPosts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EditPartial", post);
        }

        // POST: AdminPosts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Content,CreatedAt,Status")] ElectionPost electionPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(electionPost).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { message = "Invalid model state." }, JsonRequestBehavior.AllowGet);
        }

        // GET: AdminPosts/Details/5
        public ActionResult Details(int id)
        {
            var post = db.ElectionPosts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: AdminPosts/Delete/5
        public ActionResult Delete(int id)
        {
            var post = db.ElectionPosts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: AdminPosts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var post = db.ElectionPosts.Find(id);
            db.ElectionPosts.Remove(post);
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}