import {
    Component,
    OnInit
} from '@angular/core';
import {
    ProviderViewModel
} from '../models/provider';
import {
    FieldModel,
    FieldFormControl
} from '../models/field';
import {
    FormGroup
} from '@angular/forms';
import {
    FieldBuilderService
} from '../field-builder.service';

@Component({
    selector: 'app-set-recovery-options',
    templateUrl: './set-recovery-options.component.html',
    styleUrls: ['./set-recovery-options.component.css']
})
export class SetRecoveryOptionsComponent implements OnInit {

    public isSubmitting: boolean = false;
    // form fields, required
    public providerName: string = null;
    public provider: ProviderViewModel = null;
    public fields: FieldModel[] = [];
    public fieldFormControls: FieldFormControl[] = [];
    public isLoading: boolean = false;
    public form: FormGroup = null;

    constructor(private fieldBuilderService: FieldBuilderService) {}

    ngOnInit(): void {
        this.fieldBuilderService.loadProvider(this, 'Recovery');
    }

    setRecoveryOptions() {

    }
}