import React, { useState, useEffect } from 'react'

export interface BookImages {
    smallThumbnail: string;
    thumbnail: string;
}
export interface BookInfo {
    title: string;
    authors: string[];
    description: string;
    categories: string[];
    imageLinks: BookImages;
}

type QueryResult<T> = T | "loading";
export const fetchBookInfo = async (): Promise<BookInfo> => {
    const url =
        `${process.env.REACT_APP_API_BARCODE_LOOKUP ?? ''}?barcode=9780241981979`;

    const response = await fetch(url);

    return await response.json();
};


const BookInfoPage = () => {
    const [book, setBook] = useState("loading" as QueryResult<BookInfo>);
    useEffect(() => {
        fetchBookInfo().then(response => setBook(response));
    });
    return (<div>{process.env.REACT_APP_API_BARCODE_LOOKUP}
        {book === 'loading' && "Loading"}
        {book !== 'loading' && <div>
            {JSON.stringify(book)}
        </div>}
    </div>);
}

export default BookInfoPage;