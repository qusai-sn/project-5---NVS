using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_voting_System.Models
{
    public class CirclesViewModel
    {
        public int ActiveCircleId { get; set; }
        public List<int> Circles { get; set; }
    }
}