using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Master.Models
{
    public class ContractorProfile
    {
        [Required]
		[EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }
        
        [StringLength(120)]
        public string Headline { get; set; }

        [StringLength(800)]
        public string PersonalStatement { get; set; }
        
        public string Location { get; set; }

        public ContractorAccount ProfileAccount { get; set; }
    }
}
