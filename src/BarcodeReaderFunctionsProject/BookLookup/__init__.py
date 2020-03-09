import requests
import logging
import json
import azure.functions as func
import os


def main(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Python HTTP trigger function processed a request.')

    barcode = req.params.get('barcode')

    apikey = os.environ.get('APIKey')

    url = os.environ.get('BookLookupAPIUrl')

    logging.info(apikey)
    try:
        params = {
            'q': f'isbn:{barcode}', 'key': apikey}
        r = requests.get(url=url, params=params)

        response = r.json()

        book = response['items'][0]['volumeInfo']
        return func.HttpResponse(json.dumps(book))
    except ValueError:
        pass

    if barcode:
        return func.HttpResponse(f"Hello {barcode}!")
    else:
        return func.HttpResponse(
            "Please pass a name on the query string or in the request body",
            status_code=400
        )
