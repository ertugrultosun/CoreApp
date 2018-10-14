using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.Models;
using Microsoft.AspNetCore.Http;

namespace CoreApp
{
    public enum SignUpResultError
    {
        CredentialTypeNotFound
    }

    public class SignUpResult
    {
        public Users User { get; set; }
        public bool Success { get; set; }
        public SignUpResultError? Error { get; set; }

        public SignUpResult(Users user = null, bool success = false, SignUpResultError? error = null)
        {
            this.User = user;
            this.Success = success;
            this.Error = error;
        }
    }

    public enum ValidateResultError
    {
        CredentialTypeNotFound,
        CredentialNotFound,
        SecretNotValid
    }

    public class ValidateResult
    {
        public Users User { get; set; }
        public bool Success { get; set; }
        public ValidateResultError? Error { get; set; }

        public ValidateResult(Users user = null, bool success = false, ValidateResultError? error = null)
        {
            this.User = user;
            this.Success = success;
            this.Error = error;
        }
    }

    public enum ChangeSecretResultError
    {
        CredentialTypeNotFound,
        CredentialNotFound
    }

    public class ChangeSecretResult
    {
        public bool Success { get; set; }
        public ChangeSecretResultError? Error { get; set; }

        public ChangeSecretResult(bool success = false, ChangeSecretResultError? error = null)
        {
            this.Success = success;
            this.Error = error;
        }
    }

    public interface IUserManager
    {
        SignUpResult SignUp(string name, string credentialTypeCode, string identifier);
        SignUpResult SignUp(string name, string credentialTypeCode, string identifier, string secret);
        void AddToRole(Users user, string roleCode);
        void AddToRole(Users user, Roles role);
        void RemoveFromRole(Users user, string roleCode);
        void RemoveFromRole(Users user, Roles role);
        ChangeSecretResult ChangeSecret(string credentialTypeCode, string identifier, string secret);
        ValidateResult Validate(string credentialTypeCode, string identifier);
        ValidateResult Validate(string credentialTypeCode, string identifier, string secret);
        void SignIn(HttpContext httpContext, Users user, bool isPersistent = false);
        void SignOut(HttpContext httpContext);
        int GetCurrentUserId(HttpContext httpContext);
        Users GetCurrentUser(HttpContext httpContext);
    }
}
