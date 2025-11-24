import {CanActivateFn, Router} from "@angular/router";
import {inject} from "@angular/core";
import {catchError, map, of} from "rxjs";
import {AccountService} from "../../features/auth/data-access/account.service";

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
