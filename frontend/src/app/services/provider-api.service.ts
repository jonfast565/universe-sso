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
    ProviderViewModel
} from "../models/provider";

import {
    ProviderViewModelSlim
} from "../models/providerviewmodelslim";
import { FieldModel } from '../models/field';

@Injectable({
    providedIn: 'root'
})
export class ProviderApiService {
    private providersEndpoint = "api/login/providers";
    private providerEndpoint = "api/login/provider";
    private fieldsEndpoint = "api/login/fields";

    constructor(@Inject(HttpClient) private http: HttpClient) {}

    getProviders(): Observable < ProviderViewModelSlim[] > {
        const params = new HttpParams();
        const response = this.http.get < ProviderViewModelSlim[] > (this.providersEndpoint, {
            params
        });
        return response;
    }

    getProvider(providerName: string): Observable < ProviderViewModel > {
        var params = new HttpParams();
        params = params.append('providerName', providerName);
        const response = this.http.get < ProviderViewModel > (this.providerEndpoint, {
            params
        });
        return response;
    }

    getFields(providerName: string, pageType: string): Observable < FieldModel[] > {
        var params = new HttpParams();
        params = params.append('providerName', providerName);
        params = params.append('pageType', pageType);
        const response = this.http.get < FieldModel[] > (this.fieldsEndpoint, {
            params
        });
        return response;
    }
}