from django.views import View
from django.core.serializers.json import DjangoJSONEncoder
from django.http import HttpResponse, HttpResponseBadRequest
from django.shortcuts import render
from django.http import JsonResponse
from django.http import HttpResponseRedirect
# Create your views here.

def index(request):
    return JsonResponse({'name': 'Tom', 'age': 22})

class Person:
    def __init__(self, name, age) -> None:
        self.name = name
        self.age = age

# 1 VAR
# def person(request):
#     bob = Person('Bob', 19)
#     return JsonResponse(bob.__dict__)

# 2 VAR
def person(request):
    bob = Person('Bob', 19)
    return JsonResponse(bob, safe=False, ecnoder=PersonEncoder)

class PersonEncoder(DjangoJSONEncoder):
    def default(self, o):  # обязательно у параметра должно быть имя o!!!
        if isinstance(o, Person):
            return {"name": o.name, "age": o.age}
            # return o.__dict__
        return super().default(o)


def list(request):
    lst = [Person(name='Tom', age=22).__dict__, Person(name='Bob', age=19).__dict__]
    return JsonResponse({'list': lst})

# ===== Задание на урок: Статьи по году =====
# Создайте представление articles_by_year, которое принимает параметр year из URL и возвращает JSON-ответ со списком статей, опубликованных в этом году.
# Для этого создайте фиктивный список статей в самом представлении, где каждая статья представлена словарем с ключами id, title и year.
# Отфильтруйте статьи по переданному году и верните JSON-ответ, содержащий количество статей и их список.

def article(request, year):
    articles = [
        {'article': "asdfasdf",'year': 2025},
        {'article': "asdfasdf",'year': 2024},
        {'article': "asdfasdf",'year': 2023},
        {'article': "asdfasdf",'year': 2022},
        {'article': "asdfasdf",'year': 2021},
    ]
    rez = []
    for art in articles:
        if article['year'] == year:
            rez.append(art)

    return JsonResponse(rez, safe=False)


def request_info(request):
    info = f'''
<h1> Request Info </h1>
<ul>
    <li> Path: {request.path} </li>
    <li> Method: {request.method} </li>
    <li> Host: {request.get_host()} </li>
    <li> Agent: {request.META["HTTP_USER_AGENT"]} </li>
</ul>'''
    return HttpResponse(info)

def response_info(request):
    response = HttpResponse("Response Info: ")
    response["Custom Header"] = "Custom Value"
    response.status_code = 202
    return response

def user(request):
    name = request.GET.get('name')
    age = request.GET.get('age')
    return HttpResponse(f"{name}, {age}")

def bad(request):
    return HttpResponseBadRequest("Bad Request")

def about(request):
    return HttpResponseRedirect("/list/")

class SimpleView(View):
    def get(self, request):
        return HttpResponse("This is GET")
    def post(self, request):
        return HttpResponse("This is POST")

class HelloUser(View):
    def get(self, request, username):
        return HttpResponse(f"Hello, {username}")