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

namespace UniverseSso.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<IEnumerable<LoginFieldModel>> GetLoginFields(CancellationToken ct, string providerName)
        {
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
        public async Task PostLogin(CancellationToken ct, string providerName, Dictionary<string, string> loginFields)
        {
            var providerFields = await _dbContext.Field
                .Where(x => x.Provider.ProviderName == providerName)
                .ToListAsync(ct);

            // TODO: Add login logic
        }
    }
}
