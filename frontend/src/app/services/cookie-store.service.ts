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
        // TODO: Update domain and path
        let dateTime = new Date();
        dateTime.setDate(dateTime.getDate() + 1);
        this.cookieStore.set(cookieName, cookieValue, dateTime, "", "", false, "Strict");
    }

    public getCookie(cookieName: string): string {
        return this.cookieStore.get(cookieName);
    }
}