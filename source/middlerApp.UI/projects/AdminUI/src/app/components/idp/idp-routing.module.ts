import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UsersComponent } from './users/users.component';
import { RolesComponent } from './roles/roles.component';
import { ClientsComponent } from './clients/clients.component';
import { UserDetailsComponent } from './users/user-details.component';
import { RoleDetailsComponent } from './roles/role-details.component';
import { ClientDetailsComponent } from './clients/client-details.component';
import { ApiResourcesComponent } from './api-resources/api-resources.component';
import { ApiResourceDetailsComponent } from './api-resources/api-resource-details.component';
import { IdentityResourcesComponent } from './identity-resources/identity-resources.component';
import { IdentityResourceDetailsComponent } from './identity-resources/identity-resource-details.component';
import { ApiScopesComponent } from './api-scopes/api-scopes.component';
import { ApiScopeDetailsComponent } from './api-scopes/api-scope-details.component';
import { AuthenticationProvidersComponent } from './authentication-providers/authentication-providers.component';
import { AuthenticationProviderDetailsComponent } from './authentication-providers/authentication-provider-details.component';


const routes: Routes = [
    { path: 'users', component: UsersComponent },
    { path: 'users/:id', component: UserDetailsComponent },
    { path: 'roles', component: RolesComponent },
    { path: 'roles/:id', component: RoleDetailsComponent },
    { path: 'clients', component: ClientsComponent },
    { path: 'clients/:id', component: ClientDetailsComponent },
    { path: 'api-resources', component: ApiResourcesComponent },
    { path: 'api-resources/:id', component: ApiResourceDetailsComponent },
    { path: 'identity-resources', component: IdentityResourcesComponent },
    { path: 'identity-resources/:id', component: IdentityResourceDetailsComponent },
    { path: 'api-scopes', component: ApiScopesComponent },
    { path: 'api-scopes/:id', component: ApiScopeDetailsComponent },
    { path: 'authentication-providers', component: AuthenticationProvidersComponent },
    { path: 'authentication-providers/:id', component: AuthenticationProviderDetailsComponent },
];

export const RoutingComponents = [
    UsersComponent,
    UserDetailsComponent,
    RolesComponent,
    RoleDetailsComponent,
    ClientsComponent,
    ClientDetailsComponent,
    ApiResourcesComponent,
    ApiResourceDetailsComponent,
    IdentityResourcesComponent,
    IdentityResourceDetailsComponent,
    ApiScopesComponent,
    ApiScopeDetailsComponent,
    AuthenticationProvidersComponent,
    AuthenticationProviderDetailsComponent
]

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class IDPRoutingModule { }
