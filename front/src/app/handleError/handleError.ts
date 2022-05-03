import { throwError } from "rxjs";

export function handleError(error: any, HttpErrorResponse: any) {
    if (error.error instanceof ErrorEvent) {
        return throwError(error.error.message);
    } else {
        return throwError(JSON.stringify(error.error));
    }
}
