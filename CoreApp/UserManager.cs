using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace CoreApp
{
    public class UserManager : IUserManager
    {
        private readonly MainContext _context;

        public UserManager(MainContext context)
        {
            this._context = context;

        }

        public SignUpResult SignUp(string name, string credentialTypeCode, string identifier)
        {
            return this.SignUp(name, credentialTypeCode, identifier, null);
        }

        public SignUpResult SignUp(string name, string credentialTypeCode, string identifier, string secret)
        {
            Users user = new Users();

            user.Name = name;
            user.Created = DateTime.Now.ToShortDateString();
            this._context.Users.Add(user);
            this._context.SaveChanges();

            CredentialTypes credentialType = this._context.CredentialTypes.FirstOrDefault(ct => string.Equals(ct.Code, credentialTypeCode, StringComparison.OrdinalIgnoreCase));

            if (credentialType == null)
                return new SignUpResult(success: false, error: SignUpResultError.CredentialTypeNotFound);

            Credentials credential = new Credentials();

            credential.UserId = user.Id;
            credential.CredentialTypeId = credentialType.Id;
            credential.Identifier = identifier;

            if (!string.IsNullOrEmpty(secret))
            {
                byte[] salt = Pbkdf2Hasher.GenerateRandomSalt();
                string hash = Pbkdf2Hasher.ComputeHash(secret, salt);

                credential.Secret = hash;
                credential.Extra = Convert.ToBase64String(salt);
            }

            this._context.Credentials.Add(credential);
            this._context.SaveChanges();
            return new SignUpResult(user: user, success: true);
        }

        public void AddToRole(Users user, string roleCode)
        {
            Roles role = this._context.Roles.FirstOrDefault(r => string.Equals(r.Code, roleCode, StringComparison.OrdinalIgnoreCase));

            if (role == null)
                return;

            this.AddToRole(user, role);
        }

        public void AddToRole(Users user, Roles role)
        {
            UserRoles userRole = this._context.UserRoles.Find(user.Id, role.Id);

            if (userRole != null)
                return;

            userRole = new UserRoles();
            userRole.UserId = user.Id;
            userRole.RoleId = role.Id;
            this._context.UserRoles.Add(userRole);
            this._context.SaveChanges();
        }

        public void RemoveFromRole(Users user, string roleCode)
        {
            Roles role = this._context.Roles.FirstOrDefault(r => string.Equals(r.Code, roleCode, StringComparison.OrdinalIgnoreCase));

            if (role == null)
                return;

            this.RemoveFromRole(user, role);
        }

        public void RemoveFromRole(Users user, Roles role)
        {
            UserRoles userRole = this._context.UserRoles.Find(user.Id, role.Id);

            if (userRole == null)
                return;

            this._context.UserRoles.Remove(userRole);
            this._context.SaveChanges();
        }

        public ChangeSecretResult ChangeSecret(string credentialTypeCode, string identifier, string secret)
        {
            CredentialTypes credentialType = this._context.CredentialTypes.FirstOrDefault(ct => string.Equals(ct.Code, credentialTypeCode, StringComparison.OrdinalIgnoreCase));

            if (credentialType == null)
                return new ChangeSecretResult(success: false, error: ChangeSecretResultError.CredentialTypeNotFound);

            Credentials credential = this._context.Credentials.FirstOrDefault(c => c.CredentialTypeId == credentialType.Id && c.Identifier == identifier);

            if (credential == null)
                return new ChangeSecretResult(success: false, error: ChangeSecretResultError.CredentialNotFound);

            byte[] salt = Pbkdf2Hasher.GenerateRandomSalt();
            string hash = Pbkdf2Hasher.ComputeHash(secret, salt);

            credential.Secret = hash;
            credential.Extra = Convert.ToBase64String(salt);
            this._context.Credentials.Update(credential);
            this._context.SaveChanges();
            return new ChangeSecretResult(success: true);
        }

        public ValidateResult Validate(string credentialTypeCode, string identifier)
        {
            return this.Validate(credentialTypeCode, identifier, null);
        }

        public ValidateResult Validate(string credentialTypeCode, string identifier, string secret)
        {
            CredentialTypes credentialType = this._context.CredentialTypes.FirstOrDefault(ct => string.Equals(ct.Code, credentialTypeCode, StringComparison.OrdinalIgnoreCase));

            if (credentialType == null)
                return new ValidateResult(success: false, error: ValidateResultError.CredentialTypeNotFound);

            Credentials credential = this._context.Credentials.FirstOrDefault(c => c.CredentialTypeId == credentialType.Id && c.Identifier == identifier);

            if (credential == null)
                return new ValidateResult(success: false, error: ValidateResultError.CredentialNotFound);

            if (!string.IsNullOrEmpty(secret))
            {
                byte[] salt = Convert.FromBase64String(credential.Extra);
                string hash = Pbkdf2Hasher.ComputeHash(secret, salt);

                if (credential.Secret != hash)
                    return new ValidateResult(success: false, error: ValidateResultError.SecretNotValid);
            }

            return new ValidateResult(user: this._context.Users.Find(credential.UserId), success: true);
        }

        public async void SignIn(HttpContext httpContext, Users user, bool isPersistent = false)
        {
            ClaimsIdentity identity = new ClaimsIdentity(this.GetUserClaims(user), CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await httpContext.SignInAsync(
              CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties() { IsPersistent = isPersistent }
            );
        }


        public async void SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public int GetCurrentUserId(HttpContext httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                return -1;

            Claim claim = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
                return -1;

            int currentUserId;

            if (!int.TryParse(claim.Value, out currentUserId))
                return -1;

            return currentUserId;
        }

        public Users GetCurrentUser(HttpContext httpContext)
        {
            long currentUserId = this.GetCurrentUserId(httpContext);

            if (currentUserId == -1)
                return null;

            return this._context.Users.Find(currentUserId);
        }

        private IEnumerable<Claim> GetUserClaims(Users user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            claims.AddRange(this.GetUserRoleClaims(user));
            return claims;
        }

        private IEnumerable<Claim> GetUserRoleClaims(Users user)
        {
            List<Claim> claims = new List<Claim>();
            IEnumerable<long> roleIds = _context.UserRoles.Where(ur => ur.UserId == user.Id).Select(ur => ur.RoleId).ToList();
            if (roleIds != null)
            {
                foreach (long roleId in roleIds)
                {
                    Roles role = this._context.Roles.Find(roleId);

                    claims.Add(new Claim(ClaimTypes.Role, role.Code));
                    claims.AddRange(this.GetUserPermissionClaims(role));
                }
            }

            return claims;
        }

        private IEnumerable<Claim> GetUserPermissionClaims(Roles role)
        {
            List<Claim> claims = new List<Claim>();
            IEnumerable<long> permissionIds = _context.RolePermissions.Where(rp => rp.RoleId == role.Id).Select(rp => rp.PermissionId).ToList();

            if (permissionIds != null)
            {
                foreach (long permissionId in permissionIds)
                {
                    Permissions permission = this._context.Permissions.Find(permissionId);

                    claims.Add(new Claim("Permission", permission.Code));
                }
            }

            return claims;
        }

    }
}
