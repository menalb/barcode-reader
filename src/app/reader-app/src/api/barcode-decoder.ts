import { BarcodeDecodeResult } from "./models";

export const fetchBookInfo = async (picture: string): Promise<BarcodeDecodeResult> => {
    const url =
        `${process.env.REACT_APP_API ?? ''}/Reader`;

    const data = new FormData();
    data.append("file", picture)

    const response = await fetch(url, {
        method: 'POST',
        body: data
    });

    return await response.json();
};