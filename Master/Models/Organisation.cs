using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Master.Models
{
    public partial class Organisation
    {
        [Required]
        [Range(101100, 2147483647)]
        public int OrganisationId { get; set; }

        [Required]
        [StringLength(75)]
        public string OrganisationName { get; set; }

        [Required]
        public OrganisationType OrganisationType { get; set; }

        [StringLength(1500)]
        public string OrganisationStatement { get; set; }

        [Required]
        [StringLength(30)]
        public string Location { get; set; }

        [Range(0, 65535)]
        public ushort? NumberOfAvailableAdverts { get; set; }

        [Required]
        [EmailAddress]
        public string Director { get; set; }

        public DirectorAccount DirectorAccount { get; set; }
        public ICollection<Contract> Contracts { get; set; }
        public ICollection<RecruiterAccount> Recruiters { get; set; }
        
        public Organisation()
        {
            Contracts = new HashSet<Contract>();
            Recruiters = new HashSet<RecruiterAccount>();
        }

        public Organisation(string orgName, OrganisationType orgType, string location, string director)
        {
            OrganisationName = orgName;
            OrganisationType = orgType;
            Location = location;
            Director = director;

            Contracts = new HashSet<Contract>();
            Recruiters = new HashSet<RecruiterAccount>();
        }
    }
}
