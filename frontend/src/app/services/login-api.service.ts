import {
    Injectable,
    Inject
} from '@angular/core';
import {
    HttpClient,
    HttpParams
} from "@angular/common/http";

import {
    Observable
} from 'rxjs';
import {
    ProviderViewModel,
    ProviderViewModelSlim
} from "../models/provider";

import {
    FieldModel
} from '../models/field';
import {
    AuthenticationReasons
} from '../models/authentication';

@Injectable({
    providedIn: 'root'
})
export class LoginApiService {
    private providersEndpoint = "api/login/providers";
    private providerEndpoint = "api/login/provider";
    private fieldsEndpoint = "api/login/fields";
    private loginEndpoint = "api/login/login";

    constructor(@Inject(HttpClient) private http: HttpClient) {}

    public getProviders(): Observable < ProviderViewModelSlim[] > {
        const params = new HttpParams();
        const response = this.http.get < ProviderViewModelSlim[] > (this.providersEndpoint, {
            params
        });
        return response;
    }

    public getProvider(providerName: string): Observable < ProviderViewModel > {
        var params = new HttpParams();
        params = params.append('providerName', providerName);
        const response = this.http.get < ProviderViewModel > (this.providerEndpoint, {
            params
        });
        return response;
    }

    public getFields(providerName: string, pageType: string): Observable < FieldModel[] > {
        var params = new HttpParams();
        params = params.append('providerName', providerName);
        params = params.append('pageType', pageType);
        const response = this.http.get < FieldModel[] > (this.fieldsEndpoint, {
            params
        });
        return response;
    }

    public login(providerName: string, fields: object): Observable < AuthenticationReasons > {
        var params = new HttpParams();
        params = params.append('providerName', providerName);
        const response = this.http.post < AuthenticationReasons > (this.loginEndpoint, fields, { params });
        return response;
    }
}