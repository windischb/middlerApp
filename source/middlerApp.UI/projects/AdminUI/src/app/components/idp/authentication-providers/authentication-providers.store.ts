import { StoreConfig, EntityState, EntityStore, QueryEntity } from '@datorama/akita';
import { Injectable } from '@angular/core';
import { AuthenticationProviderListDto } from '../models/authentication-provider-list-dto';




export interface AuthenticationProvidersState extends EntityState<AuthenticationProviderListDto, string> { }

@Injectable({ providedIn: 'root' })
@StoreConfig({ name: 'authentication-providers', idKey: 'Id' })
export class AuthenticationProvidersStore extends EntityStore<AuthenticationProvidersState> {
    constructor() {
        super();
    }
}

@Injectable({
    providedIn: 'root'
})
export class AuthenticationProvidersQuery extends QueryEntity<AuthenticationProvidersState> {
    constructor(protected store: AuthenticationProvidersStore) {
        super(store);
    }
}