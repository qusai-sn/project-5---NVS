//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace E_voting_System.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class localCandidate
    {
        public int CandidateId { get; set; }
        public string National_ID { get; set; }
        public string Candidate_Name { get; set; }
        public string Type_Of_Chair { get; set; }
        public string List_Name { get; set; }
        public Nullable<int> Counter { get; set; }
        public string Picture { get; set; }
        public Nullable<int> List_ID { get; set; }
    }
}
