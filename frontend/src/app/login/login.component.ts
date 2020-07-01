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
    FieldModel, FieldFormControl
} from '../models/field';
import {
    NgForm,
    FormGroup,
    FormControl,
    Validators
} from '@angular/forms';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {

    public providerName: string = null;
    public provider: ProviderViewModel = null;
    public fields: FieldModel[] = [];
    public fieldFormControls: FieldFormControl[] = [];
    public isLoading: boolean = false;
    public isLoggingIn: boolean = false;
    public form: FormGroup = null;
    public rememberMe: boolean = false;

    constructor(private route: ActivatedRoute,
        private router: Router,
        private loginApi: LoginApiService) {
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
        this.loadProvider();
    }

    private loadProvider() {
        this.isLoading = true;
        this.loginApi.getProvider(this.providerName)
            .subscribe({
                next: provider => {
                    this.provider = provider;
                    this.loadFields();
                },
                error: error => {
                    console.log(error)
                    this.isLoading = false;
                }
            });
    }

    private loadFields() {
        this.loginApi.getFields(this.provider.name, 'Login').subscribe(fields => {
            this.fields = fields;
            let fieldMap = {};
            this.fields.forEach(field => {
                let validators = [];

                if (field.required) {
                    validators.push(Validators.required);
                }

                if (field.pattern) {
                    validators.push(Validators.pattern(field.pattern));
                }

                let fieldFormControl = new FieldFormControl();
                fieldFormControl.field = field;
                fieldFormControl.control = new FormControl('', validators);
                fieldMap[field.fieldName] = fieldFormControl.control;
                this.fieldFormControls.push(fieldFormControl);
            });

            this.form = new FormGroup(fieldMap);
            this.isLoading = false;
        });
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
        this.disableFields(this.form, fields);
        // set remember me
        fields['RememberMe'] = this.rememberMe;

        this.loginApi.login(this.providerName, fields).subscribe({
            next: result => {
                // TODO: What to do?
                this.enableFields(this.form);
                this.isLoggingIn = false;

                if (result.flags.accountLocked) {
                    console.log('account locked');
                }

                if (result.flags.requiresPasswordReset) {
                    console.log('requires password reset');
                }

                if (result.flags.requiresRecoveryOptionsSet) {
                    console.log('requires recovery options set');
                }

                if (result.flags.requiresTwoFactorAuthentication) {
                    console.log('requires two factor authentication');
                }
            },
            error: error => {
                // re-enable fields
                this.enableFields(this.form);
                this.isLoggingIn = false;

                // TODO: Do not log error
                console.log(error);
            }
        });
    }

    private disableFields(form: FormGroup, fields: {}) {
        this.fields.forEach(field => {
            form.controls[field.fieldName].disable();
            fields[field.fieldName] = form.controls[field.fieldName].value;
        });
    }

    private enableFields(form: FormGroup) {
        this.fields.forEach(field => {
            form.controls[field.fieldName].enable();
        });
    }
}