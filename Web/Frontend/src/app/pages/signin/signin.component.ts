import { Component, OnInit } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { AppAuthService } from "src/app/services/auth.service";

@Component({
    selector: "app-signin",
    templateUrl: "./signin.component.html"
})
export class AppSigninComponent implements OnInit {
    form = this.formBuilder.group({
        login: ["admin", Validators.required],
        password: ["admin", Validators.required]
    });

    constructor(
        private readonly formBuilder: FormBuilder,
        private router: Router,
        private readonly appAuthService: AppAuthService) {
    }

    ngOnInit() {

    }

    signin() {
        this.appAuthService.login(this.form.value).subscribe((result: any) => {
            console.log(result);
            if (!result || !result.token) { return; }
            localStorage.setItem("token", result.token);
            this.appAuthService.saveUser(result);
            this.router.navigate(["/main/home"]);
        });;
    }
}
