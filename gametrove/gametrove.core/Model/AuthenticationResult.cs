using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Gametrove.Core.Model
{
    public class AuthenticationResult
    {
        public string IdToken { get; set; }

        public string AccessToken { get; set; }

        public IEnumerable<Claim> UserClaims { get; set; }

        public IEnumerable<Claim> Roles => UserClaims.Where(c =>
                c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");

        public bool IsError { get; }

        public string Error { get; }

        public AuthenticationResult() { }

        public AuthenticationResult(bool isError, string error)
        {
            IsError = isError;
            Error = error;
        }
    }
}