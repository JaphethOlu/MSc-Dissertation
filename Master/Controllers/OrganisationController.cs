using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Master.Models;
using Master.Services;
using Master.Contexts;
using Master.Repositories;
using Master.Interfaces.Models;
using Master.Interfaces.Services;
using Master.Interfaces.Repositories;

namespace Master.Controllers
{
    [Route("api/[controller]")]
    public class OrganisationController : Controller
    {
        private readonly DissertationContext DbContext;
        private IOrganisationRepository OrganisationRepository;

        public OrganisationController(IOrganisationRepository OrganisationRepository)
        {
            DbContext = new DissertationContext();
            this.OrganisationRepository = new OrganisationRepository(DbContext);
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult TopEmployer()
        {
            IActionResult response;
            
            var topEmployers = OrganisationRepository.GetMostContractsByEmployer();

            if(topEmployers.GetType().IsGenericType == true)
            {
                response = Json(topEmployers);
            }
            else
            {
                response = NotFound();
            }
            return response;
        }


        [AllowAnonymous]
        [HttpGet("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult TopAgency()
        {
            IActionResult response;
            
            var topAgencies = OrganisationRepository.GetMostContractsByEmployer();

            if(topAgencies.GetType().IsGenericType == true)
            {
                response = Json(topAgencies);
            }
            else
            {
                response = NotFound();
            }
            return response;
        }
    }
}