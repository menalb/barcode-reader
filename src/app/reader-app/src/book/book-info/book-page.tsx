import React, { useState, useEffect } from 'react'
import { BookInfo as BookInfoModel } from './models';
import BookInfo from './book-info';
import { QueryResult } from '../../api/models';
import { fetchBookInfo } from '../../api/book-info';

const BookInfoPage = () => {
    const barcode = '9780241981979';
    const [book, setBook] = useState("loading" as QueryResult<BookInfoModel>);
    useEffect(() => {
        fetchBookInfo(barcode).then(response => setBook(response));
    }, [barcode]);
    return (
        <section>
            {book === 'loading' && "Loading"}
            {book !== 'loading' && <div>
                <BookInfo {...book} />
            </div>}
        </section>);
}

export default BookInfoPage;