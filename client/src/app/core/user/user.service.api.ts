import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {UserInfo} from "./types/UserInfo";

@Injectable({
  providedIn: 'root'
})
export class UserApiService {

  #httpClient = inject(HttpClient);

  #apiPath = 'Users';

  getUserInfo$(userId: string) {
    return this.#httpClient.get<UserInfo>(`/api/${this.#apiPath}/${userId}/info`);
  }

  updateUserInfo$(userId: string, newUserInfo: UserInfo) {
    return this.#httpClient.post<UserInfo>(`/api/${this.#apiPath}/${userId}/info`, newUserInfo);
  }
}
