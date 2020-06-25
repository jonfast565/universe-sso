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

@Component({
    selector: 'app-select-provider',
    templateUrl: './select-provider.component.html',
    styleUrls: ['./select-provider.component.css']
})
export class SelectProviderComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private router: Router) {
        let navigationExtras: NavigationExtras = {
            queryParamsHandling: 'preserve',
            preserveFragment: true
        };
        let provider = this.route.snapshot.paramMap.get("provider");
        if (provider) {
            router.navigate(['login'], navigationExtras);
        }
    }

    ngOnInit(): void {}

    gotoLogin() {
        this.router.navigate(['login']);
    }
}