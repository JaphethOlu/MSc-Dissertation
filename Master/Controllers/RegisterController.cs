using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
        private ITokenGenerator tokenGenerator;
        private IAccount contractor;

		public RegisterController(IContractorAccountRepository contractorAccountRepository,
                                  IAccount contractor,
                                  ITokenGenerator tokenGenerator)
		{
			dbContext = new DissertationContext();
            this.contractorAccountRepository = new ContractorAccountRepository(dbContext);
            this.contractor = contractor;
            this.tokenGenerator = tokenGenerator;
		}
		
        [AllowAnonymous]
        [HttpPost("contractor")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult RegisterContractor([FromForm] ContractorAccount contractor,
                                        [FromServices] IPasswordHasher passwordHasher)
		{
            IActionResult response;

            if(ModelState.IsValid)
            {
                bool AccountExist = contractorAccountRepository.CheckIfAccountExist(contractor.EmailAddress);

                if(AccountExist == true)
                {
                    string errorMessage = "This email address is already in use";
                    response = BadRequest(new { error = errorMessage});
                }
                else
                {
                    passwordHasher = new PasswordHasher();

                    string encryptedPassword = passwordHasher.GeneratePassword(contractor.Password);

                    contractor.Password = encryptedPassword;

                    contractorAccountRepository.MarkAsModified(contractor);

                    string userToken = BuildUserIdentity(contractor);

                    contractorAccountRepository.SaveContractorAccount(contractor);

                    response = Ok(new { token = userToken });
                }
                return response;
            }
            else
            {
                return new BadRequestObjectResult(ModelState);
            }
		}

        private string BuildUserIdentity(IAccount userAccount)
        {
            string authenticationToken = null;

            if(userAccount.GetType() == typeof(ContractorAccount))
            {
                Contractor contractor = new Contractor
                {
                    EmailAddress = userAccount.EmailAddress,
                    FirstName = userAccount.FirstName,
                    LastName = userAccount.LastName
                };
                authenticationToken = tokenGenerator.GenerateToken(contractor);
            }
            else
            {
                /*
                Recruiter recruiter = new Recruiter
                {
                    EmailAddress = userAccount.EmailAddress,
                    FirstName = userAccount.FirstName,
                    LastName = userAccount.LastName
                };
                */
                throw new NotImplementedException();
            }
            return authenticationToken;
        }
    }
}