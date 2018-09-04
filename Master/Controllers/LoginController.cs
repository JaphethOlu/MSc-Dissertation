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
	public class LoginController : Controller
	{
        private readonly DissertationContext DbContext;
        private IContractorAccountRepository ContractorAccountRepository;
        private ITokenGenerator TokenGenerator;

		public LoginController(IContractorAccountRepository ContractorAccountRepository,
							   ITokenGenerator TokenGenerator)
		{
            DbContext = new DissertationContext();
            this.ContractorAccountRepository = new ContractorAccountRepository(DbContext);
            this.TokenGenerator = TokenGenerator;
		}

        [AllowAnonymous]
        [HttpPost("contractor")]
        [ProducesResponseType(202)]
        [ProducesResponseType(401)]
        public IActionResult LoginContractor([FromForm] Login login)
        {
            IActionResult response = Unauthorized();

            if(ModelState.IsValid)
            {
				IAccount contractor = CredentialsChecker(login.EmailAddress);
				if (contractor != null)
				{
					if (Autheticate(login, contractor) == true)
					{
						string userToken = BuildUserIdentity(contractor);

						var jsonResponse = new
						{
							user = new
							{
								account = login.EmailAddress,
								token = userToken,
								role = "contractor"
							}
						};

						response = Accepted(jsonResponse);
					}
					else
					{
						response = Unauthorized();
					}
				}
                else
                {
                    response = Unauthorized();
                }
                return response;
            }
            else
            {
                return new BadRequestObjectResult(ModelState);
            }
        }

        private bool Autheticate(ILogin login, IAccount userAccount)
        {
            PasswordManager PasswordManager = new PasswordManager();
            if(typeof(ContractorAccount) == userAccount.GetType())
            {
                return PasswordManager.VerifyPassword(userAccount.Password, login.Password);
            }
            else
            {
                //throw new NotImplementedException();
                return false;
            }
            
        }

		private IAccount CredentialsChecker(string emailAddress)
		{
			ContractorAccount contractor;
			EmailValidator EmailValidator = new EmailValidator();
			if(EmailValidator.IsValidEmail(emailAddress) == true)
			{
				contractor = ContractorAccountRepository.FindContractorAccount(emailAddress);
				return contractor;
			}
			else
			{
				contractor = null;
				return contractor;
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
	}

}