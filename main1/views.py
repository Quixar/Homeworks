from django.http import HttpResponse
from django.shortcuts import render

# Create your views here.

def index(request):
    return HttpResponse("Main 2")

def about(request):
    return HttpResponse("About 2")

def contact(request):
    return HttpResponse("Contact 2")