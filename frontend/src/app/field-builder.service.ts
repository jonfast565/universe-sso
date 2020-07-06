import {
    Injectable
} from '@angular/core';
import {
    FormGroup,
    Validators,
    FormControl
} from '@angular/forms';
import {
    FieldFormControl,
    FieldModel
} from './models/field';
import { LoginApiService } from './services/login-api.service';

@Injectable({
    providedIn: 'root'
})
export class FieldBuilderService {

    constructor(private loginApi: LoginApiService) {}

    public loadProvider(that: any, pageType: string) {
        that.isLoading = true;
        this.loginApi.getProvider(that.providerName)
            .subscribe({
                next: (provider: any) => {
                    that.provider = provider;
                    this.loadFields(that, pageType);
                },
                error: (error: any) => {
                    console.log(error)
                    that.isLoading = false;
                }
            });
    }

    public loadFields(that: any, pageType: string) {
        this.loginApi.getFields(that.provider.name, pageType).subscribe(fields => {
            that.fields = fields;
            this.initializeFields(that);
            that.isLoading = false;
        });
    }

    public initializeFields(that: any) {
        let fieldMap = {};
        that.fields.forEach((field: FieldModel) => {
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
            that.fieldFormControls.push(fieldFormControl);
        });

        that.form = new FormGroup(fieldMap);
    }

    public disableFields(that: any, form: FormGroup, fields: {}) {
        that.fields.forEach((field: {
            fieldName: string | number;
        }) => {
            form.controls[field.fieldName].disable();
            fields[field.fieldName] = form.controls[field.fieldName].value;
        });
    }

    public enableFields(that: any, form: FormGroup) {
        that.fields.forEach((field: {
            fieldName: string | number;
        }) => {
            form.controls[field.fieldName].enable();
        });
    }
}