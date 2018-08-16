using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
    public class RegisterController : Controller
    {
		private readonly DissertationContext dbContext;
        private IContractorAccountRepository contractorAccountRepository;
        private IPasswordHasher passwordHasher;
        private IContractorAccount contractor;

		public RegisterController(IContractorAccountRepository contractorAccountRepository,
                                           IContractorAccount contractor)
		{
			dbContext = new DissertationContext();
            this.contractorAccountRepository = new ContractorAccountRepository(dbContext);
            this.contractor = contractor;
		}
		
        [HttpPost("contractor")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public HttpResponseMessage RegisterContractor([FromForm] ContractorAccount contractor,
                                        [FromServices] IPasswordHasher passwordHasher)
		{
            if(ModelState.IsValid)
            {
                bool AccountExist = contractorAccountRepository.CheckIfAccountExist(contractor.EmailAddress);
                
                if(AccountExist)
                {
                    return new HttpResponseMessage(HttpStatusCode.Forbidden);
                }
                
                ContractorAccount newContractorAccount;

                passwordHasher = new PasswordHasher();

                string encryptedPassword = passwordHasher.GeneratePassword(contractor.Password);

                newContractorAccount = new ContractorAccount
                {
                    EmailAddress = contractor.EmailAddress,
                    Password = encryptedPassword,
                    FirstName = contractor.FirstName,
                    LastName = contractor.LastName
                };

                contractorAccountRepository.SaveContractorAccount(newContractorAccount);

                contractorAccountRepository.MarkAsModified(newContractorAccount);

                HttpResponseMessage accountCreatedResponse = new HttpResponseMessage(HttpStatusCode.Created);

                return accountCreatedResponse;
            }
            else
            {
                HttpResponseMessage invalidModelResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return invalidModelResponse;
            }
		}		
    }
}