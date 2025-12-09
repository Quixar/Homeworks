from django.urls import path
from main.views import *

urlpatterns = [
    path("", index, name="home"),
    path("about/", about),
    path("contact/", contact),
    path("task4/", task4),
    path("task5/", task5)
]