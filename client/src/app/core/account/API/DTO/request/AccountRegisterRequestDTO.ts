import {GenderEnum} from "../../../../enums/GenderEnum";

export type AccountRegisterRequestDTO = {
  login: string;
  password: string;
  gender: GenderEnum;
  weight: number;
  height: number;
  targetWeight: number;
  tgChatId: number | null;
}
