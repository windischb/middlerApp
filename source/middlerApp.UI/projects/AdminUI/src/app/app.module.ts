import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';

import { AppRoutingModule, RoutingComponents } from './app-routing.module';
import { AppComponent } from './app.component';

import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NZ_I18N } from 'ng-zorro-antd/i18n';
import { en_US } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import { TreeModule } from '@circlon/angular-tree-component';

import { FontAwesomeModule, FaIconLibrary, FaConfig } from '@fortawesome/angular-fontawesome';

import { AkitaNgDevtools } from '@datorama/akita-ngdevtools';
import { AkitaNgRouterStoreModule } from '@datorama/akita-ng-router-store';
import { environment } from '../environments/environment';
import { GlobalImportsModule } from './global-imports.module';
import { OverlayModule } from '@angular/cdk/overlay';
import { OAuthModule, OAuthStorage } from 'angular-oauth2-oidc';
import { FortAwesomeLib } from './fortawesome';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { AdminUIConfigService } from './admin-ui-config.service';



registerLocaleData(en);
export function storageFactory() : OAuthStorage {
  return localStorage
}

const appInitializerFn = (configService: AdminUIConfigService) => {
  return () => configService.loadConfiguration();
};


@NgModule({
  declarations: [
    AppComponent,
    ...RoutingComponents
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    AppRoutingModule,
    // IconsProviderModule,
    HttpClientModule,
    BrowserAnimationsModule,
    TreeModule,
    FontAwesomeModule,
    GlobalImportsModule,
    environment.production ? [] : AkitaNgDevtools.forRoot(),
    AkitaNgRouterStoreModule,
    OverlayModule,
    OAuthModule.forRoot(),
    NzIconModule.forRoot(FortAwesomeLib.AntdIcons)
  ],
  providers: [
    AdminUIConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: appInitializerFn,
      multi: true,
      deps: [AdminUIConfigService]
    },

    { provide: NZ_I18N, useValue: en_US },
    { provide: OAuthStorage, useFactory: storageFactory }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {

  constructor(private library: FaIconLibrary, faConfig: FaConfig) {
    
    var fLib = new FortAwesomeLib();
    fLib.Init(library);
    
  }
}
