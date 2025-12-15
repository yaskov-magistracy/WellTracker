import {Message} from "../../../types/Message";

export type SendMessageInChatResponseDTO = {
  sent: Message;
  received: Message;
}
