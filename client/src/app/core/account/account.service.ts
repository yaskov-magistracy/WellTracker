import {computed, inject, Injectable} from '@angular/core';
import {tap} from "rxjs";
import {AccountApiService} from "./API/account.api.service";
import {AccountLoginRequestDTO} from "./API/DTO/request/AccountLoginRequestDTO";
import {SessionInfo} from "./types/SessionInfo";
import {AccountRegisterRequestDTO} from "./API/DTO/request/AccountRegisterRequestDTO";
import {errorRxResource} from "../error-handling/errorRxResource";

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  #accountApiS = inject(AccountApiService);

  #sessionInfo = errorRxResource<SessionInfo | null, unknown>(
    { stream: () => this.#session$() }
  );
  sessionInfo = this.#sessionInfo.asReadonly();
  userId = computed(() => this.#sessionInfo.value()?.userId);

  login$(accountLoginInfo: AccountLoginRequestDTO) {
    return this.#accountApiS.login$(accountLoginInfo)
      .pipe(
        tap(res => this.#sessionInfo.set(res))
      );
  }

  register$(accountRegisterInfo: AccountRegisterRequestDTO) {
    return this.#accountApiS.register$(accountRegisterInfo);
  }

  changePassword$(oldPassword: string, newPassword: string) {
    return this.#accountApiS.changePassword$({
      newPassword,
      oldPassword
    }).pipe(
      tap(() => this.#sessionInfo.set(null))
    );
  }

  logout$() {
    return this.#accountApiS.logout$()
      .pipe(
        tap(() => {
          this.#sessionInfo.set(null);
          location.reload();
        })
      );
  }

  #session$() {
    return this.#accountApiS.session$();
  }

}
