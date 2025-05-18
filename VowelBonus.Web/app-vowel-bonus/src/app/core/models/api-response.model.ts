export interface ApiResponse<T> {
  succeeded: boolean;
  message?: string;
  totalRecord: number;
  result?: T;
}