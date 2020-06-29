import {
    Component,
    OnInit
} from '@angular/core';
import {
    Router,
    ActivatedRoute,
    NavigationExtras
} from '@angular/router';
import {
    Observable
} from 'rxjs';
import { ProviderApiService } from '../services/provider-api.service';
import { ProviderViewModel } from '../models/provider';

const navigationExtras: NavigationExtras = {
    queryParamsHandling: 'preserve',
    preserveFragment: true
};

@Component({
    selector: 'app-select-provider',
    templateUrl: './select-provider.component.html',
    styleUrls: ['./select-provider.component.css'],
})
export class SelectProviderComponent implements OnInit {
    public providers: ProviderViewModel[] = [];
    public isLoading: boolean = false;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private providerApi: ProviderApiService) {
        this.handleProviderRedirect(router);
    }

    private handleProviderRedirect(router: Router) {
        this.route.queryParams.subscribe(params => {
            let provider = params['provider'];
            if (provider) {
                this.gotoLogin();
            }
        });
    }

    loadProviders(): void {
        this.isLoading = true;
        this.providerApi.getProviders().subscribe({
            next: providers => { 
                this.isLoading = false;
                this.providers = providers; 
            },
            error: error => { 
                this.isLoading = false;
                console.log(error); 
            }
        });
    }

    ngOnInit(): void {
        this.loadProviders();
    }

    public gotoLogin() {
        this.router.navigate(['login?provider=' + this.getSelectedProvider().name], navigationExtras);
    }


    public selectProvider(provider: ProviderViewModel) {
        for (var i = 0; i < this.providers.length; i++) {
            this.providers[i].selected = false;
        }

        provider.selected = true;
    }

    public providerSelected() {
        for (var i = 0; i < this.providers.length; i++) {
            if (this.providers[i].selected === true) {
                return true;
            }
        }
        return false;
    }

    public getSelectedProvider() {
        for (var i = 0; i < this.providers.length; i++) {
            if (this.providers[i].selected === true) {
                return this.providers[i];
            }
        }
    }
}