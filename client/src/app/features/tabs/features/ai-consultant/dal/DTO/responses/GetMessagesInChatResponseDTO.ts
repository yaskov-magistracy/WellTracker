import {Message} from "../../../types/Message";

export type GetMessagesInChatResponseDTO = {
  items: Message[];
  totalCount: number;
}
