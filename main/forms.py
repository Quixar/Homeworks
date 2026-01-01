from django import forms

class LoginForm(forms.Form):
    username = forms.CharField(label="Login")
    password = forms.CharField(label="Password", widget=forms.PasswordInput)


class CalcForm(forms.Form):
    num1 = forms.IntegerField(label="Num 1")
    num2 = forms.IntegerField(label="Num 2")
    num3 = forms.IntegerField(label="Num 3")

    radio = forms.ChoiceField(label="Select your operation", choices=[('min', 'min'), ('max', 'max'), ('avg', 'avg')],
                              widget=forms.RadioSelect)


class RegistrationForm(forms.Form):
    first_name = forms.CharField(label="Ім'я", max_length=50)
    last_name = forms.CharField(label="Прізвище", max_length=50)
    age = forms.IntegerField(label="Вік", min_value=1)
    email = forms.EmailField(label="Email")


    gender = forms.ChoiceField(label="Стать", choices=[('m', 'male'), ('f', 'female')], widget=forms.RadioSelect)

    address = forms.CharField(label="Адреса для доставки", widget=forms.Textarea(attrs={'rows': 3}))

    subscribe = forms.BooleanField(
        label="Бажаєте підписатися на новини нашого інтернет-магазину?",
        required=False
    )

class ProgDayForm(forms.Form):
    year = forms.IntegerField(label="Enter year", min_value=1, max_value=9999)