class AuthenticationFlags {
    public accountLocked: boolean
    public authFailedAttempts: number
    public passwordAgeInDays: number
    public requiresPasswordReset: boolean
    public requiresRecoveryOptionsSet: boolean
    public requiresTwoFactorAuthentication: boolean
    public sessionExists: boolean
    public sessionTransferChain: string[]
    public sessionTransferred: boolean
}

class AuthenticationReasons {
    public authenticated: boolean
    public successReasons: string[]
    public failureReasons: string[]
    public flags: AuthenticationFlags;
}

export {
    AuthenticationReasons,
    AuthenticationFlags
}