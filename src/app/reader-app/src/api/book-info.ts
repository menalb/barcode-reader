import { BookInfo } from "../book/book-info/models";


export const fetchBookInfo = async (barcode: string): Promise<BookInfo> => {
    const url =
        `${process.env.REACT_APP_API ?? ''}/BookLookup?barcode=${barcode}`;

    const response = await fetch(url);

    return await response.json();
};