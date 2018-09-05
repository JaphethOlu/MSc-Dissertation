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
    public class RegisterController : Controller
    {
		private readonly DissertationContext DbContext;
        private IContractorAccountRepository ContractorAccountRepository;
        private IContractorProfileRepository ContractorProfileRepository;
        private ITokenGenerator TokenGenerator;

		public RegisterController(IContractorAccountRepository ContractorAccountRepository,
								  IContractorProfileRepository ContractorProfileRepository,
                                  ITokenGenerator TokenGenerator)
		{
			DbContext = new DissertationContext();
            this.ContractorAccountRepository = new ContractorAccountRepository(DbContext);
			this.ContractorProfileRepository = new ContractorProfileRepository(DbContext);
            this.TokenGenerator = TokenGenerator;
		}
		
        [AllowAnonymous]
        [HttpPost("contractor")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult RegisterContractor([FromForm] ContractorAccount contractor)
		{
            IActionResult response;

            if(ModelState.IsValid)
            {

                bool AccountExist = ContractorAccountRepository.CheckIfAccountExist(contractor.EmailAddress);

                EmailValidator EmailValidator = new EmailValidator();

                bool isEmailValid = EmailValidator.IsValidEmail(contractor.EmailAddress);

                if(AccountExist == true || (isEmailValid == false))
                {
                    string errorMessage = "This email address is already in use or invalid";
                    response = BadRequest(new { error = errorMessage});
                }
                else
                {
                    PasswordManager PasswordManager = new PasswordManager();

                    string encryptedPassword = PasswordManager.GeneratePassword(contractor.Password);

                    contractor.Password = encryptedPassword;

                    ContractorAccountRepository.MarkAsModified(contractor);

                    string userToken = BuildUserIdentity(contractor);

                    ContractorAccountRepository.SaveNewContractorAccount(contractor);

					CreateContractorProfile(contractor);

                    var jsonResponse = new {
                            user = new {
                                account = contractor.EmailAddress,
                                token = userToken,
                                role = "contractor"
                            }
                        };

                    response = Ok(jsonResponse);
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
                authenticationToken = TokenGenerator.GenerateToken(contractor);
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

		private void CreateContractorProfile(ContractorAccount contractorAccount)
		{
			ContractorProfile ContractorProfile = new ContractorProfile
			{
				EmailAddress = contractorAccount.EmailAddress,
				FirstName = contractorAccount.FirstName,
				LastName = contractorAccount.LastName
			};

			ContractorProfileRepository.SaveNewContractorProfile(ContractorProfile);
		}
    }
}