import { Inject, Injectable } from "@angular/core";
import { TokenService } from "../services/token.service";
import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs";
import { AuthService } from "../services/auth.service";
import { handleError } from "../handleError/handleError";

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
    constructor(
        private tokenService : TokenService,
        private authService : AuthService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (!this.refreshing) {
            if (this.tokenService.isExpired() && this.tokenService.getToken() && this.tokenService.getRefresh()) {
                this.refreshing = true;
                
                this.authService.refresh().subscribe(x => {
                    this.authService.addTokens(x.token, x.refresh);
                    this.refreshing = false;
                });
            }

            let request : HttpRequest<any> = req;
            let token = this.tokenService.getToken();
            if (token) {
                request = req.clone({
                    headers: req.headers.set('Authorization', 'Bearer ' + token)
                });
            }
    
            return next.handle(request).pipe(catchError(handleError));
        }
        else {
            let request : HttpRequest<any> = req;
            let body = request.body;
            return next.handle(request).pipe(catchError(handleError));
        }
    }

    private refreshing? : boolean;
}