import { VowelBonusScoreHistory } from "./vowel-bonus-score-history.model";

export interface ApiResponse<T> {
  succeeded: boolean;
  message?: string;
  result?: T;
}