using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_voting_System.Models
{
    public class CircleViewModel
    {
        public List<USER> users_circle_local { get; set; }
        public int localvotecounter { get; set; }
        public int whitelocalcounter { get; set; }
        public double threshold { get; set; }
        public int totalVotes { get; set; }
        public List<localList> winning_lists { get; set; }
        public Dictionary<int, List<localCandidate>> winningCandidates { get; set; } // key: list ID, value: list of top candidates



    }

}