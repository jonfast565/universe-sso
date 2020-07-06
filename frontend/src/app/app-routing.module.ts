import {
    NgModule
} from '@angular/core';
import {
    Routes,
    RouterModule
} from '@angular/router';

import {
    LoginComponent
} from './login/login.component';
import {
    SelectProviderComponent
} from './select-provider/select-provider.component';
import {
    PageNotFoundComponent
} from './page-not-found/page-not-found.component';
import {
    SetRecoveryOptionsComponent
} from './set-recovery-options/set-recovery-options.component';
import {
    TwoFactorComponent
} from './two-factor/two-factor.component';
import {
    ResetCredentialComponent
} from './reset-credential/reset-credential.component';
import {
    ErrorPageComponent
} from './error-page/error-page.component';
import {
    AuthenticationJumpComponent
} from './authentication-jump/authentication-jump.component';
import {
    AccountLockedComponent
} from './account-locked/account-locked.component';
import {
    AuthGuardService
} from './auth-guard.service';

const routes: Routes = [{
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'accountLocked',
        component: AccountLockedComponent,
        canActivate: [AuthGuardService]
    },
    {
        path: 'selectProvider',
        component: SelectProviderComponent
    },
    {
        path: 'setRecoveryOptions',
        component: SetRecoveryOptionsComponent,
        canActivate: [AuthGuardService]
    },
    {
        path: '2fa',
        component: TwoFactorComponent,
        canActivate: [AuthGuardService]
    },
    {
        path: 'resetCredential',
        component: ResetCredentialComponent,
        canActivate: [AuthGuardService]
    },
    {
        path: 'error',
        component: ErrorPageComponent
    },
    {
        path: 'authenticationJump',
        component: AuthenticationJumpComponent,
        canActivate: [AuthGuardService]
    },
    {
        path: '',
        redirectTo: '/selectProvider',
        pathMatch: 'full'
    },
    {
        path: '**',
        component: PageNotFoundComponent
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, {
        useHash: true
    })],
    exports: [RouterModule]
})
export class AppRoutingModule {}