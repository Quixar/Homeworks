from django.shortcuts import render
from django.template.defaulttags import csrf_token
import datetime
from main.forms import *


# Create your views here.

def login_view(request):
    db = {
        'user1' : {'password': 'qwerty', 'role': 'admin'},
        'user2' : {'password': '1234', 'role': 'user'},
        'user3' : {'password': 'zxcv', 'role': 'user'},
    }

    message = ''
    if request.method == 'POST':
        form = LoginForm(request.POST)
        if form.is_valid():
            username = form.cleaned_data['username']
            password = form.cleaned_data['password']

            if username in db:
                if db[username]['password'] == password:
                    role = db[username]['role']
                    message = f'Welcome {username}! You are now logged in as {role}'
                else:
                    message = 'Wrong password'
            else:
                message = 'User with that username does not exist'
    else:
        form = LoginForm()
    return render(request, 'task1.html', {'form': form, 'message': message})

def calc_view(request):
    rez = None
    if request.method == 'POST':
        form = CalcForm(request.POST)
        if form.is_valid():
            num1 = form.cleaned_data['num1']
            num2 = form.cleaned_data['num2']
            num3 = form.cleaned_data['num3']

            nums = [num1, num2, num3]

            oper = form.cleaned_data['radio']

            if oper == 'min':
                rez = f'Min: {min(nums)}'
            elif oper == 'max':
                rez = f'Max: {max(nums)}'
            elif oper == 'avg':
                rez = f'Avg: {sum(nums)/len(nums)}'
    else:
        form = CalcForm()
    return render(request, 'task2.html', {'form': form, 'rez': rez})

def register_view(request):
    user_data = None
    if request.method == 'POST':
        form = RegistrationForm(request.POST)
        if form.is_valid():
            user_data = form.cleaned_data
            user_data['gender_display'] = dict(form.fields['gender'].choices).get(user_data['gender'])
    else:
        form = RegistrationForm()

    return render(request, 'task3.html', {'form': form, 'user_data': user_data})


def programmer_day_view(request):
    result = None
    if request.method == 'POST':
        form = ProgDayForm(request.POST)
        if form.is_valid():
            year = form.cleaned_data['year']

            start_of_year = datetime.date(year, 1, 1)
            prog_day = start_of_year + datetime.timedelta(days=255)

            days_ukr = {
                0: "понеділок", 1: "вівторок", 2: "середа",
                3: "четвер", 4: "п'ятниця", 5: "субота", 6: "неділя"
            }

            months_ukr = {9: "вересня"}

            day_name = days_ukr[prog_day.weekday()]
            month_name = months_ukr[prog_day.month]

            result = f"{prog_day.day} {month_name} ({day_name})"
    else:
        form = ProgDayForm()

    return render(request, 'task4.html', {'form': form, 'result': result})