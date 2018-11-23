using AutoMapper;
using BL.DTO;
using BL.DTO.Filters;
using BL.QueryObject;
using BL.Services.Common;
using Game.DAL.Entity.Entities;
using Game.Infrastructure;
using Game.Infrastructure.Query;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BL.Services.Accounts
{
    public class AccountService : CrudQueryServiceBase<Account, AccountDto, AccountFilterDto>, IAccountService
    {
        private const int PBKDF2IterCount = 100000;
        private const int PBKDF2SubkeyLength = 160 / 8;
        private const int saltSize = 128 / 8;

        public AccountService(IMapper mapper, IRepository<Account> repository, QueryObjectBase<AccountDto, Account, AccountFilterDto, IQuery<Account>> query) : base(mapper, repository, query)
        {
        }

        protected override async Task<Account> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Character));
        }

        public async Task<AccountDto> GetAccountAccordingToEmailAsync(string email)
        {
            var queryResult = await Query.ExecuteQuery(new AccountFilterDto { Email = email });
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<AccountDto> GetAccountAccordingToUsernameAsync(string username)
        {
            var queryResult = await Query.ExecuteQuery(new AccountFilterDto { Username = username });
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<Guid> RegisterAccountAsync(AccountCreateDto account)
        {
            var emailAccount = await GetAccountAccordingToEmailAsync(account.Email);
            var usernameAccount = await GetAccountAccordingToUsernameAsync(account.Username);

            if (emailAccount != null || usernameAccount != null)
            {
                throw new ArgumentException();
            }

            var password = CreateHash(account.Password);

            var newAccount = new AccountDto()
            {
                Username = account.Username,
                Email = account.Email,
                PasswordHash = password.Item1,
                PasswordSalt = password.Item2
            };

            return Create(newAccount);
        }

        public async Task<(bool success, Guid id, string roles)> AuthorizeUserAsync(string usernameOrEmail, string password)
        {
            var emailAccount = await GetAccountAccordingToEmailAsync(usernameOrEmail);
            var usernameAccount = await GetAccountAccordingToUsernameAsync(usernameOrEmail);

            if (emailAccount != null)
            {
                return (VerifyHashedPassword(emailAccount.PasswordHash, emailAccount.PasswordSalt, password), emailAccount.Id, emailAccount.Roles);
            }
            if (usernameAccount != null)
            {
                return (VerifyHashedPassword(usernameAccount.PasswordHash, usernameAccount.PasswordSalt, password), usernameAccount.Id, usernameAccount.Roles);
            }
            return (false, Guid.Empty, null);
        }

        private bool VerifyHashedPassword(string hashedPassword, string salt, string password)
        {
            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            var saltBytes = Convert.FromBase64String(salt);

            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, PBKDF2IterCount))
            {
                var generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                return hashedPasswordBytes.SequenceEqual(generatedSubkey);
            }
        }

        private Tuple<string, string> CreateHash(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                return Tuple.Create(Convert.ToBase64String(subkey), Convert.ToBase64String(salt));
            }
        }
    }
}