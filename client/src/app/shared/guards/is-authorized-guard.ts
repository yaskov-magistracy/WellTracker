import {CanActivateFn, Router} from "@angular/router";
import {inject} from "@angular/core";
import {AccountService} from "../../auth/data-access/account.service";
import {catchError, map, of} from "rxjs";

export const isAuthorizedGuard: CanActivateFn = () => {
  const router = inject(Router);
  const accountS = inject(AccountService);
  return accountS.session$()
    .pipe(
      map(() => true),
      catchError(() => {
        router.navigate(['/', 'auth', 'login']);
        return of(false);
      })
    )

}
