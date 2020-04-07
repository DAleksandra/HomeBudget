import {Injectable} from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { EditOutgoingComponent } from '../outgoings/edit-outgoing/edit-outgoing.component';

@Injectable()
export class PreventUnsavedChanges implements CanDeactivate<EditOutgoingComponent> {

    canDeactivate(component: EditOutgoingComponent) {
        if (component.editForm.dirty) {
            return confirm('Are you sure you want to continue? Any unsaved changes will be lost')
        }
        return true;
    }
}