using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Master.Models
{
    public partial class Contract
    {
        [Required]
        [Range(10000, 2147483647)]
        public int ContractId { get; set; }

        [Required]
        [StringLength(75)]
        public string Position { get; set; }

        [Required]
        public DateTime? DateCreated { get; set; }

        [Required]
        [Range(101100, 2147483647)]
        public int OrganisationId { get; set; }

        [Required]
        [StringLength(30)]
        public string Location { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [Required]
        [Range(3, 24)]
        public sbyte Duration { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Organisation Organisation { get; set; }

        public Contract()
        {

        }

        public Contract(string position, int organisationId, string location, sbyte duration)
        {
            Position = position;
            OrganisationId = organisationId;
            Location = location;
            Duration = duration;
        }
    }
}
