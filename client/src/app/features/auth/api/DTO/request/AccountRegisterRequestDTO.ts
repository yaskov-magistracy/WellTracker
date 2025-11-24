import {UserGenderEnum} from "../../../types/UserGenderEnum";
import {UserTargetEnum} from "../../../types/UserTargetEnum";

export type AccountRegisterRequestDTO = {
  login: string;
  password: string;
  gender: UserGenderEnum,
  weight: number;
  height: number;
  target: UserTargetEnum;
}
