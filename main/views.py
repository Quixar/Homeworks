from django.http import HttpResponse

from django.shortcuts import render

# Create your views here.

def index(request):
    return HttpResponse("<h1>Hello world! 1</h1>")

def about(request):
    return HttpResponse("About 1")

def contact(request):
    return HttpResponse("Contact 1")

def task4(request):
    import datetime

    days_ua = {
        0: "Monday",
        1: "Tuesday",
        2: "Wednesday",
        3: "Thursday",
        4: "Friday",
        5: "Saturday",
        6: "Sunday",
    }
    day_index = datetime.datetime.today().weekday()

    return HttpResponse(f"Today: {days_ua[day_index]}")

def task5(request):
    import random

    a = ["Что разум человека может постигнуть и во что он может поверить, того он способен достичь",
         "Стремитесь не к успеху, а к ценностям, которые он дает",
         "Своим успехом я обязана тому, что никогда не оправдывалась и не принимала оправданий от других."]

    rez = random.choice(a)

    return HttpResponse(rez)