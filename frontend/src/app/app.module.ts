import {
    BrowserModule
} from '@angular/platform-browser';
import {
    NgModule
} from '@angular/core';
import {
    FormsModule, ReactiveFormsModule
} from '@angular/forms';
import {
    HttpClientModule
} from '@angular/common/http';
import {
    BrowserAnimationsModule
} from '@angular/platform-browser/animations';
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
    SetRecoveryOptionsComponent
} from './set-recovery-options/set-recovery-options.component';
import {
    PageNotFoundComponent
} from './page-not-found/page-not-found.component';
import {
    ResetCredentialComponent
} from './reset-credential/reset-credential.component';
import {
    TwoFactorComponent
} from './two-factor/two-factor.component';
import {
    SafeHtmlPipe
} from './pipes/safe-html';
import {
    LoaderComponent
} from './loader/loader.component';
import { ErrorPageComponent } from './error-page/error-page.component';
import { AuthenticationJumpComponent } from './authentication-jump/authentication-jump.component';
import { AccountLockedComponent } from './account-locked/account-locked.component';

@NgModule({
    declarations: [
        AppComponent,
        SelectProviderComponent,
        LoginComponent,
        SetRecoveryOptionsComponent,
        PageNotFoundComponent,
        ResetCredentialComponent,
        TwoFactorComponent,
        SafeHtmlPipe,
        LoaderComponent,
        ErrorPageComponent,
        AuthenticationJumpComponent,
        AccountLockedComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        BrowserAnimationsModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule {}