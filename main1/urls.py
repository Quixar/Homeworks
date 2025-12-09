from django.urls import path
from main1.views import *

urlpatterns = [
    path("", index, name="home"),
    path("about/", about),
    path("contact/", contact)
]