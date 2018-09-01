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
        private IPasswordManager PasswordManager;
        private ITokenGenerator TokenGenerator;
        private IEmailValidator EmailValidator;
        private ILogin Login;
        private IAccount UserAccount;

		public LoginController(IContractorAccountRepository ContractorAccountRepository,
                               IAccount UserAccount, ILogin Login, ITokenGenerator TokenGenerator)
		{
            DbContext = new DissertationContext();
            this.ContractorAccountRepository = new ContractorAccountRepository(DbContext);
            this.UserAccount = UserAccount;
            this.Login = Login;
            this.TokenGenerator = TokenGenerator;
		}

        [AllowAnonymous]
        [HttpPost("contractor")]
        [ProducesResponseType(202)]
        [ProducesResponseType(401)]
        public IActionResult LoginContractor([FromForm] Login login,
                                             [FromServices] IEmailValidator emailValidator)
        {
            IActionResult response = Unauthorized();

            emailValidator = new EmailValidator();

            bool isEmailValid = emailValidator.IsValidEmail(login.EmailAddress);

            if(ModelState.IsValid)
            {
                if(isEmailValid == true)
                {
                    ContractorAccount contractor = ContractorAccountRepository.FindContractorAccount(login.EmailAddress);
                    if(Autheticate(login, contractor, PasswordManager) == true)
                    {
                        string userToken = BuildUserIdentity(contractor);
                        
                        var jsonResponse = new {
                            user = new {
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

        private bool Autheticate(ILogin login, IAccount userAccount, [FromServices] IPasswordManager passwordManager)
        {
            passwordManager = new PasswordManager();
            if(typeof(ContractorAccount) == userAccount.GetType())
            {
                return passwordManager.VerifyPassword(userAccount.Password, login.Password);
            }
            else
            {
                //throw new NotImplementedException();
                return false;
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