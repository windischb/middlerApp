import { Component, Input, Output, EventEmitter } from "@angular/core";
import { FormGroup } from '@angular/forms';

@Component({
    selector: 'action-basic-modal',
    templateUrl: './action-basic-modal.component.html',
    styleUrls: ['./action-basic-modal.component.scss', '../style.scss'],
    host: {
        class: 'flex-grow-column action-basic-modal'
    }
})
export class ActionBasicModalComponent {

    @Output() ok = new EventEmitter();
    @Output() cancel = new EventEmitter();

    @Input() form: FormGroup;

}
