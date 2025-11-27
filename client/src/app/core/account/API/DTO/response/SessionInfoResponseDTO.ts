import {UserRoleEnum} from "../../../types/UserRoleEnum";


export type SessionInfoResponseDTO = {
  userId: string;
  role: UserRoleEnum;
}
