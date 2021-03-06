import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';

import { AppRoutingModule, RoutingComponents } from './app-routing.module';
import { AppComponent } from './app.component';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzFormModule } from 'ng-zorro-antd/form';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { IconsProviderModule } from './icons-provider.module';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { GlobalImportsModule } from './global-imports.module';
import { FaIconLibrary, FaConfig } from '@fortawesome/angular-fontawesome';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { far } from '@fortawesome/free-regular-svg-icons';
import { OAuthModule, OAuthStorage } from 'angular-oauth2-oidc';
import { IdentityUIConfigService } from './identity-ui-config.service';


export function storageFactory() : OAuthStorage {
  return localStorage
}

const appInitializerFn = (configService: IdentityUIConfigService) => {
  return () => configService.loadConfiguration();
};

@NgModule({
  declarations: [
    AppComponent,
    ...RoutingComponents
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    AppRoutingModule,
    NzFormModule,
    NzInputModule,
    NzButtonModule,
    NzCheckboxModule,
    ReactiveFormsModule,
    IconsProviderModule,
    HttpClientModule,
    GlobalImportsModule,
    OAuthModule.forRoot()
  ],
  providers: [
    IdentityUIConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: appInitializerFn,
      multi: true,
      deps: [IdentityUIConfigService]
    },
    { provide: OAuthStorage, useFactory: storageFactory }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(private library: FaIconLibrary, faConfig: FaConfig) {
    library.addIconPacks(fas, far);
  }
 }
