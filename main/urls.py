from django.urls import path, include
from main.views import *


urlpatterns = [
    path('', index, name='index_json'),
    path('person/', person, name='person_json'),
    path('list/', list, name='list_json'),
    path("article/<int:year>", article),
    path("request_info/", request_info),
    path("response_info/", response_info),
    path('user/', user),
    path('bad/', bad),
    path('about/', about),
    path("simple/", SimpleView.as_view()),
    path("hello/<str:username>", HelloUser.as_view()),
]