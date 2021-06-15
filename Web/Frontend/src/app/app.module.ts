import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { APP_INITIALIZER, ErrorHandler, NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { RouterModule } from "@angular/router";
import { AppComponent } from "./app.component";
import { AppErrorHandler } from "./app.error.handler";
import { AppHttpInterceptor } from "./app.http.interceptor";
import { ROUTES } from "./app.routes";
import { AppLayoutsModule } from "./layouts/layouts.module";
import { AppSettingsService } from "./settings/settings.service";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {UpdateProductFormComponent} from "./pages/main/update-product-form/update-product-form.component";
import {MatButtonModule} from "@angular/material/button";
import {MatDialogModule} from "@angular/material/dialog";
import {MatInputModule} from "@angular/material/input";
import {ReactiveFormsModule} from "@angular/forms";
import { HistoryComponent } from './pages/main/history/history.component';

@NgModule({
    bootstrap: [AppComponent],
    declarations: [AppComponent, UpdateProductFormComponent, HistoryComponent],
    imports: [
        BrowserModule,
        HttpClientModule,
        RouterModule.forRoot(ROUTES),
        AppLayoutsModule,
        BrowserAnimationsModule,
        MatButtonModule,
        MatDialogModule,
        MatInputModule,
        ReactiveFormsModule
    ],
    providers: [
        { provide: ErrorHandler, useClass: AppErrorHandler },
        { provide: APP_INITIALIZER, useFactory: (_: AppSettingsService) => () => { }, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: AppHttpInterceptor, multi: true }
    ]
})
export class AppModule { }
