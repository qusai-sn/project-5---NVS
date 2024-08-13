using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_voting_System.Models
{
    public class PartyListViewModel
    {
        public int? SelectedPartyId { get; set; }
        public List<PartyList> PartyLists { get; set; }
    }


}