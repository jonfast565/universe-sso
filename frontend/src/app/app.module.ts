import {
    BrowserModule
} from '@angular/platform-browser';
import {
    NgModule
} from '@angular/core';
import {
    FormsModule
} from '@angular/forms';

import {
    AppRoutingModule
} from './app-routing.module';
import {
    AppComponent
} from './app.component';
import {
    SelectProviderComponent
} from './select-provider/select-provider.component';
import {
    LoginComponent
} from './login/login.component';
import {
    RecoveryOptionsComponent
} from './recovery-options/recovery-options.component';
import {
    PageNotFoundComponent
} from './page-not-found/page-not-found.component';
import {
    ResetCredentialComponent
} from './reset-credential/reset-credential.component';
import {
    TwoFactorComponent
} from './two-factor/two-factor.component';

@NgModule({
    declarations: [
        AppComponent,
        SelectProviderComponent,
        LoginComponent,
        RecoveryOptionsComponent,
        PageNotFoundComponent,
        ResetCredentialComponent,
        TwoFactorComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        FormsModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule {}