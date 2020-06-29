import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { SelectProviderComponent } from './select-provider/select-provider.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { RecoveryOptionsComponent } from './recovery-options/recovery-options.component';
import { TwoFactorComponent } from './two-factor/two-factor.component';
import { ResetCredentialComponent } from './reset-credential/reset-credential.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'selectProvider', component: SelectProviderComponent },
  { path: 'recovery', component: RecoveryOptionsComponent },
  { path: '2fa', component: TwoFactorComponent },
  { path: 'resetCredential', component: ResetCredentialComponent },
  { path: '', redirectTo: '/selectProvider', pathMatch: 'full' },
  // { path: '**', component: FallbackHomepageComponent },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
