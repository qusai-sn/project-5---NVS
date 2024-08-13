using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_voting_System.Models
{
    public class ListViewModel
    {
        public List<localList> LocalLists { get; set; }
        public Dictionary<int, List<localCandidate>> CandidatesByList { get; set; }

        public List<PartyList> PartyLists { get; set; }
        public Dictionary<int, List<PartyCandidate>> PartyCandidatesByList { get; set; }


    }
}