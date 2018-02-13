using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using Jose;
using Nancy;
using Nancy.Authentication.Stateless;
using Nancy.Bootstrapper;
using Nancy.Extensions;
using Nancy.Hosting.Self;
using Nancy.Security;
using Nancy.TinyIoc;

namespace Repetitions
{
    public class Secret
    {
        public static readonly byte[] Key 
            = new byte[]{164,60,194,0,161,189,41,38,130,89,141,164,45,170,159,209,69,137,243,216,191,131,47,250,32,107,231,117,37,158,225,234};
    }
    
    public class DummyModule : NancyModule
    {
        public DummyModule()
        {
            this.RequiresAuthentication();
            Get["/Dummy"] = _ => GetAllCategories();
        }

        private object GetAllCategories()
        {
            return new[] {33, 3, 3, 3, 3, 3, 3, 3};
        }
    }

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
    
    internal sealed class AuthToken
    {
        public string UserName { get; set; }
        public IEnumerable<string> Claims { get; set; }
        public DateTime ExpirationDateTime { get; set; }
    }
    
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
    

    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        private const string BearerDeclaration = "Bearer ";
        
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            StatelessAuthentication.Enable(pipelines, new StatelessAuthenticationConfiguration(Authenticate));   
            base.ApplicationStartup(container, pipelines);
        }

        private IUserIdentity Authenticate(NancyContext context)
        {
            var authorizationHeader = context.Request.Headers.Authorization;
            var jwt = authorizationHeader.Substring(BearerDeclaration.Length);
 
            var authToken = JWT.Decode<AuthToken>(jwt, Secret.Key, JwsAlgorithm.HS256);
 
            if(authToken.ExpirationDateTime < DateTime.UtcNow)
                return null;
            return new AuthUser(authToken.UserName, authToken.Claims);                     
        }
    }
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            using (var host = new NancyHost(new Uri("http://localhost:19123")))
            {
                host.Start();
                Console.WriteLine("Running on http://localhost:19123");

                new DatabaseConnectionInitializer().Start();
                Console.ReadLine();
            }
        }
    }
}
