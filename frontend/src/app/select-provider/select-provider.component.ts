import {
    Component,
    OnInit
} from '@angular/core';
import {
    Router,
    ActivatedRoute,
    ParamMap,
    NavigationExtras
} from '@angular/router';
import {
    Observable
} from 'rxjs';

const navigationExtras : NavigationExtras = {
    queryParamsHandling: 'preserve',
    preserveFragment: true
};

@Component({
    selector: 'app-select-provider',
    templateUrl: './select-provider.component.html',
    styleUrls: ['./select-provider.component.css']
})
export class SelectProviderComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private router: Router) {
        this.handleProviderRedirect(router);
    }

    private handleProviderRedirect(router: Router) {
        this.route.queryParamMap.subscribe(params => {
            let unwrappedParams = { ...params };
            let provider = unwrappedParams['provider'];
            if (provider) {
                this.gotoLogin();
            }
        });
    }

    ngOnInit(): void {}

    public gotoLogin() {
        this.router.navigate(['login'], navigationExtras);
    }
}