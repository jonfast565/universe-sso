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

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {

    public provider: string = null;

    constructor(private route: ActivatedRoute,
        private router: Router,
        private providerApi: ProviderApiService) {
        this.handleSelectProviderRedirect(router);
    }

    handleSelectProviderRedirect(router: Router) {
        this.route.queryParams.subscribe(params => {
            let provider = params['provider'];
            if (!provider) {
                this.gotoProviderSelection();
            } else {
                this.provider = provider;
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
        this.providerApi.getProvider(this.provider).subscribe(provider => {

        });
    }

    public login() {

    }
}