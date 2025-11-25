import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {SessionInfoResponseDTO} from "./DTO/response/SessionInfoResponseDTO";
import {AccountLoginRequestDTO} from "./DTO/request/AccountLoginRequestDTO";
import {AccountRegisterRequestDTO} from "./DTO/request/AccountRegisterRequestDTO";

@Injectable({
  providedIn: 'root'
})
export class AccountApiService {

  #httpClient = inject(HttpClient);

  #apiPath = 'Accounts';

  login$(accountLoginInfo: AccountLoginRequestDTO) {
    return this.#httpClient.post<SessionInfoResponseDTO>(`/api/${this.#apiPath}/login`, accountLoginInfo);
  }

  register$(accountRegisterInfo: AccountRegisterRequestDTO) {
    return this.#httpClient.post(`/api/${this.#apiPath}/register/user`, accountRegisterInfo);
  }

  session$() {
    return this.#httpClient.get<SessionInfoResponseDTO>(`/api/${this.#apiPath}/session`);
  }

  logout$() {
    return this.#httpClient.post(`/api/${this.#apiPath}/logout`, null);
  }

}
