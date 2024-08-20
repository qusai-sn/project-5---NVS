using E_voting_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace E_voting_System.Controllers
{
    public class userController : Controller
    {
        private ElectionEntities db = new ElectionEntities();

        // GET: user
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(USER user)
        {
            try
            {
                var userRecord = db.USERS.Find(user.National_ID);

                if (userRecord == null)
                {
                    ModelState.AddModelError("", "User not found.");
                    return View();
                }

                if (userRecord.Password == null)
                {
                    // Generate a random confirmation code
                    Random random = new Random();
                    int randomCode = random.Next(100000, 1000000);
                    Response.Cookies["ConfCode"].Value = randomCode.ToString();
                    Response.Cookies["ConfCode"].Expires = DateTime.Now.AddMinutes(3);

                    // Email settings
                    string fromEmail = "techlearnhub.contact@gmail.com";
                    string toEmail = "qusayomar20@gmail.com"; // Assuming the user record contains the user's email
                    string subjectText = "Your Confirmation Code";
                    string messageText = $"Your confirmation code is {randomCode}";

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
                            Response.Cookies["National_ID"].Value = user.National_ID.ToString();
                             Response.Cookies["National_ID"].Expires = DateTime.Now.AddMinutes(30);

                            ViewBag.Emailsent = "The code has been sent to your Email";
                             

                            return RedirectToAction("EnterCode");
                        }
                    }
                }
                else
                {
                    // User has a password, redirect to prompt for password
                    ViewBag.NationalId = user.National_ID;

                    return RedirectToAction("LoginWithPassword");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                Console.WriteLine("Exception message: " + ex.Message);
                Console.WriteLine("Stack trace: " + ex.StackTrace);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception message: " + ex.InnerException.Message);
                    Console.WriteLine("Inner exception stack trace: " + ex.InnerException.StackTrace);
                }
            }

            return View();
        }

        public ActionResult EnterCode()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EnterCode(string code)
        {
            var confCodeCookie = Request.Cookies["ConfCode"];

            if (confCodeCookie != null)
            {
                var confCode = confCodeCookie.Value;

                if (code == confCode)
                {
                    return RedirectToAction("ResetPassword");
                }
            }

            ModelState.AddModelError("", "Invalid code.");
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(string NewPassword, string ConfirmPassword)
        {
            // Retrieve National ID from cookies or session
            string National_ID = null;

            if (Request.Cookies["National_ID"] != null)
            {
                National_ID = Request.Cookies["National_ID"].Value;
            }
            else if (Session["National_ID"] != null)
            {
                National_ID = Session["National_ID"].ToString();
            }

            if (National_ID == null)
            {
                ModelState.AddModelError("", "National ID not found.");
                return View();
            }

            if (NewPassword != ConfirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match.");
                return View();
            }

            var userRecord = db.USERS.Find(int.Parse(National_ID));

            if (userRecord == null)
            {
                ModelState.AddModelError("", "User not found.");
                return View();
            }

            userRecord.Password = NewPassword;
            db.SaveChanges();

            return RedirectToAction("Login");
        }





        public ActionResult LoginWithPassword()
        {
             
            return View();
        }

        [HttpPost]
        public ActionResult LoginWithPassword(string National_ID, string Password)
        {
            var db_record = db.USERS.Find(int.Parse(National_ID));

            if (db_record != null && Password == db_record.Password)
            {
                ViewBag.auth = "";
                Session["National_ID"] = db_record.National_ID; // Ensure this is set as an integer
                return RedirectToAction("TypeOfElection", new { national_id = db_record.National_ID });
            }
            else
            {
                ViewBag.auth = "Password is not correct";
            }

            return View();
        }


        public ActionResult TypeOfElection(int national_id)
        {
            var user_record = db.USERS.Find(national_id);
            var circle_id = user_record.Circle_ID;
            ViewBag.circle = circle_id;

            if (user_record.local_Vote == 0)
            {
                ViewBag.localList_actionName = "LocalList";
            }
            else
            {
                ViewBag.localList_actionName = null;
            }

            if (user_record.Party_Vote == 0)
            {
                ViewBag.PartyList_actionName = "PartyList";
            }
            else
            {
                ViewBag.PartyList_actionName = null;
            }

            return View();
        }

       
        

        public ActionResult PartyList()
        {
            var national_id = Session["National_ID"];
            if (national_id == null)
            {
                return RedirectToAction("Login");
            }

            var partyLists = db.PartyLists.ToList();

            var model = new PartyListViewModel
            {
                PartyLists = partyLists
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult PartyList(PartyListViewModel model)
        {
            if (Session["National_ID"] == null)
            {
                return RedirectToAction("Login");
            }

            int national_id;
            if (!int.TryParse(Session["National_ID"].ToString(), out national_id))
            {
                // Handle the error if the session value cannot be cast to an integer
                ModelState.AddModelError("", "Invalid National ID.");
                return View(model);
            }

            var user = db.USERS.Find(national_id);
            if (user != null)
            {
                if (model.SelectedPartyId.HasValue)
                {
                    var selectedParty = db.PartyLists.Find(model.SelectedPartyId.Value);
                    if (selectedParty != null)
                    {
                        user.Party_Vote = 1; // Update the user's party vote status
                        selectedParty.Counter += 1; // Increment the counter for the selected party
                        db.SaveChanges();
                    }
                }
                else
                {
                    user.Party_Vote = 1; // Update the user's party vote status
                    user.White_Party_Vote = 1; // Update the user's white party vote status
                    db.SaveChanges();
                }
            }

            return RedirectToAction("TypeOfElection", new { national_id = national_id });
        }


        public ActionResult LocalList(int circleId)
        {
            var national_id = Session["National_ID"];
            if (national_id == null)
            {
                return RedirectToAction("Login");
            }

            var user = db.USERS.Find(national_id);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            // Fetch only the approved local lists for the given circle ID
            var localLists = db.localLists
                                .Where(l => l.Circle_ID == circleId && l.Status == "Approved")
                                .ToList();

            var candidates = db.localCandidates.ToList(); // Get all candidates

            var model = new LocalListViewModel
            {
                LocalLists = localLists,
                CandidatesByList = localLists.ToDictionary(
                    list => list.ID,
                    list => candidates.Where(candidate => candidate.List_Name == list.list_Name).ToList()
                )
            };

            return View(model);
        }



        [HttpPost]
        public ActionResult LocalList(LocalListViewModel model)
        {
            var national_id = Session["National_ID"];
            if (national_id == null)
            {
                return RedirectToAction("Login");
            }

            var user = db.USERS.Find(national_id);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            if (model.SelectedListId.HasValue)
            {
                var selectedList = db.localLists.Find(model.SelectedListId.Value);
                if (selectedList != null)
                {
                    selectedList.Counter += 1;
                    db.SaveChanges();
                }

                if (model.SelectedCandidateIds != null)
                {
                    foreach (var candidateId in model.SelectedCandidateIds)
                    {
                        var candidate = db.localCandidates.Find(candidateId);
                        if (candidate != null)
                        {
                            candidate.Counter += 1;
                        }
                    }
                    db.SaveChanges();
                }

                user.local_Vote = 1;
                db.SaveChanges();
            }
            else
            {
                user.local_Vote = 1; // Also update local_Vote
                user.White_Local_Vote = 1;
                db.SaveChanges();
            }

            return RedirectToAction("TypeOfElection", new { national_id = user.National_ID });
        }




        public ActionResult Circles()
        {
            var national_id = Session["National_ID"];
            if (national_id == null)
            {
                return RedirectToAction("Login");
            }

            var user = db.USERS.Find(national_id);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var model = new CirclesViewModel
            {
                ActiveCircleId = user.Circle_ID,
                Circles = Enumerable.Range(1, 4).ToList() // List of circle IDs from 1 to 18
            };

            return View(model);
        }








    }
}
