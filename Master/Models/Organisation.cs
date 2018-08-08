using System;
using System.Collections.Generic;

namespace Master.Models
{
    public partial class Organisation
    {
        public Organisation()
        {
            Contracts = new HashSet<Contract>();
            Recruiters = new HashSet<Recruiter>();
        }

        public int OrganisationId { get; set; }
        public string OrganisationName { get; set; }
        public string PersonalStatement { get; set; }
        public string Location { get; set; }
        public sbyte? NumberOfAvailableAdverts { get; set; }
        public string Director { get; set; }

        public User DirectorNavigation { get; set; }
        public ICollection<Contract> Contracts { get; set; }
        public ICollection<Recruiter> Recruiters { get; set; }
    }
}
