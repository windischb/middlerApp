import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class IdentityUIConfigService {

    private readonly CONFIGURATION_URL = "/api/identity-ui/config";
    private _configuration: IdentityUIConfig;
    constructor(private _http: HttpClient) {

    }

    public loadConfiguration() {
        return this._http
            .get(this.CONFIGURATION_URL)
            .toPromise()
            .then((configuration: IdentityUIConfig) => {
                this._configuration = configuration;
                console.log(configuration);
                return configuration;
            })
            .catch((error: any) => {
                console.error(error);
            });
    }

    getConfiguration() {
        return this._configuration;
    }

}

export interface IdentityUIConfig {
    IDPBaseUri: string;
}
