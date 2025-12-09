from django.http import HttpResponse
from django.shortcuts import render

# Create your views here.

def task3(request):
    import datetime
    return HttpResponse(datetime.datetime.now())
def task4(request):
    rez = []
    for i in range(1, 11):
        for j in range(1, 11):
            rez.append(str(i) + " * " + str(j) + " = " + str(i * j) + "<br>")
    return HttpResponse(rez)
def task5(request):
    import datetime
    year = datetime.datetime.now().year
    p_day = datetime.datetime(year, 1, 1) + datetime.timedelta(days=255)
    return HttpResponse("Programmers day " + str(p_day))