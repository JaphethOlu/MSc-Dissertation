using System;
using System.Collections.Generic;

namespace Master.Models
{
    public partial class Recruiter
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int OrganisationId { get; set; }

        public UserAccount EmailAddressNavigation { get; set; }
        public Organisation Organisation { get; set; }
    }
}
