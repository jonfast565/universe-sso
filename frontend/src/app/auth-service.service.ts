import {
    Injectable
} from '@angular/core';
import {
    CookieStoreService
} from './services/cookie-store.service';
import {
    AuthenticationFlags
} from './models/authentication';

@Injectable({
    providedIn: 'root'
})
export class AuthServiceService {
    private authFlagsCookie: string = 'authFlagsCookie';
    constructor(private cookieStoreService: CookieStoreService) {}

    public isAuthenticated(): boolean {
        let authFlags = this.getAuthenticationFlags();
        if (authFlags) {
            return true;
        }
        return false;
    }

    public setAuthenticationFlags(flags: AuthenticationFlags) {
        this.cookieStoreService.storeCookie(this.authFlagsCookie, JSON.stringify(flags));
    }

    public getAuthenticationFlags(): AuthenticationFlags {
        try {
            let flags = < AuthenticationFlags > JSON.parse(this.cookieStoreService.getCookie(this.authFlagsCookie));
            return flags;
        } catch (e) {
            return null;
        }
    }
}