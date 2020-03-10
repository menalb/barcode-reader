import azure.functions as func
import json

def okResponse(messageBody) -> {}:
    return func.HttpResponse(json.dumps(messageBody), status_code=200)


def badRequestResponse(messageBody=None) -> {}:
    return func.HttpResponse(json.dumps(messageBody or ''), status_code=400)
