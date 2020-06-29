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

@Injectable({
    providedIn: 'root'
})
export class ProviderApiService {
    private providersEndpoint = "api/login/providers";
    private providerEndpoint = "api/login/provider";

    constructor(@Inject(HttpClient) private http: HttpClient) {}

    getProviders(): Observable < ProviderViewModelSlim[] > {
        const params = new HttpParams();
        const response = this.http.get < ProviderViewModelSlim[] > (this.providersEndpoint, {
            params
        });
        return response;
    }

    getProvider(providerName: string): Observable < ProviderViewModel > {
        const params = new HttpParams();
        params.set('providerName', providerName);
        const response = this.http.get < ProviderViewModel > (this.providerEndpoint, {
            params
        });
        return response;
    }
}