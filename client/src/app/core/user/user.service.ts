import {computed, effect, inject, Injectable, signal} from '@angular/core';
import {UserApiService} from "./user.service.api";
import {AccountService} from "../account/account.service";
import {UserInfo} from "./types/UserInfo";
import {tap} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  #userApiS = inject(UserApiService);
  #accountS = inject(AccountService);

  #userInfo = signal<UserInfo | null>(null);
  userInfo = this.#userInfo.asReadonly();

  constructor() {
    this.#initUserInfo();
  }

  #getUserInfo$(userId: string) {
    return this.#userApiS.getUserInfo$(userId);
  }

  updateUserInfo$(newUserInfo: UserInfo) {
    return this.#userApiS.updateUserInfo$(this.#accountS.userId()!, newUserInfo)
      .pipe(
        tap(() => this.#userInfo.set(newUserInfo))
      );
  }

  #initUserInfo() {
    effect(() => {
      const sessionInfo = this.#accountS.sessionInfo.value();
      if (!sessionInfo) { this.#userInfo.set(null); }
      else {
        this.#getUserInfo$(sessionInfo.userId)
          .subscribe(userInfo => this.#userInfo.set(userInfo));
      }
    });
  }
}
