using System;
using Jose;
using Nancy;

namespace Repetitions
{
    public class LoginModule : NancyModule
    {
        public LoginModule()
        {
            Post["/Login"] = _ => Login();
        }

        private object Login()
        {
            // TODO here access the login and password hash in the request body
            var newToken = new AuthToken
            {
                UserName = "anon",
                ExpirationDateTime = DateTime.UtcNow.Add(TimeSpan.FromHours(1)),
                Claims = new[] {"a", "b", "c"}
            };
            return JWT.Encode(newToken, Secret.Key, JwsAlgorithm.HS256);
        }
    }
}