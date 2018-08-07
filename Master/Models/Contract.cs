using System;
using System.Collections.Generic;

namespace Master.Model
{
    public partial class Contract
    {
        public int ContractId { get; set; }
        public string Position { get; set; }
        public DateTime? DateCreated { get; set; }
        public int OrganisationId { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public sbyte Duration { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Organisation Organisation { get; set; }
    }
}
