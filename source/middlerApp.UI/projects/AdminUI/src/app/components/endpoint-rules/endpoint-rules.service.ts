import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { EndpointRule } from './models/endpoint-rule';
import { CreateMiddlerRuleDto } from './models/create-endpoint-rule';
import { of, BehaviorSubject } from 'rxjs';
import { tap, shareReplay, take, map } from 'rxjs/operators';
import { MessageService } from '@services';
import { compare } from 'fast-json-patch';
import { DoobEditorFile } from '@doob-ng/editor';
import { EndpointRuleListDto } from './models/endpoint-rule-list-dto';
import { EndpointAction } from './models/endpoint-action';
import { EndpointActionsListComponent } from './endpoint-actions-list/endpoint-actions-list.component';
import { DataEvent } from '../../shared/models/data-event';
import { EndpointRulesQuery, EndpointRulesStore } from './endpoint-rules-store';
import { OAuthService } from 'angular-oauth2-oidc';
//import { DoobEditorFile } from '@doob-ng/editor';


@Injectable({
    providedIn: 'root'
})
export class EndpointRulesService {


    // private _middlerRules: Array<EndpointRuleListDto>;
    // set MiddlerRules(value: Array<EndpointRuleListDto>) {
    //     this._middlerRules = (value || []).sort(this.SortRules);
    //     this.MiddlerRulesSubject$.next(this._middlerRules);
    // }
    // get MiddlerRules() {
    //     return this._middlerRules
    // }

    // private MiddlerRulesSubject$ = new BehaviorSubject<Array<EndpointRuleListDto>>(null);
    // public MiddlerRules$ = this.MiddlerRulesSubject$.asObservable().pipe(shareReplay());

    private RulesOrder: {
        [key: string]: number
    } = {}

    constructor(
        private http: HttpClient,
        private message: MessageService,
        private endpointRulesStore: EndpointRulesStore,
        private endpointRulesQuery: EndpointRulesQuery,
        private auth: OAuthService

    ) {

        this.Subscribe();

        this.message.RunOnEveryReconnect(() => this.GetAllRules().subscribe());
        this.message.RunOnEveryReconnect(() => this.GetTypings());
        this.message.RunOnEveryReconnect(() => this.GetImports());

        // this.message.Stream<any>("MiddlerRule.Subscribe").pipe(
        //     tap(item => console.log(item))
        // ).subscribe()
    }

    getHeaders(includePatchHeaders: boolean = false) {
        if (includePatchHeaders) {
            return new HttpHeaders({
                'Authorization': 'Bearer ' + this.auth.getAccessToken(),
                'Content-Type': 'application/json-patch+json',
                'Accept': 'application/json'
            });
        } else {
            return new HttpHeaders({
                'Authorization': 'Bearer ' + this.auth.getAccessToken()
            });
        }

    }

    Subscribe() {

        this.message.Stream<DataEvent<any>>("MiddlerRule.Subscribe").pipe(
            tap(item => {
                switch (item.Action) {
                    case "Created": {
                        this.endpointRulesStore.add(item.Payload);
                        break;
                    }
                    case "Updated": {
                        if (item.MetaData.UpdateOrder) {
                            var orders = <{ [id: string]: number }>item.Payload
                            Object.keys(orders).forEach(id => {
                                this.endpointRulesStore.update(id, {
                                    Order: orders[id]
                                });
                            })
                        } else {
                            this.endpointRulesStore.update(item.Payload.Id, entity => item.Payload);
                        }

                        break;
                    }
                    case "Deleted": {
                        this.endpointRulesStore.update(<any>item.Payload, entity => ({
                            ...entity,
                            Deleted: true
                        }));
                        this.endpointRulesStore.remove(<any>item.Payload)
                    }
                }

            })
        ).subscribe()
    }

    ReLoadRules() {
        this.http.get<Array<EndpointRuleListDto>>(`/api/endpoint-rules`, { headers: this.getHeaders() }).pipe(
            tap(rules => this.endpointRulesStore.set(rules))
        ).subscribe();
    }
    GetAllRules(force?: boolean) {
        if (force || !this.endpointRulesQuery.getHasCache()) {
            this.ReLoadRules()
        }

        return this.endpointRulesQuery.selectAll().pipe(
            map(rules => rules.sort(this.SortRules))
        );
    }


    public GetRule(id: string) {
        return this.http.get<EndpointRule>(`/api/endpoint-rules/${id}`, { headers: this.getHeaders() });
    }

    // public GetActionsForRule(ruleId: string) {
    //     return this.http.get<Array<EndpointAction>>(`/api/endpoint-rules/${ruleId}/actions`);
    // }

    public Add(rule: CreateMiddlerRuleDto) {
        return this.http.post(`/api/endpoint-rules`, rule, { headers: this.getHeaders() });
    }

    // public AddAction(ruleId: string, action: EndpointAction) {
    //     return this.http.post(`/api/endpoint-rules/${ruleId}/actions`, action);
    // }

    public Remove(id: string) {
        return this.http.delete(`/api/endpoint-rules/${id}`, { headers: this.getHeaders() });
    }

    public Update(id: string, rule: EndpointRule) {
        return this.http.put<EndpointRule>(`/api/endpoint-rules/${id}`, rule, { headers: this.getHeaders() });
    }

    public UpdateAction(action: EndpointAction) {
        return this.http.put<EndpointAction>(`/api/endpoint-action/${action.Id}`, action, { headers: this.getHeaders() });
    }

    // public UpdatePartial(id: string, patchDocument: any) {

    //     const patchHeaders = new HttpHeaders({
    //         'Content-Type': 'application/json-patch+json',
    //         'Accept': 'application/json'
    //     })

    //     return this.http.patch<EndpointRule>(`/api/endpoint-rules/${id}`, patchDocument, { headers: patchHeaders })
    //         .pipe(tap((rule: any) => this.UpdatedMiddlerRule(rule)));

    // }

    // public PatchAction(ruleId: string, actionId: string, patchDocument: any) {

    //     const patchHeaders = new HttpHeaders({
    //         'Content-Type': 'application/json-patch+json',
    //         'Accept': 'application/json'
    //     })

    //     return this.http.patch<EndpointRule>(`/api/endpoint-rules/${ruleId}/actions/${actionId}`, patchDocument, { headers: patchHeaders });;

    // }

    // private AddedMiddlerRule(value: EndpointRuleListDto) {
    //     this.MiddlerRules = [...this.MiddlerRules, value];
    // }

    // private UpdatedMiddlerRule(value: EndpointRuleListDto) {

    //     this.MiddlerRules = this.MiddlerRules.map(rule => {
    //         if (rule.Id == value.Id) {
    //             return value;
    //         } else {
    //             return rule;
    //         }
    //     });
    // }

    // private DeletedRule(value: EndpointRule | string) {
    //     let id: string;
    //     if (value instanceof EndpointRule) {
    //         id = value.Id;
    //     } else {
    //         id = value;
    //     }

    //     this.MiddlerRules = this.MiddlerRules.filter(rule => rule.Id == id);
    // }

    public SortRules(a: EndpointRuleListDto, b: EndpointRuleListDto) {
        if (a.Order > b.Order) {
            return 1;
        } else if (a.Order < b.Order) {
            return -1;
        }
        return 0;
    }

    public GetNextLastOrder() {
        const rules = this.endpointRulesQuery.getAll();
        if (!rules || rules.length === 0) {
            return 10;
        }
        return Math.trunc(Math.max(...rules.map(r => r.Order)) + 10);
    }

    // public UpdateRulesOrder() {

    //     let rules: {
    //         [key: string]: number
    //     } = {}
    //     this.endpointRulesQuery.getAll().forEach(r => rules[r.Id] = r.Order);

    //     return this.http.post<EndpointRule>(`/api/endpoint-rules/order`, rules, { headers: patchHeaders });
    // }

    public UpdateRuleOrder(rule: EndpointRuleListDto, newOrder: number) {
        var rules = {};
        rules[rule.Id] = newOrder;
        this.http.post<EndpointRule>(`/api/endpoint-rules/order`, rules, { headers: this.getHeaders(true) }).subscribe()
    }

    // public UpdateActionsOrder(ruleId: string, actions: Array<EndpointAction>) {

    //     let orders: {
    //         [key: string]: number
    //     } = {}
    //     actions.forEach(act => orders[act.Id] = act.Order);

    //     return this.http.post<EndpointRule>(`/api/endpoint-rules/${ruleId}/actions/order`, orders, { headers: patchHeaders });
    // }

    public SetRuleEnabled(rule: EndpointRuleListDto, value: boolean) {

        this.http.put<EndpointRule>(`/api/endpoint-rules/${rule.Id}/enabled/${value}`, null, { headers: this.getHeaders(true) }).subscribe();

    }


    // public SetActionEnabled(ruleId: string, action: EndpointAction, value: boolean) {

    //     var orig = JSON.parse(JSON.stringify(action))
    //     action.Enabled = value;
    //     var patchDocument = compare(orig, action)

    //     if (patchDocument.length > 0) {
    //         return this.PatchAction(ruleId, action.Id, patchDocument);
    //     }
    // }

    // public RemoveActions(ruleId: string, ...actionIds: Array<string>) {

    //     const options = {
    //         body: actionIds
    //     }

    //     return this.http.request("delete", `/api/endpoint-rules/${ruleId}/actions`, options);
    // }


    typings$: BehaviorSubject<DoobEditorFile[]> = new BehaviorSubject<DoobEditorFile[]>([]);

    public GetTypings() {
        this.http.get<Array<{ Key: string, Value: string }>>("api/endpoint-rules/type-definitions", { headers: this.getHeaders() })
            .pipe(
                take(1),
                tap(typings => {
                    var ts = typings.map(t => new DoobEditorFile(t.Key, t.Value, false))
                    this.typings$.next(ts);
                })
            ).subscribe();
    }

    imports$: BehaviorSubject<DoobEditorFile[]> = new BehaviorSubject<DoobEditorFile[]>([]);

    public GetImports() {
        this.http.get<Array<{ Key: string, Value: string }>>("api/endpoint-rules/import-definitions", { headers: this.getHeaders() })
            .pipe(
                take(1),
                tap(typings => {
                    var ts = typings.map(t => new DoobEditorFile(t.Key, t.Value, false))
                    this.imports$.next(ts);
                })
            ).subscribe();
    }

}

