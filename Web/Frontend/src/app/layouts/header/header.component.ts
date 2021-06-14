import { Component, OnInit } from "@angular/core";
import { AppAuthService } from "../../services/auth.service";

@Component({
    selector: "app-header",
    templateUrl: "./header.component.html"
})
export class AppHeaderComponent implements OnInit {

    isLoggedIn = false;
    username = 'Profile';

    constructor(private auth: AppAuthService) { }

    ngOnInit() {
        this.isLoggedIn = !!this.auth.token();

        if (this.isLoggedIn) {
            const user = this.auth.getUser();
            this.username = user.name;
        }
    }
}
