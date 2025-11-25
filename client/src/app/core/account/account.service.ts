import {computed, inject, Injectable, signal} from '@angular/core';
import {Observable, shareReplay, tap} from "rxjs";
import {AccountApiService} from "./API/account.api.service";
import {AccountLoginRequestDTO} from "./API/DTO/request/AccountLoginRequestDTO";
import {SessionInfoResponseDTO} from "./API/DTO/response/SessionInfoResponseDTO";
import {SessionInfo} from "./types/SessionInfo";
import {AccountRegisterRequestDTO} from "./API/DTO/request/AccountRegisterRequestDTO";
import {httpResource} from "@angular/common/http";
import {rxResource} from "@angular/core/rxjs-interop";

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  #accountApiS = inject(AccountApiService);

  #sessionInfo = rxResource<SessionInfo | null, unknown>(
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

  logout$() {
    return this.#accountApiS.logout$()
      .pipe(
        tap(() => this.#sessionInfo.set(null))
      );
  }

  #session$() {
    return this.#accountApiS.session$();
  }

}
