using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UniverseSso.Entities;
using UniverseSso.Models;
using UniverseSso.Models.Implementation;
using Newtonsoft.Json;
using UniverseSso.Utilities;

namespace UniverseSso.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LoginDbContext _dbContext;
        private readonly ILogger<LoginController> _logger;

        public LoginController(LoginDbContext dbContext, ILogger<LoginController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet]
        [Route("providers")]
        public async Task<IEnumerable<ProviderViewModelSlim>> GetProviders(CancellationToken ct)
        {
            var providers = await _dbContext.Provider
                .Where(x => x.Enabled)
                .Select(x => new ProviderViewModelSlim
                {
                    Name = x.ProviderName,
                    Logo = $"data:image/png;base64, {Convert.ToBase64String(x.ProviderLogo)}"
                }).ToListAsync(ct);

            return providers;
        }

        [HttpGet]
        [Route("provider")]
        public async Task<ProviderViewModel> GetProvider(CancellationToken ct, string providerName)
        {
            if (string.IsNullOrEmpty(providerName))
            {
                throw new Exception($"Provider {providerName} not found.");
            }

            var provider = await _dbContext.Provider
                .FirstAsync(x => x.Enabled && x.ProviderName == providerName, ct);

            return new ProviderViewModel
            {
                Name = provider.ProviderName,
                Logo = $"data:image/png;base64, {Convert.ToBase64String(provider.ProviderLogo)}",
                Background = $"data:image/png;base64, {Convert.ToBase64String(provider.ProviderBackground)}"
            };
        }

        [HttpGet]
        [Route("fields")]
        public async Task<IEnumerable<LoginFieldModel>> GetFields(CancellationToken ct, string providerName, string pageType)
        {
            if (string.IsNullOrEmpty(providerName))
            {
                throw new Exception($"Provider not provided.");
            }

            if (string.IsNullOrEmpty(pageType))
            {
                throw new Exception($"Page type not provider.");
            }

            var provider = await _dbContext.Provider
                .FirstOrDefaultAsync(x => x.ProviderName == providerName, ct);

            if (provider == null)
            {
                throw new Exception($"Provider {providerName} not found");
            }

            var providerFields = await _dbContext.Field
                .Where(x => x.ProviderId == provider.ProviderId
                && x.PageType == pageType)
                .OrderBy(x => x.Order)
                .ToListAsync(ct);

            var loginFields = providerFields.Select(x => new LoginFieldModel
            {
                FieldName = x.FieldName,
                FieldType = x.FieldType,
                OptionalFieldValues = x.OptionalFieldValues,
                Required = x.Required,
                Pattern = x.Pattern
            });

            return loginFields;
        }

        [HttpPost]
        [Route("login")]
        public async Task<AuthenticationReasons> PostLogin(CancellationToken ct, [FromQuery] string providerName, [FromBody] Dictionary<string, object> loginFields)
        {
            if (string.IsNullOrEmpty(providerName))
            {
                throw new Exception("Provider not provided");
            }

            var provider = await _dbContext.Provider
                .FirstOrDefaultAsync(x => x.ProviderName == providerName, ct);

            if (provider == null)
            {
                throw new Exception($"Provider {providerName} not found.");
            }

            var authStrategy = AuthenticationUtilities.LoadAuthenticationStrategyByProvider(providerName);
            var reasons = authStrategy.Authenticate(loginFields);
            return reasons;
        }
    }
}
