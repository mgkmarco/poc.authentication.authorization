using Dapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CustomIdentityProviderSample.CustomProvider
{
    public class DapperUsersTable
    {
        private readonly SqlConnection _connection;
        public DapperUsersTable(SqlConnection connection)
        {
            _connection = connection;
        }

#region createuser
        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            string sql = "INSERT INTO dbo.[User] " +
                "VALUES " +
                "(" +
                    "@id, " +
                    "@Email, " +
                    "@EmailConfirmed, " +
                    "@NormalizedEmail, " +
                    "@PasswordHash, " +
                    "@UserName, " +
                    "@NormalizedUserName, " +
                    "@Name, " +
                    "@PhoneNumber, " +
                    "@PhoneNumberConfirmed, " +
                    "@TwoFactorEnabled, " +
                    "@AccessFailedCount, " +
                    "@LockoutEnabled, " +
                    "@LockoutEnd, " +
                    "@CreatedBy, " +
                    "@CreatedDate" +
                ")";

            int rows = await _connection.ExecuteAsync(sql, new 
                { 
                    user.Id, 
                    user.Email, 
                    user.EmailConfirmed,
                    user.NormalizedEmail,
                    user.PasswordHash,
                    user.UserName,
                    user.NormalizedUserName,
                    user.Name,
                    user.PhoneNumber,
                    user.PhoneNumberConfirmed,
                    user.TwoFactorEnabled,
                    user.AccessFailedCount,
                    user.LockoutEnabled,
                    user.LockoutEnd,
                    user.CreatedBy,
                    user.CreatedDate
                });

            if(rows > 0)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(new IdentityError { Description = $"Could not insert user {user.Email}." });
        }
#endregion

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user)
        {
            string sql = "DELETE FROM dbo.[User] WHERE Id = @Id";
            int rows = await _connection.ExecuteAsync(sql, new { user.Id });

            if(rows > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not delete user {user.Email}." });
        }


        public async Task<ApplicationUser> FindByIdAsync(Guid userId)
        {
            string sql = "SELECT * " +
                        "FROM dbo.[User] " +
                        "WHERE Id = @Id;";

            return await _connection.QuerySingleOrDefaultAsync<ApplicationUser>(sql, new
            {
                Id = userId
            });
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            string sql = "SELECT * " +
                        "FROM dbo.[User] " +
                        "WHERE UserName = @UserName;";

            return await _connection.QuerySingleOrDefaultAsync<ApplicationUser>(sql, new
            {
                UserName = userName
            });
        }

        //user login

        public async Task<SignInResult> CreateAsync(string userId, ExternalLoginInfo userLoginInfo)
        {
            string sql = "INSERT INTO dbo.[UserLogin] " +
                "VALUES " +
                "(" +
                    "@LoginProvider, " +
                    "@ProviderKey, " +
                    "@ProviderDisplayName, " +
                    "@UserId, " +
                    "@AuthenticationType, " +
                    "@IssuedDate " +
                ")";

            var issuedUtc = userLoginInfo.AuthenticationProperties.IssuedUtc.Value.UtcDateTime;

            int rows = await _connection.ExecuteAsync(sql, new
            {
                userLoginInfo.LoginProvider,
                userLoginInfo.ProviderKey,
                userLoginInfo.ProviderDisplayName,
                userId,
                userLoginInfo.Principal.Identity.AuthenticationType,
                IssuedDate = issuedUtc
            });

            //here should return additional data in the SignInResult for logging....
            if (rows <= 0)
            {
                return SignInResult.Success;
            }

            return SignInResult.Failed;
        }
    }
}
