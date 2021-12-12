using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReportsApi.Context;
using ReportsApi.Models;

namespace ReportsApi.Auth
{
    public class EmployeeAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string SchemeName = "Employee";
        private readonly ReportsDbContext _context;

        public EmployeeAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ReportsDbContext context) : base(options,
            logger,
            encoder,
            clock)
        {
            this._context = context;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // skip authentication if endpoint has [AllowAnonymous] attribute
            Endpoint endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return AuthenticateResult.NoResult();

            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            if (!authHeader.Scheme.Equals(SchemeName, StringComparison.InvariantCultureIgnoreCase))
            {
                return AuthenticateResult.Fail("Invalid Authorization Header Scheme");
            }
            if (!Guid.TryParse(authHeader.Parameter, out Guid employeeId))
            {
                return AuthenticateResult.Fail("Invalid Authorization Header Parameter");
            }

            Employee employee = await _context.Employees.AsNoTracking().SingleOrDefaultAsync(x => x.EmployeeId == employeeId);
            if (employee == null) {
                return AuthenticateResult.Fail("Invalid Employee Identity");
            }

            Claim[] claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, employee.EmployeeId.ToString()),
                new Claim(ClaimTypes.Name, employee.Name),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}