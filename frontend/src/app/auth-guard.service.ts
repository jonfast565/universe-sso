import {
    Injectable
} from '@angular/core';
import {
    CanActivate,
    Router
} from '@angular/router';
import {
    AuthServiceService
} from './auth-service.service';

@Injectable({
    providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

    constructor(public authService: AuthServiceService, public router: Router) {}

    canActivate(): boolean {
        if (!this.authService.isAuthenticated()) {
            this.router.navigate(['pageNotFound']);
            return false;
        }
        return true;
    }
}