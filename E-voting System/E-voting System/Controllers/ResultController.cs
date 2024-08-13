using E_voting_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_voting_System.Controllers
{
    public class ResultController : Controller
    {
        ElectionEntities db = new ElectionEntities();
        public ActionResult Index()
        {
            return View();
        }

        // GET : LocalResults
        public ActionResult LocalResult()
        {
            List<CircleViewModel> objects_list = new List<CircleViewModel>();

            var availableSeats = new Dictionary<int, int>
                {
                    { 1, 7 },
                    { 2, 5 },
                    { 3, 3 }
                };

            for (int counter = 1; counter <= 3; counter++)
            {
                CircleViewModel circleViewModel = new CircleViewModel();

                var records = db.USERS
                                .Where(u => u.Circle_ID == counter && (u.local_Vote == 1 || u.White_Local_Vote == 1))
                                .ToList();

                circleViewModel.users_circle_local = records;
                circleViewModel.localvotecounter = records.Count(u => u.local_Vote == 1);
                circleViewModel.whitelocalcounter = records.Count(u => u.White_Local_Vote == 1);
                circleViewModel.totalVotes = records.Count();
                circleViewModel.threshold = circleViewModel.totalVotes * 0.07;

                var local_lists = db.localLists.Where(l => l.Circle_ID == counter).ToList();
                var winning_lists = local_lists.Where(l => l.Counter >= circleViewModel.threshold).ToList();

                if (!winning_lists.Any() && local_lists.Any())
                {
                    var maxCounterList = local_lists.OrderByDescending(l => l.Counter).FirstOrDefault();
                    if (maxCounterList != null)
                    {
                        winning_lists.Add(maxCounterList);
                    }

                }

                double sumOfCounters = winning_lists.Sum(l => l.Counter);

                if (sumOfCounters == 0)
                {
                    sumOfCounters = 1;
                }

                var winningCandidates = new Dictionary<int, List<localCandidate>>();

                foreach (var list in winning_lists)
                {
                    // Calculate the actual seats and ensure it's not null
                    int calculatedSeats = (int)Math.Max(Math.Round((list.Counter / sumOfCounters) * availableSeats[counter]), 1);
                    list.ActualSeats = calculatedSeats;

                    var candidates = db.localCandidates
                                       .Where(c => c.List_Name == list.list_Name)
                                       .OrderByDescending(c => c.Counter)
                                       .Take(calculatedSeats)
                                       .ToList();

                    winningCandidates[list.ID] = candidates;
                }


                circleViewModel.winning_lists = winning_lists.Where(l => l.ActualSeats > 0).ToList();
                circleViewModel.winningCandidates = winningCandidates;

                objects_list.Add(circleViewModel);
            }

            return View(objects_list);
        }


        public void CalculateThresholdOfParty()
        {
            // Calculate the necessary values
            var totalParticipants = db.USERS.Count(u => u.Party_Vote > 0 || u.White_Party_Vote > 0);
            var totalWhitePartyVotes = db.USERS.Sum(u => u.White_Party_Vote);
            var threshold = totalParticipants * 0.025;

            // Set cookies (this assumes you have access to the HttpContext)
            var response = HttpContext?.Response;
            if (response != null)
            {
                response.Cookies["TotalParticipants"].Value = totalParticipants.ToString();
                response.Cookies["TotalWhitePartyVotes"].Value = totalWhitePartyVotes.ToString();
                response.Cookies["Threshold"].Value = threshold.ToString();
            }
        }



        private Dictionary<string, List<string>> GetPartyCandidates(Dictionary<int, int> seatAllocations)
        {
            var partyCandidates = new Dictionary<string, List<string>>();

            foreach (var allocation in seatAllocations)
            {
                int partyId = allocation.Key;
                int numberOfSeats = allocation.Value;

                var partyName = db.PartyLists
                    .Where(p => p.ID == partyId)
                    .Select(p => p.Party_Name)
                    .FirstOrDefault();

                if (partyName != null)
                {
                    var candidates = db.PartyCandidates
                        .Where(c => c.PartyLIST_ID == partyId)
                        .OrderBy(c => c.ID)
                        .Take(numberOfSeats)
                        .Select(c => c.Name)
                        .ToList();

                    partyCandidates[partyName] = candidates;
                }
            }

            return partyCandidates;
        }

        public ActionResult CalculateVotesForEachParty()
        {
            // Ensure threshold calculation is up-to-date
            CalculateThresholdOfParty();

            // Retrieve the threshold from cookies
            var thresholdCookie = HttpContext.Request.Cookies["Threshold"];
            if (thresholdCookie == null)
            {
                // Handle the case where the threshold cookie is not set
                return new HttpStatusCodeResult(500, "Threshold cookie not found.");
            }

            var threshold = double.Parse(thresholdCookie.Value);

            var partyVotes = db.PartyLists.ToList();
            var results = new List<string>();

            foreach (var party in partyVotes)
            {
                var Party_Name = party.Party_Name;
                var Party_Counter = party.Counter;
                bool passedThreshold = Party_Counter >= threshold;
                string resultString = $"{Party_Name}:{Party_Counter}:{passedThreshold}";
                results.Add(resultString);
            }

            string allStrings = string.Join(",", results);
            HttpContext.Response.Cookies["PartyResults"].Value = allStrings;

            var passedParties = partyVotes.Where(x => x.Counter >= threshold).ToList();
            var totalVotesAboveThreshold = passedParties.Sum(x => x.Counter);
            HttpContext.Response.Cookies["totalVotesAboveThreshold"].Value = totalVotesAboveThreshold.ToString();

            var TotalSeats = 41;
            int totalAssignedSeats = 0;
            var seatAllocations = new Dictionary<int, int>();
            foreach (var party in passedParties)
            {
                double seatsForEachParty = (party.Counter / (double)totalVotesAboveThreshold) * TotalSeats;
                int roundedSeatCount = (int)Math.Round(seatsForEachParty);
                seatAllocations[party.ID] = roundedSeatCount;
                totalAssignedSeats += roundedSeatCount;
                HttpContext.Response.Cookies[$"Seats_{party.Party_Name}"].Value = roundedSeatCount.ToString();
            }

            int diff = TotalSeats - totalAssignedSeats;
            for (int i = 0; diff > 0 && i < seatAllocations.Count; i++)
            {
                var key = seatAllocations.Keys.ElementAt(i);
                seatAllocations[key]++;
                totalAssignedSeats++;
                diff--;
            }

            foreach (var party in seatAllocations)
            {
                HttpContext.Response.Cookies[$"Seats_{party.Key}"].Value = party.Value.ToString();
            }

            string partyResultsString = string.Join(",", results);
            HttpContext.Response.Cookies["PartyResults"].Value = partyResultsString;
            HttpContext.Response.Cookies["TotalVotesAboveThreshold"].Value = totalVotesAboveThreshold.ToString();

            var partyCandidates = GetPartyCandidates(seatAllocations);

            // Optionally, pass the candidates to the view
            ViewBag.PartyCandidates = partyCandidates;

            return View();
        }














    }
}
