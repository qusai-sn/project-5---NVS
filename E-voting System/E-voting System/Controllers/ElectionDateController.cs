using E_voting_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace E_voting_System.Controllers
{
    public class ElectionDateController : Controller
    {
        private ElectionEntities db = new ElectionEntities();

        // GET: DateSet
        public ActionResult DateSet()
        {
            var schedules = db.ElectionSchedules.ToList();
            return View(schedules);
        }

        // GET: DateSet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ElectionSchedule schedule = db.ElectionSchedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }

            return View(schedule);
        }

        // POST: DateSet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ElectionSchedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedule).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DateSet");
            }

            return View(schedule);
        }
    }
}