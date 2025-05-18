export interface VowelBonusScoreHistory {
  vowelBonusScoreHistoryId : number;
  word: string;
  point: number;
  createdDate?: Date;
  isEditing:boolean;
  editingWord: string;
}