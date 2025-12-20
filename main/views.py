from django.http import HttpResponse
from django.shortcuts import render

# Create your views here.

def index(request):
    return HttpResponse("Home")

def news(request):
    return HttpResponse("News")

def facts(request):
    return HttpResponse("Facts")

def contacts(request):
    return HttpResponse("Contacts")

def management(request):
    return HttpResponse("Management")

def history(request):
    return HttpResponse("History")

def people(request):
    return HttpResponse("People")

def photos(request):
    return HttpResponse("Photos")