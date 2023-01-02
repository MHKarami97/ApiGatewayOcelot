using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace ApiGateway.Authentication
{
    public class SystemKeyAuthenticationHandler : AuthenticationHandler<SystemKeyAuthenticationOptions>
    {
        private const string Authentication = nameof(Authentication);

        public SystemKeyAuthenticationHandler(IOptionsMonitor<SystemKeyAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Options.SystemKeys is null)
            {
                throw new InvalidOperationException("Please Configure Scheme Options");
            }

            var header = Request.Headers[Authentication].FirstOrDefault();

            if (header is null)
            {
                return Task.FromResult(AuthenticateResult.Fail("Authentication Header Was Empty"));
            }

            var system = Options.SystemKeys.FirstOrDefault(s => string.Equals(s.Key, header, StringComparison.OrdinalIgnoreCase));

            if (system is null)
            {
                return Task.FromResult(AuthenticateResult.Fail("System Key Was Not Registered"));
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, system.Name),
            };
            var claimsIdentity = new ClaimsIdentity(claims, nameof(SystemKeyAuthenticationHandler));
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
