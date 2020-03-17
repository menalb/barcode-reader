import React from 'react';
import { BookInfo as BookInfoModel } from './models';
import './book-info.css'

const BookInfo = (book: BookInfoModel) => (
    <article className="book-info">
        <section className="book-image">
            <img src={book.imageLinks.smallThumbnail} />
        </section>
        <div className="book-details">
            <h2 className="title">
                {book.title}
            </h2>
            <h3 className="authors">
                {concat(book.authors.map(author => author))}
            </h3>
            <em className="categories">
                {concat(book.categories.map(category => category))}
            </em>
            <section className="description">
                {book.description}
            </section>
        </div>
    </article>
);

const concat = (items: string[]) => items.join(' - ');
export default BookInfo;