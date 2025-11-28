
export type SearchFoodRequestDTO = {
  "take": number;
  "skip": number;
  "searchText": string;
  "excludedIds": string[];
}
