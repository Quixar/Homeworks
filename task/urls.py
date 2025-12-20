from django.urls import path, include
from task.views import *


urlpatterns = [
    path('home/', index, name='home'),
    path('history/', history),
    path('history/<int:year>', history),
    path('cities/<str:city>/<int:year>/', history),
    path('cities/<str:city>', cities),
    path('cities/', cities),
    path('facts/', facts),
]