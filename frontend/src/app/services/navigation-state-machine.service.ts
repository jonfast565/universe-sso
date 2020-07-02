import {
    Injectable
} from '@angular/core';
import {
    Router,
    ActivatedRoute
} from '@angular/router';
import {
    AuthServiceService
} from '../auth-service.service';

@Injectable({
    providedIn: 'root'
})
export class NavigationStateMachineService {

    constructor(private route: ActivatedRoute,
        private router: Router,
        private authService: AuthServiceService) {}

    public navigateNext() {
        let flags = this.authService.getAuthenticationFlags();
        let page: string = null;
        let pageSet: boolean = false;

        if (flags.accountLocked && !pageSet) {
            page = "accountLocked";
            pageSet = true;
            flags.accountLocked = false;
        }

        if (flags.requiresTwoFactorAuthentication && !pageSet) {
            page = "2fa";
            pageSet = true;
            flags.requiresTwoFactorAuthentication = false;
        }

        if (flags.requiresPasswordReset && !pageSet) {
            page = "resetCredential";
            pageSet = true;
            flags.requiresPasswordReset = false;
        }

        if (flags.requiresRecoveryOptionsSet && !pageSet) {
            page = "recoveryOptions"
            pageSet = true;
            flags.requiresRecoveryOptionsSet = false;
        }

        if (!pageSet) {
            page = "authenticationJump";
            pageSet = true;
        }

        this.authService.setAuthenticationFlags(flags);
        this.navigateToPage(page);
    }

    public navigateToPage(pageName: string) {
        this.router.navigate([pageName], {
            queryParamsHandling: 'merge'
        });
    }

    public navigateToErrorPage(error: any) {
        this.router.navigate(['error'], {
            queryParamsHandling: 'merge'
        });
    }
}