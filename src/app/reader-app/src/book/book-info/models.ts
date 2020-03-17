export interface BookInfo {
    title: string;
    authors: string[];
    description: string;
    categories: string[];
    imageLinks: BookImages;
}

export interface BookImages {
    smallThumbnail: string;
    thumbnail: string;
}
