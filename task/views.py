from django.http import HttpResponse, HttpResponseRedirect
from django.shortcuts import render, redirect


# Create your views here.

def index(request):
    return HttpResponse("Main page")


def history(request, city=None, year=None):
    db = {
        "Paris": {
            1924: "In 1924 something happened in Paris",
        },
        "Marseille": {
            1956: "In 1956 something happened in Marseille",
        }
    }

    if not city:
        city = request.GET.get('city')

    if not year:
        year_param = request.GET.get('year')
        if year_param and year_param.isdigit():
            year = int(year_param)

    if city in db and year in db[city]:
        info = db[city][year]
        return HttpResponse(f"<h1>{city} ({year})</h1><p>{info}</p>")

    return redirect('/cities/')

def cities(request, city="Undef"):
    db = ["Paris", "Marseille"]
    if city in db:
        return HttpResponse(f"<h1>{city}</h1> Something about {city}")
    else:
        content = "<h1>Frances cities</h1><ul>"
        for item in db:
            content += f'<li><a href="{item}">{item}</a></li>'
        content += "</ul>"
        return HttpResponse(content)

def facts(request):
    return HttpResponse("Facts page")