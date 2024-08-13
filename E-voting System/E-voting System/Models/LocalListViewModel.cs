using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_voting_System.Models
{
    public class LocalListViewModel
    {
        public int? SelectedListId { get; set; }
        public List<int> SelectedCandidateIds { get; set; }
        public List<localList> LocalLists { get; set; }
        public Dictionary<int, List<localCandidate>> CandidatesByList { get; set; }
    }


}