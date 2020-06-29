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

@Injectable({
    providedIn: 'root'
})
export class ProviderApiService {
    private providerEndpoint = "api/login/providers"
    constructor(@Inject(HttpClient) private http: HttpClient) {}

    getProviders(): Observable < ProviderViewModel[] > {
        const params = new HttpParams();
        const response = this.http.get < ProviderViewModel[] > (this.providerEndpoint, {
            params
        });
        return response;
    }
}