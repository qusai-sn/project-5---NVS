using E_voting_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_voting_System.Controllers
{
    public class DateController : Controller
    {
        private ElectionEntities db = new ElectionEntities();
        public ActionResult ListOfDates()
        {
            var dates = db.ElectionSchedules.ToList();
            return View(dates);
        }

        public ActionResult EditDates(int id = 1)
        {
            ElectionSchedule electionSchedule = db.ElectionSchedules.Find(id);
            return View(electionSchedule);
        }
        [HttpPost]
        public ActionResult EditDates([Bind(Include = "Start_Date_Of_Election,End_Date_Of_Election")] ElectionSchedule x)
        {
            // Fetch the existing election schedule record
            ElectionSchedule electionSchedule = db.ElectionSchedules.Find(1);

            if (electionSchedule == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                // Update the fetched record with new values
                electionSchedule.Start_Date_Of_Election = x.Start_Date_Of_Election;
                electionSchedule.End_Date_Of_Election = x.End_Date_Of_Election;

                // Mark the entity as modified
                db.Entry(electionSchedule).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ListOfDates");
            }

            // Return the view with the original entity if the model state is not valid
            return View(electionSchedule);
        }

    }
}