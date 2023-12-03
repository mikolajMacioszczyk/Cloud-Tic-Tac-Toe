export interface Cell {
    id: string;
    fieldState: 'Empty' | 'X' | 'O';
    rowNumber: number;
    columnNumber: number;
}
