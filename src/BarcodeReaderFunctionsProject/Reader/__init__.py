import io
import logging

from PIL import Image
from pyzbar import pyzbar

import azure.functions as func


def main(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Python HTTP trigger function processed a request.')

    body = req.get_body()

    try:
        image = Image.open(io.BytesIO(body))

        barcodes = pyzbar.decode(image)

        for barcode in barcodes:
            #  extract the bounding box location of the barcode and draw the
            # bounding box surrounding the barcode on the image
            (x, y, w, h) = barcode.rect
            # cv2.rectangle(image, (x, y), (x + w, y + h), (0, 0, 255), 2)
            # the barcode data is a bytes object so if we want to draw it on
            # our output image we need to convert it to a string first
            barcodeData = barcode.data.decode("utf-8")
            barcodeType = barcode.type
            # draw the barcode data and barcode type on the image
            text = "{} ({})".format(barcodeData, barcodeType)
            # cv2.putText(image, text, (x, y - 10), cv2.FONT_HERSHEY_SIMPLEX,
            #            0.5, (0, 0, 255), 2)
            # print the barcode type and data to the terminal
            found = "[INFO] Found {} barcode: {}".format(
                barcodeType, barcodeData)
            return func.HttpResponse(found, status_code=200)
    
    except IOError:
        return func.HttpResponse(
            "Bad input. Unable to cast request body to an image format.",
            status_code=400)