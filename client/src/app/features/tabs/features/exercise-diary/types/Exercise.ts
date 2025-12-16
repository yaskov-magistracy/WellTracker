import {ExerciseTypeEnum} from "./ExerciseTypeEnum";
import {ExerciseMeasurementEnum} from "./ExerciseMeasurementEnum";
import {MuscleTypeEnum} from "./MuscleTypeEnum";
import {EquipmentTypeEnum} from "./EquipmentTypeEnum";

export type Exercise = {
  id: string;
  name: string;
  description?: string;
  type: ExerciseTypeEnum;
  measurement: ExerciseMeasurementEnum;
  muscles: MuscleTypeEnum[];
  equipments: EquipmentTypeEnum[];
}
