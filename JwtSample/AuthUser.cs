using System.Collections.Generic;
using Nancy.Security;

namespace Repetitions
{
    internal class AuthUser : IUserIdentity
    {
        public AuthUser(string userName, IEnumerable<string> claims)
        {
            UserName = userName;
            Claims = claims;
        }

        public string UserName { get; }
        public IEnumerable<string> Claims { get; }
    }
}