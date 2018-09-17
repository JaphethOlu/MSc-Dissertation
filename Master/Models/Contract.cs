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
        public string JobTitle { get; set; }

        [Required]
        public DateTime? DateAdded { get; set; }

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
        
        [Required]
        [Range(30, 1000)]
        public int MinimumSalary { get; set; }

        [Required]
        [Range(30, 1000)]
        public int MaximumSalary { get; set; }

        [Url]
        public string ApplicationUrl { get; set; }

        public Organisation Organisation { get; set; }

        public Contract()
        {

        }
    }
}
