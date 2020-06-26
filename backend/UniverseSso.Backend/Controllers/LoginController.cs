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
        public async Task<IEnumerable<ProviderViewModel>> GetProviders(CancellationToken ct)
        {
            var provider = await _dbContext.Provider
                .Where(x => x.Enabled)
                .ToListAsync(ct);

            return provider.Select(x => new ProviderViewModel
            {
                Name = x.ProviderName,
                Logo = x.ProviderLogo,
                Background = x.ProviderBackground
            });
        }

        [HttpGet]
        [Route("fields")]
        public async Task<IEnumerable<LoginFieldModel>> GetLoginFields(CancellationToken ct, string providerName)
        {
            if (string.IsNullOrEmpty(providerName))
            {
                return new List<LoginFieldModel>();
            }

            var provider = await _dbContext.Provider
                .FirstAsync(x => x.ProviderName == providerName, ct);

            var providerFields = await _dbContext.Field
                .Where(x => x.ProviderId == provider.ProviderId)
                .ToListAsync(ct);

            var loginFields = providerFields.Select(x => new LoginFieldModel
            {
                FieldName = x.FieldName,
                // FieldType = x.FieldType,
                OptionalFieldValues = x.OptionalFieldValues,
            });

            return loginFields;
        }

        [HttpPost]
        [Route("login")]
        public async Task PostLogin(CancellationToken ct, string providerName, Dictionary<string, string> loginFields)
        {
            var providerFields = await _dbContext.Field
                .Where(x => x.Provider.ProviderName == providerName)
                .ToListAsync(ct);

            // TODO: Add login logic
        }
    }
}
