using System;
using Jose;
using Nancy;
using Nancy.Authentication.Stateless;
using Nancy.Bootstrapper;
using Nancy.Security;
using Nancy.TinyIoc;

namespace Repetitions
{
    public class CustomNancyBootstrapper : DefaultNancyBootstrapper
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
}