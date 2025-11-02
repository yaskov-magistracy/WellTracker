import {inject, Injectable, signal} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AccountRegisterRequestDTO} from "../api/DTO/request/AccountRegisterRequestDTO";
import {AccountLoginRequestDTO} from "../api/DTO/request/AccountLoginRequestDTO";
import {SessionInfo} from "../types/SessionInfo";
import {SessionInfoResponseDTO} from "../api/DTO/response/SessionInfoResponseDTO";
import {Observable, shareReplay, tap} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private httpClient = inject(HttpClient);

  private apiPath = '/api/Accounts';

  sessionInfo = signal<SessionInfo | null>(null);

  // Кэшируем сессию с shareReplay(1)
  private sessionCache$!: Observable<SessionInfoResponseDTO>;

  login$(accountLoginInfo: AccountLoginRequestDTO) {
    return this.httpClient.post<SessionInfoResponseDTO>(`${this.apiPath}/login`, accountLoginInfo)
      .pipe(
        tap(res => this.sessionInfo.set(res))
      );
  }

  register$(accountRegisterInfo: AccountRegisterRequestDTO) {
    return this.httpClient.post(`${this.apiPath}/register/user`, accountRegisterInfo);
  }

  session$() {
    if (!this.sessionCache$) {
      this.sessionCache$ = this.httpClient.get<SessionInfoResponseDTO>(`${this.apiPath}/session`).pipe(
        shareReplay(1) // Кэшируем последний успешный результат
      );
    }
    return this.sessionCache$;
  }

  logout$() {
    return this.httpClient.post(`${this.apiPath}/logout`, null);
  }

}
