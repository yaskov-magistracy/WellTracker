import {GenderEnum} from "../../enums/GenderEnum";

export type UserInfo = {
  gender: GenderEnum,
  weight: number;
  height: number;
  targetWeight: number;
  tgChatId: number | null;
}
