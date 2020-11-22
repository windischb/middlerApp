import { Component, OnInit, ChangeDetectorRef } from "@angular/core";
import { AppUIService } from '@services';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { combineLatest, from, Observable, of } from 'rxjs';
import { map, mergeAll, tap } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { IDPService } from '../idp.service';
import { AuthenticationProviderDto } from '../models/authentication-provider-dto';


@Component({
    templateUrl: './authentication-provider-details.component.html',
    styleUrls: ['./authentication-provider-details.component.scss']
})
export class AuthenticationProviderDetailsComponent implements OnInit {


    form: FormGroup


    private Id: string;
    public provider$ = combineLatest(this.route.paramMap, this.route.queryParamMap, this.route.fragment).pipe(
        map(([paramMap, queryParamMap, fragment]) => {
            this.Id = paramMap.get('id')
            return this.idService.GetAuthenticationProvider(this.Id);
        }),
        mergeAll(),
        tap(dto => this.setProvider(dto))
    )

    showDebugInformations$ = this.uiService.showDebugInformations$;

    constructor(
        private uiService: AppUIService,
        private fb: FormBuilder,
        private route: ActivatedRoute,
        private idService: IDPService,
        private cref: ChangeDetectorRef
    ) {
        uiService.Set(ui => {
            ui.Header.Title = "Authentication Provider"
            ui.Content.Scrollable = false;
            ui.Header.Icon = "fa#shield-alt"

            ui.Footer.Show = true;
            ui.Footer.Button1.Text = "Save";
            ui.Footer.Button2.Text = "Reset";

            ui.Footer.Button1.OnClick = () => this.Save();

            ui.Footer.Button2.OnClick = () => {
                this.form.reset(this.BaseDto);
            }
        })
    }

    ngOnInit() {

        this.form = this.fb.group({
            Id: [null],
            Type: [null],
            Enabled: [false],
            Name: [null, Validators.required],
            DisplayName: [null, Validators.required],
            Description: [null],
            Parameters: []
        })

    }

 
    Save() {

        console.log(this);
        if (this.Id == 'create') {
            this.idService.CreateAuthenticationProvider(this.form.value).subscribe();
        } else {
            this.idService.UpdateAuthenticationProvider(this.form.value).subscribe();
        }
    }

    setProvider(dto: AuthenticationProviderDto) {
        if (!this.BaseDto) {
            this.BaseDto = dto;
        }

        this.uiService.Set(ui => {
            ui.Header.Title = "Authentication Provider";
            //ui.Header.SubTitle = user.UserName
            ui.Header.Icon = "form"

            ui.Footer.Button1.Visible = true;
            ui.Footer.Button1.Text = dto.Id ? "Save Changes" : "Create ApiScope"
            ui.Footer.Button2.Visible = true;
        })

        this.form.patchValue(dto)
    }

    Reload() {
        this.idService.GetAuthenticationProvider(this.Id).subscribe(apiresource => {
            this.setProvider(apiresource)
            this.idService.ReLoadAuthenticationProviders();
        });


    }

    private _baseDto: AuthenticationProviderDto;
    private set BaseDto(rule: AuthenticationProviderDto) {
        this._baseDto = JSON.parse(JSON.stringify(rule));

    }
    private get BaseDto() {
        return this._baseDto;
    }
}