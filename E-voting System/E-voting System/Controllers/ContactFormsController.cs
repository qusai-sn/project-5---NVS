using E_voting_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace E_voting_System.Controllers
{
    public class ContactFormsController : Controller
    {
        private ElectionEntities db = new ElectionEntities();
        // ---------------- Contact Form (User Side) ----------------------

        // get the contact form 
        public ActionResult ContactForm()
        {
            return View();
        }

        // Take the submission from user and store it in the database

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactForm([Bind(Include = "ID,Email,Name,Subject,Message")] ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                db.ContactForms.Add(contactForm);
                db.SaveChanges();
                return RedirectToAction("ContactForm", "ContactForms", new { success = true });
            }

            return View(contactForm);
        }


        // ---------------- Contact Form (Admin Side) ----------------------
        //The Contact Submissions on Admin Dashboard
        public ActionResult ContactSubmissions()
        {
            return View(db.ContactForms.ToList());
        }
        public ActionResult Reply(int id)
        {
            var contactForm = db.ContactForms.Find(id);
            if (contactForm == null)
            {
                return HttpNotFound();
            }

            ViewBag.ID = contactForm.ID;
            ViewBag.Email = contactForm.Email;

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendReply(int ID, string subject, string message)
        {
            try
            {
                var contactForm = db.ContactForms.Find(ID);
                if (contactForm == null)
                {
                    TempData["AlertMessage"] = "Contact form not found.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("ContactSubmissions");
                }

                // Email settings
                string fromEmail = "techlearnhub.contact@gmail.com";
                string toEmail = contactForm.Email;
                string subjectText = subject;
                string messageText = message;

                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;
                string smtpUsername = "techlearnhub.contact@gmail.com";
                string smtpPassword = "lyrlogeztsxclank";

                // Send the email
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(fromEmail);
                    mailMessage.To.Add(toEmail);
                    mailMessage.Subject = subjectText;
                    mailMessage.Body = messageText;
                    mailMessage.IsBodyHtml = false;

                    using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                    {
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                        smtpClient.EnableSsl = true;

                        smtpClient.Send(mailMessage);
                    }
                }
                contactForm.Status = messageText;
                db.SaveChanges();
                TempData["AlertMessage"] = $"تم إرسال الرد إلى  {toEmail} ";
                TempData["AlertType"] = "success";

                return RedirectToAction("ContactSubmissions");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred while processing your request. Please try again later.";
                TempData["AlertType"] = "error";
                Console.WriteLine("Exception message: " + ex.Message);
            }

            return RedirectToAction("ContactSubmissions");
        }



    }

}