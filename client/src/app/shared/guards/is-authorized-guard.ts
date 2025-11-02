import {CanActivateFn, Router} from "@angular/router";
import {inject} from "@angular/core";
import {AccountService} from "../../auth/data-access/account.service";
import {map} from "rxjs";

export const isAuthorizedGuard: CanActivateFn = () => {
  const router = inject(Router);
  const accountS = inject(AccountService);
  return accountS.session$()
    .pipe(
      map(session => {
        if (session) { return true; }
        router.navigate(['/', 'auth', 'login']);
        return false;
      })
    )

}
