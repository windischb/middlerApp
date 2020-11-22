import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class AdminUIConfigService {

    private readonly CONFIGURATION_URL = "/api/admin-ui/config";
    private _configuration: AdminUIConfig;
    constructor(private _http: HttpClient) {

    }

    public loadConfiguration() {
        return this._http
            .get(this.CONFIGURATION_URL)
            .toPromise()
            .then((configuration: AdminUIConfig) => {
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

export interface AdminUIConfig {
    IDPBaseUri: string;
}
