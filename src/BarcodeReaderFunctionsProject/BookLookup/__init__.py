import requests
import logging
import json
import azure.functions as func
import os
from ..http_helpers.http_response import *


url = os.environ.get('BookLookupAPIUrl')
apikey = os.environ.get('APIKey')

apiConfig = {'key': apikey, 'url': url}


def main(req: func.HttpRequest) -> func.HttpResponse:
    barcode = req.params.get('barcode')

    if barcode is None:
        return badRequestResponse('Invalid Barcode')

    book = getBookInfo(barcode, apiConfig)
    return okResponse(book)


def getBookInfo(barcode: str, bookLookupApiConfig: {}) -> {}:
    params = {
        'q': f'isbn:{barcode}', 'key': bookLookupApiConfig['key']}
    r = requests.get(url=bookLookupApiConfig['url'], params=params)

    response = r.json()

    if 'items' in response and len(response['items']) > 0:
        book = response['items'][0]['volumeInfo']
        return {
            'title': book['title'],
            'authors': book['authors'],
            'description': book['description'],
            'categories': book['categories'],
            'imageLinks': book['imageLinks'],
        }

    return None


def getBarcodeFromRequest(req: func.HttpRequest) -> str:
    return req.params.get('barcode')
