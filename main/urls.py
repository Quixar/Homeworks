from django.urls import path, include, re_path
from main.views import *

urlpatterns = [
    path('', index, name='index'),
    re_path(r'^news/.*$', news, name='news'),
    re_path(r'^management/.*$', news, name='news'),
    re_path(r'^facts/.*$', news, name='news'),
    re_path(r'^contacts/.*$', news, name='news'),
    path('history/', history, name='history'),
    path('history/people/', people, name='people'),
    path('history/photos/', photos, name='photos'),
]