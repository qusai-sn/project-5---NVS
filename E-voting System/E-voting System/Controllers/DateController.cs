using E_voting_System.Models;
using System;
using System.Collections.Generic;
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
        public ActionResult EditDates(int id)
        {
            ElectionSchedule electionSchedule = db.ElectionSchedules.Find(id);
            return View(electionSchedule);
        }
    }
}