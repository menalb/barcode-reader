export interface BarcodeDecodeResult {
    barcode: string;
}
export type QueryResult<T> = T | "loading";