import { VowelBonusScoreHistory } from "./vowel-bonus-score-history.model";

export interface VowelBonusScoreResponse {
  totalPoint?: number;
  vowelBonusScoreHistories?: VowelBonusScoreHistory[];
}