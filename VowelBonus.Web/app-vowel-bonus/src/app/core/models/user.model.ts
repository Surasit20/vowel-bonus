import { VowelBonusScoreHistory } from "./vowel-bonus-score-history.model";

export interface User {
  userId?: number;
  userName?: string;
  totalPoint?: number;
  lastLoginDate?: Date;
  vowelBonusScoreHistories?: VowelBonusScoreHistory[];
}