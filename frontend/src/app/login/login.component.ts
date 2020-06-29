import {
    Component,
    OnInit
} from '@angular/core';
import {
    ProviderApiService
} from '../services/provider-api.service';
import {
    Router,
    ActivatedRoute
} from '@angular/router';
import {
    ProviderViewModel
} from '../models/provider';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {

    public providerName: string = null;
    public provider: ProviderViewModel = null;
    public isLoading: boolean = false;
    public isLoggingIn: boolean = false;

    constructor(private route: ActivatedRoute,
        private router: Router,
        private providerApi: ProviderApiService) {
        this.handleSelectProviderRedirect(router);
    }

    handleSelectProviderRedirect(router: Router) {
        this.route.queryParams.subscribe(params => {
            let providerName = params['provider'];
            if (!providerName) {
                this.gotoProviderSelection();
            } else {
                this.providerName = providerName;
            }
        });
    }
    gotoProviderSelection() {
        this.router.navigate(['selectProvider'], {
            queryParamsHandling: 'merge'
        });
    }

    ngOnInit(): void {
        this.loadProvider();
    }

    loadProvider() {
        this.isLoading = true;
        this.providerApi.getProvider(this.providerName)
            .subscribe(provider => {
                this.provider = provider;
                this.isLoading = false;
            });
    }

    public login() {
        this.isLoggingIn = true;
        setTimeout(() => this.isLoggingIn = false, 2000 , this)
    }
}