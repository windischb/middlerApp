import { Component } from "@angular/core";
import { AppUIService } from '../shared/services';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../shared/services/authentication/authentication-service';
import { map } from 'rxjs/operators';

@Component({
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})
export class HomeComponent {

    uiContext$ = this.uiService.UIContext$;
    sideBarCollapsed$ = this.uiService.sideBarCollapsed$;

    currentUser$ = this.auth.currentUser$;

    userName$ = this.currentUser$.pipe(
        map(user => {
            if (!user) {
                return null;
            }
            var name = user.userName;
            if(user.firstName?.trim() && user.lastName?.trim()) {
                name = `${user.firstName?.trim()} ${user.lastName?.trim()}`
            }
            

            return name;
        })
    );

    constructor(private uiService: AppUIService, private router: Router, private auth: AuthenticationService) {

        uiService.SetDefault(ui => {
            ui.Content.Scrollable = false;
            ui.Content.Container = true;
            ui.Header.Icon = null
            ui.Footer.Show = false;
        })

        
               
        
    }
    ngAfterViewInit(): void {
        
        // if(location.pathname == "/first-setup") {
        //     setTimeout(() => {
        //         this.uiService.Set(ui => {
        //             ui.Sidebar.Hide = true;
        //             ui.Content.ShowAlways = true;
        //             this.cref.detectChanges();
        //         })
        //     }, 1000);
            
        // }
    }
    
    identity = false;

    toggleSideBar() {
        this.uiService.toggleSideBar();
    }

    Login() {
        this.auth.LogIn();
    }

    Logout() {
        this.auth.LogOut();
    }
    
}