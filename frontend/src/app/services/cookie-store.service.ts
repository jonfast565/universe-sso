import {
    Injectable
} from '@angular/core';
import {
    CookieService
} from 'ngx-cookie-service';

@Injectable({
    providedIn: 'root'
})
export class CookieStoreService {
    constructor(private cookieStore: CookieService) {}

    public storeCookie(cookieName: string, cookieValue: string) {
        this.cookieStore.set(cookieName, cookieValue);
    }

    public getCookie(cookieName: string): string {
        return this.cookieStore.get(cookieName);
    }
}