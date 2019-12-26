import { ErrorHandler, Injectable, Injector, NgZone } from '@angular/core';
import { Router } from '@angular/router';
@Injectable()
export class GlobalErrorHandler implements ErrorHandler {

    constructor(private injector: Injector, private zone: NgZone) { }

    handleError(error: any) {
        console.error('Unhandled error', error);

        let msg = 'System Error ';

        try {
            const s = error.error as string;
            if (s) {
                msg += s;
            } else {
                const json = JSON.stringify(error, null, 2);
                msg += json;
            }
        } catch (e) { }

        alert(msg);

        const router = this.injector.get(Router);

        this.zone.run(() => {
            router.navigate(['/dashboard']);
        });
    }
}
