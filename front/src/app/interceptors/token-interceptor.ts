import { Inject, Injectable } from "@angular/core";
import { AccountService } from "../services/account.service";
import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs";

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
    constructor() {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const token = window.localStorage.getItem('token');
        let request : HttpRequest<any> = req;

        if (token) {
            request = req.clone({
                headers: req.headers.set('Authorization', 'Bearer ' + token)
            });
        }

        return next.handle(request).pipe(catchError(this.handleError));
    }

    private handleError(error : HttpErrorResponse) {
        if (error.error instanceof ErrorEvent) {
            alert(error.error.message);
            return throwError(error.error.message);
        } else {
            alert(JSON.stringify(error.error));
            return throwError(JSON.stringify(error.error));
        }
    }
}