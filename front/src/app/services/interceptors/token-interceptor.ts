import { Inject, Injectable } from "@angular/core";
import { TokenService } from "../token.service";
import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs";

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
    constructor(private tokenService : TokenService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const token = this.tokenService.getToken();
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
            console.error("Erro: " + error.error.message);
        } else {
            console.error("Código de erro: " + error.status + "; erro: " + JSON.stringify(error.error));
        }

        return throwError("Ocorreu um erro, tente novamente.");
    }
}