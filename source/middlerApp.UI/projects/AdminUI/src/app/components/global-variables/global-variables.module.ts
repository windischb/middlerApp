import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoutingComponents, GlobalVariablesRoutingModule } from './global-variables-routing.module';
import { ReactiveFormsModule } from '@angular/forms';

import { AngularSplitModule } from 'angular-split';
import { DoobCdkHelperModule } from '@doob-ng/cdk-helper';
import { GlobalVariablesExplorerComponent } from './explorer';
import { DoobCoreModule } from '@doob-ng/core';
import { DoobGridModule } from "@doob-ng/grid";
import { VariablesFolderContentComponent } from './folder-content';

import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { VariableModalsModule } from './modals/variable-modals.module';
import { GlobalImportsModule } from '../../global-imports.module';
import { TreeModule } from '@circlon/angular-tree-component';


@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        GlobalVariablesRoutingModule,
        TreeModule,
        AngularSplitModule,
        DoobCoreModule,
        DoobCdkHelperModule,
        DoobGridModule,
        FontAwesomeModule,
        VariableModalsModule,
        GlobalImportsModule
    ],
    declarations: [
        ...RoutingComponents,
        GlobalVariablesExplorerComponent,
        VariablesFolderContentComponent
    ],
    exports: [
        
    ]
})
export class GlobalVariablesModule { }
