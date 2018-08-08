using System;
using System.Collections.Generic;

namespace Master.Models
{
    public class Contractor
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Headline { get; set; }
        public string PersonalStatement { get; set; }
        public string Location { get; set; }

        public User EmailAddressNavigation { get; set; }
    }
}
