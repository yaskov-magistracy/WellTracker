import {CanActivateFn, Router} from "@angular/router";
import {inject} from "@angular/core";
import {catchError, filter, firstValueFrom, of, take} from "rxjs";
import {AccountService} from "../account/account.service";
import {toObservable} from "@angular/core/rxjs-interop";

export const isUnauthorizedGuard: CanActivateFn = async () => {
  const router = inject(Router);
  const sessionInfo = inject(AccountService).sessionInfo;
  if (sessionInfo.isLoading()) {
    await firstValueFrom(
      toObservable(sessionInfo.value).pipe(
        catchError(() => of(null)),
        filter(() => !sessionInfo.isLoading()),
        take(1)
      )
    );
  }
  if (sessionInfo.error() || !sessionInfo.value()) {
    return true;
  }
  router.navigate(['/', 'auth', 'login']);
  return false;
}
