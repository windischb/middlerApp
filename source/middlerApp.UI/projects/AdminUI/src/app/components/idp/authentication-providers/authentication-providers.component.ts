import { Component, ViewChild, TemplateRef, ViewContainerRef } from "@angular/core";
import { AppUIService } from '@services';
import { GridBuilder, DefaultContextMenuContext } from '@doob-ng/grid';
import { IOverlayHandle, DoobOverlayService } from '@doob-ng/cdk-helper';
import { IDPService } from '../idp.service';
import { Router, ActivatedRoute } from '@angular/router';
import { tap, takeUntil } from 'rxjs/operators';

import { AuthenticationProviderListDto } from '../models/authentication-provider-list-dto';


@Component({
    templateUrl: './authentication-providers.component.html',
    styleUrls: ['./authentication-providers.component.scss']
})
export class AuthenticationProvidersComponent {

    @ViewChild('itemsContextMenu') itemsContextMenu: TemplateRef<any>

    grid = new GridBuilder<AuthenticationProviderListDto>()
        .SetColumns(
            c => c.Default("Name")
                .SetInitialWidth(200, true)
                .SetLeftFixed()
                .SetCssClass("pValue"),
            c => c.Default("DisplayName"),
            c => c.Default("Description")
        )
        .SetData(this.idService.GetAllAuthenticationProvidersList())
        .WithRowSelection("multiple")
        .WithFullRowEditType()
        .WithShiftResizeMode()
        .OnCellContextMenu(ev => {
            const selected = ev.api.getSelectedNodes();
            if (selected.length == 0 || !selected.includes(ev.node)) {
                ev.node.setSelected(true, true)
            }

            let vContext = new DefaultContextMenuContext(ev.api, ev.event as MouseEvent)
            this.contextMenu = this.overlay.OpenContextMenu(ev.event as MouseEvent, this.itemsContextMenu, this.viewContainerRef, vContext)
        })
        .OnViewPortContextMenu((ev, api) => {
            let vContext = new DefaultContextMenuContext(api, ev)
            this.contextMenu = this.overlay.OpenContextMenu(ev, this.itemsContextMenu, this.viewContainerRef, vContext)
        })
        .OnRowDoubleClicked(el => {
            this.Edit(el.node.data);
            //console.log("double Clicked", el)

        })
        .StopEditingWhenGridLosesFocus()
        .OnGridSizeChange(ev => ev.api.sizeColumnsToFit())
        .OnViewPortClick((ev, api) => {
            api.deselectAll();
        })
        .SetRowClassRules({
            'deleted': 'data.Deleted'
        })
        .SetDataImmutable(data => data.Id);



    private contextMenu: IOverlayHandle;

    constructor(
        private uiService: AppUIService,
        private idService: IDPService,
        private router: Router,
        private route: ActivatedRoute,
        public overlay: DoobOverlayService,
        public viewContainerRef: ViewContainerRef
    ) {
        uiService.Set(ui => {
            ui.Header.Title = "IDP / Authentication Providers"
            ui.Content.Scrollable = false;
            ui.Header.Icon = "fa#shield-alt"
        })

        // idService.GetAllApiScopes().subscribe(api-resources => {
        //     this.grid.SetData(api-resources);
        // });
    }

    Add() {
        this.router.navigate(["create"], { relativeTo: this.route });
        this.contextMenu?.Close();
    }

    Edit(item: AuthenticationProviderListDto) {
        this.router.navigate([item.Id], { relativeTo: this.route });
        this.contextMenu?.Close();
    }

    Remove(item: Array<AuthenticationProviderListDto>) {
        this.idService.DeleteAuthenticationProvider(...item.map(r => r.Id)).subscribe();
        this.contextMenu?.Close();
    }

    Reload() {
        this.idService.ReLoadAuthenticationProviders();
    }
}