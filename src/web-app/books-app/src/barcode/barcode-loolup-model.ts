export interface LookupResponse {
    authors: string[],
    categories: category[],
    description: string,
    id: string,
    identifiers: identifier[],
    images: image,
    title: string,
    subTitle: string,
    isbn: string,
    language: string,
    link: string,
    publishedDate: string,
    publisher: string
}

interface identifier {
    type: string,
    value: string
}
interface category {
    name: string;
}
interface image {
    thumbnail: string,
    smallThumbnail: string
}
