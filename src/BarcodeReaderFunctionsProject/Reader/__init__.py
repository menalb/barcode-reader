import io
import logging
from PIL import Image
from pyzbar import pyzbar
import azure.functions as func

from ..http_helpers.http_response import *


def main(req: func.HttpRequest) -> func.HttpResponse:
    body = req.get_body()

    try:
        image = Image.open(io.BytesIO(body))

        found = list(readBarcode(image))

        return okResponse(found)

    except IOError:
        return func.HttpResponse(
            "Bad input. Unable to cast request body to an image format.",
            status_code=400)


def readBarcode(image):
    barcodes = pyzbar.decode(image)
    for barcode in barcodes:
        # (x, y, w, h) = barcode.rect

        yield {
            'barcode': barcode.data.decode("utf-8"),
            'type': barcode.type
        }
