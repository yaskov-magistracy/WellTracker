import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Chat} from "../types/Chat";
import {SendMessageInChatResponseDTO} from "./DTO/responses/SendMessageInChatResponseDTO";
import {GetMessagesInChatResponseDTO} from "./DTO/responses/GetMessagesInChatResponseDTO";

@Injectable({
  providedIn: 'root'
})
export class AiConsultantApiService {

  private _httpClient = inject(HttpClient);

  private _apiPath = 'Chats';

  getChats$() {
    return this._httpClient.get<Chat[]>(`/api/${this._apiPath}`);
  }

  createChat$(title: string) {
    return this._httpClient.post<Chat>(`/api/${this._apiPath}`, { title });
  }

  getChatMessages$(chatId: string, take: number, skip: number) {
    return this._httpClient.get<GetMessagesInChatResponseDTO>(`/api/${this._apiPath}/${chatId}`, {
      params: {
        take,
        skip
      }
    });
  }

  sendMessageInChat$(message: string, chatId: string) {
    return this._httpClient.post<SendMessageInChatResponseDTO>(`/api/${this._apiPath}/${chatId}/messages`, { message });
  }
}
