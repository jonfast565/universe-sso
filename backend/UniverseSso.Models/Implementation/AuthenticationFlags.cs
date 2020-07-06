namespace UniverseSso.Models.Implementation
{
    public class AuthenticationFlags
    {
        public bool RequiresPasswordReset { get; set; }
        public int PasswordAgeInDays { get; set; }
        public bool AccountLocked { get; set; }
        public int AuthFailedAttempts { get; set; }
        public bool SessionExists { get; set; }
        public bool SessionTransferred { get; set; }
        public string[] SessionTransferChain { get; set; }
        public bool RequiresRecoveryOptionsSet { get; set; }
        public bool RequiresTwoFactorAuthentication { get; set; }
    }
}
