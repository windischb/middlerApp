import { EndpointAction } from '../../models/endpoint-action';
import { ScriptModalComponent } from './script-modal.component';


export class ScriptParameters {
    Language: "Javascript" | "Typescript" | "Powershell" = "Typescript";
    SourceCode: string;
}

export class ScriptAction extends EndpointAction<ScriptParameters> {

    ActionType = "Script"
    Parameters = new ScriptParameters()

    Terminating = true
}
