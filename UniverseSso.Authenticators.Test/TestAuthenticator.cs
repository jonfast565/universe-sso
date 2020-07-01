﻿using System;
using System.Collections.Generic;
using UniverseSso.Models;
using UniverseSso.Models.Implementation;
using UniverseSso.Models.Interfaces;

namespace UniverseSso.Authenticators.Test
{
    public class TestAuthenticator : IAuthenticationStrategy
    {
        public string ProviderName { get; set; } = "Test";

        public AuthenticationReasons Authenticate(IDictionary<string, object> loginFields)
        {
            return new AuthenticationReasons
            {
                Authenticated = true,
                SuccessReasons = new[] { "nothing implemented yet" },
                FailureReasons = null,
                Flags = new AuthenticationFlags
                {
                    RequiresPasswordReset = true,
                    PasswordAgeInDays = 1,
                    AccountLocked = false,
                    AuthFailedAttempts = 0,
                    SessionExists = false,
                    SessionTransferred = false,
                    SessionTransferChain = new string[] { },
                    RequiresRecoveryOptionsSet = true,
                    RequiresTwoFactorAuthentication = false
                }
            };
        }
    }
}
