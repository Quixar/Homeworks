from django.contrib import admin
from main.views import *
from django.urls import path


urlpatterns = [
    path('', login_view, name='login'),
    path('task2/', calc_view, name='task2'),
    path('task3/', register_view, name='task3'),
    path('task4/', programmer_day_view, name='task4'),
]