using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using BL.DTO.Common;
using BL.DTO.Filters;
using BL.Facades.Common;
using BL.Services.Accounts;
using Game.Infrastructure.UnitOfWork;

namespace BL.Facades
{
    public class AccountFacade : FacadeBase
    {
        private readonly IAccountService _accountService;

        public AccountFacade(IUnitOfWorkProvider unitOfWorkProvider, IAccountService accountService) : base(unitOfWorkProvider)
        {
            this._accountService = accountService;
        }

        /// <summary>
        /// Gets customer according to email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Customer with specified email</returns>
        public async Task<AccountDto> GetCustomerAccordingToEmailAsync(string email)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _accountService.GetAccountAccordingToEmailAsync(email);
            }          
        }

        /// <summary>
        /// Gets all customers according to page
        /// </summary>
        /// <returns>all customers</returns>
        public async Task<QueryResultDto<AccountDto, AccountFilterDto>> GetAllCustomersAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _accountService.ListAllAsync();
            }
        }

        ///// <summary>
        ///// Performs account registration
        ///// </summary>
        ///// <param name="registrationDto">Account registration details</param>
        ///// <param name="success">argument that tells whether the registration was successful</param>
        ///// <returns>Registered account ID</returns>
        public int RegisterAccount(AccountCreateDto registrationDto, out bool success)
        {
            if (_accountService.GetAccountAccordingToEmailAsync(registrationDto.Email) != null)
            {
                success = false;
                return -1;
            }
            if (_accountService.GetAccountAccordingToUsernameAsync(registrationDto.Username) != null)
            {
                success = false;
                return -2;
            }

            var accountId = _accountService.Register(registrationDto);
            success = true;
            return accountId;
        }


    }
}
