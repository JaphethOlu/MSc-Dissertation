using System;
using System.Collections.Generic;

namespace Master.Models
{
    public partial class Organisation
    {
        public Organisation()
        {
            Contracts = new HashSet<Contract>();
            Recruiters = new HashSet<RecruiterAccount>();
        }

        public int OrganisationId { get; set; }
        public string OrganisationName { get; set; }
        public string PersonalStatement { get; set; }
        public string Location { get; set; }
        public sbyte? NumberOfAvailableAdverts { get; set; }
        public string Director { get; set; }

        public RecruiterAccount DirectorNavigation { get; set; }
        public ICollection<Contract> Contracts { get; set; }
        public ICollection<RecruiterAccount> Recruiters { get; set; }
    }
}
