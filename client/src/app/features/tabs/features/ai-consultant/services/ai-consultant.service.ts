import {inject, Injectable} from "@angular/core";
import {AiConsultantApiService} from "../dal/ai-consultant.api.service";
import {map} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AiConsultantService {

  #aiConsultantApiS = inject(AiConsultantApiService);

  getChats$() {
    return this.#aiConsultantApiS.getChats$()
      .pipe(
        map(chats => chats.reverse())
      );
  }

  createChat$(title: string) {
    return this.#aiConsultantApiS.createChat$(title);
  }

  getChatMessages$(chatId: string, take: number, skip: number) {
    return this.#aiConsultantApiS.getChatMessages$(chatId, take, skip)
      .pipe(
        map(res => res.items.sort((f, s) =>
          new Date(f.dateTime).getTime() - new Date(s.dateTime).getTime() ))
      );
  }

  sendMessageInChat$(message: string, chatId: string) {
    return this.#aiConsultantApiS.sendMessageInChat$(message, chatId);
  }
}
