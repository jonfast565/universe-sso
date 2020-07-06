import {
    Component,
    OnInit
} from '@angular/core';
import {
    LoginApiService
} from '../services/login-api.service';
import {
    Router,
    ActivatedRoute,
    Event
} from '@angular/router';
import {
    ProviderViewModel
} from '../models/provider';
import {
    FieldModel,
    FieldFormControl
} from '../models/field';
import {
    NgForm,
    FormGroup,
    FormControl,
    Validators
} from '@angular/forms';
import {
    NavigationStateMachineService
} from '../services/navigation-state-machine.service';
import {
    AuthServiceService
} from '../auth-service.service';
import { FieldBuilderService } from '../field-builder.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {

    // form fields, required
    public providerName: string = null;
    public provider: ProviderViewModel = null;
    public fields: FieldModel[] = [];
    public fieldFormControls: FieldFormControl[] = [];
    public isLoading: boolean = false;
    public form: FormGroup = null;

    public isLoggingIn: boolean = false;
    public rememberMe: boolean = false;

    constructor(private route: ActivatedRoute,
        private router: Router,
        private loginApi: LoginApiService,
        private navigation: NavigationStateMachineService,
        private authService: AuthServiceService,
        private fieldBuildService: FieldBuilderService) {
        this.handleSelectProviderRedirect(router);
    }

    private handleSelectProviderRedirect(router: Router) {
        this.route.queryParams.subscribe(params => {
            let providerName = params['provider'];
            if (!providerName) {
                this.gotoProviderSelection();
            } else {
                this.providerName = providerName;
            }
        });
    }

    private gotoProviderSelection() {
        this.router.navigate(['selectProvider'], {
            queryParamsHandling: 'merge'
        });
    }

    ngOnInit(): void {
        this.fieldBuildService.loadProvider(this, 'Login');
    }

    onChangeRememberMe(e: {
        target: {
            checked: boolean;
        };
    }) {
        this.rememberMe = e.target.checked;
    }

    public login() {
        // fix loading issues
        this.isLoggingIn = true;
        var fields = {};

        // disable fields and push fields
        this.fieldBuildService.disableFields(this, this.form, fields);
        // set remember me
        fields['RememberMe'] = this.rememberMe;

        this.loginApi.login(this.providerName, fields).subscribe({
            next: result => {
                // TODO: What to do?
                this.fieldBuildService.enableFields(this, this.form);
                this.isLoggingIn = false;

                // set auth flags cookie
                this.authService.setAuthenticationFlags(result.flags);
                this.navigation.navigateNext();
            },
            error: error => {
                // re-enable fields
                this.fieldBuildService.enableFields(this, this.form);
                this.isLoggingIn = false;
                this.navigation.navigateToErrorPage(error);
            }
        });
    }
}