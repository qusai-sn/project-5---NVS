using E_voting_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_voting_System.Controllers
{
    public class AdsController : Controller
    {
        private ElectionEntities db = new ElectionEntities();

        // GET: Ads
        public ActionResult Index()
        {
            var approvedAds = db.ElectionAdvertisements
                .Where(a => a.Status == "approved")
                .OrderByDescending(a => a.CreatedAt)
                .ToList();
            var approvedPosts = db.ElectionPosts
                .Where(p => p.Status == "approved")
                .OrderByDescending(p => p.CreatedAt)
                .ToList();

            // Initialize LikeCount to 120 if null
            approvedAds.ForEach(ad => ad.LikeCount = ad.LikeCount ?? 120);
            approvedPosts.ForEach(post => post.LikeCount = post.LikeCount ?? 120);

            ViewBag.Ads = approvedAds;
            ViewBag.Posts = approvedPosts;

            return View();
        }

        // Other controller actions...

        [HttpPost]
        public ActionResult UpdateLikes(int id, string type, bool increment)
        {
            if (type == "ad")
            {
                var ad = db.ElectionAdvertisements.Find(id);
                if (ad != null)
                {
                    ad.LikeCount = (ad.LikeCount ?? 120) + (increment ? 1 : -1);
                    db.SaveChanges();
                    return Json(new { success = true, likeCount = ad.LikeCount });
                }
            }
            else if (type == "post")
            {
                var post = db.ElectionPosts.Find(id);
                if (post != null)
                {
                    post.LikeCount = (post.LikeCount ?? 120) + (increment ? 1 : -1);
                    db.SaveChanges();
                    return Json(new { success = true, likeCount = post.LikeCount });
                }
            }

            return Json(new { success = false });
        }
        public ActionResult Plans()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Ads(string plan, decimal amount)
        {
            ViewBag.Plan = plan;
            ViewBag.Amount = amount; // Set the amount based on the plan
            return View();
        }

        [HttpPost]
        public ActionResult Ads(ElectionAdvertisement model, string plan, decimal amount)
        {
            if (ModelState.IsValid)
            {
                // Save the ad with status "pending_payment" or "unpaid"
                model.Status = "pending_payment";
                db.ElectionAdvertisements.Add(model);
                db.SaveChanges();

                // Store ID and amount in session variables
                Session["AdId"] = model.ID;
                Session["Amount"] = amount;

                // Redirect to payment with plan only
                return RedirectToAction("Payment", new { plan = plan });
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Payment(string plan)
        {
            if (Session["AdId"] == null || Session["Amount"] == null)
            {
                return RedirectToAction("Plans");
            }

            var id = (int)Session["AdId"];
            var amount = (decimal)Session["Amount"];
            var ad = db.ElectionAdvertisements.Find(id);
            if (ad == null) return HttpNotFound();

            ViewBag.AdId = id;
            ViewBag.Plan = plan;
            ViewBag.Amount = amount; // Retrieve the amount from session

            return View();
        }

        [HttpPost]
        public ActionResult ProcessPayment(int adId, string orderID, decimal amount, string currency, string plan)
        {
            var ad = db.ElectionAdvertisements.Find(adId);
            if (ad == null) return Json(new { success = false });

            // Process payment
            var payment = new PayPalPayment
            {
                Amount = amount,
                PaymentDate = DateTime.Now,
                PaymentMethod = "PayPal",
                TransactionID = orderID,
                Status = "Completed"
            };

            db.PayPalPayments.Add(payment);
            db.SaveChanges();

            // Update ad status to "pending" after successful payment
            ad.PaymentID = payment.PaymentID;
            ad.Status = "pending"; // Status that allows the ad to be reviewed by admin
            db.SaveChanges();

            return Json(new { success = true });
        }

        [HttpGet]
        public ActionResult AdminAds()
        {
            // Fetch only ads with status "pending" or "approved"
            var ads = db.ElectionAdvertisements.Where(a => a.Status == "pending" || a.Status == "approved").ToList();
            return View(ads);
        }

        [HttpPost]
        public ActionResult AdminAds(string action, int[] selectedAds, DateTime[] endDates, int[] ids, int? id = null)
        {
            if (id.HasValue)
            {
                var ad = db.ElectionAdvertisements.Find(id.Value);
                if (ad != null)
                {
                    if (action == "approve")
                    {
                        ad.Status = "approved";
                        ad.CreatedAt = DateTime.Now;
                    }
                    else if (action == "reject")
                    {
                        ad.Status = "rejected";
                    }
                    else if (action == "saveSingle")
                    {
                        DateTime? endDate = endDates[Array.IndexOf(ids, id.Value)] != DateTime.MinValue ? (DateTime?)endDates[Array.IndexOf(ids, id.Value)] : null;
                        ad.EndDate = endDate;
                    }
                    db.SaveChanges();
                }
            }

            if (action == "delete")
            {
                if (selectedAds != null)
                {
                    foreach (var selectedId in selectedAds)
                    {
                        var ad = db.ElectionAdvertisements.Find(selectedId);
                        if (ad != null)
                        {
                            db.ElectionAdvertisements.Remove(ad);
                        }
                    }
                    db.SaveChanges();
                    ViewBag.Message = "Selected advertisements deleted.";
                }
                else
                {
                    ViewBag.Message = "No advertisements selected for deletion.";
                }
            }

            // Fetch updated list of ads with status "pending" or "approved"
            var updatedAds = db.ElectionAdvertisements.Where(a => a.Status == "pending" || a.Status == "approved").ToList();
            return View(updatedAds);
        }

    }
}