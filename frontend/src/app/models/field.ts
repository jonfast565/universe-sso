import { FormControl } from '@angular/forms'

class FieldModel {
    public fieldName: string
    public fieldType: string
    public optionalFieldValues: string
    public required: boolean
    public pattern: string
}

class FieldFormControl {
    public field: FieldModel
    public control: FormControl
}

export {
    FieldModel,
    FieldFormControl
}