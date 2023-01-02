using Microsoft.AspNetCore.Authentication;

namespace ApiGateway.Authentication
{
    public class SystemKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public IEnumerable<SystemKey>? SystemKeys { get; set; }
    }
}