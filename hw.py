class Car:
    __model = None
    __year = None
    __manufacturer = None
    __engine_capacity = None
    __color = None
    __price = None

    def __init__(self, model=None, year=None, manufacturer=None, engine_capacity=None, color=None, price=None):
        self.__model = model
        self.__year = year
        self.__manufacturer = manufacturer
        self.__engine_capacity = engine_capacity
        self.__color = color
        self.__price = price

    def __str__(self):
        return f"\n Model: {self.__model}, Year: {self.__year}, Manufacturer: {self.__manufacturer}, Engine: {self.__engine_capacity}, Color: {self.__color}, Price: {self.__price}\n"

    def get_model(self):
        return self.__model

    def get_year(self):
        return self.__year

    def get_manufacturer(self):
        return self.__manufacturer

    def get_engine_capacity(self):
        return self.__engine_capacity

    def get_color(self):
        return self.__color

    def get_price(self):
        return self.__price

    def set_model(self, model):
        self.__model = model

    def set_year(self, year):
        self.__year = year

    def set_manufacturer(self, manufacturer):
        self.__manufacturer = manufacturer

    def set_engine_capacity(self, engine_capacity):
        self.__engine_capacity = engine_capacity

    def set_color(self, color):
        self.__color = color

    def set_price(self, price):
        self.__price = price


class Book:
    __name = None
    __year = None
    __publisher = None
    __genre = None
    __price = None

    def __init__(self, name=None, year=None, publisher=None, genre=None, price=None):
        self.__name = name
        self.__year = year
        self.__publisher = publisher
        self.__genre = genre
        self.__price = price

    def __str__(self):
        return f"\nName: {self.__name}, Year: {self.__year}, Publisher: {self.__publisher}, Genre: {self.__genre}, Price: {self.__price}\n"

    def get_name(self):
        return self.__name

    def get_year(self):
        return self.__year

    def get_publisher(self):
        return self.__publisher

    def get_genre(self):
        return self.__genre

    def get_price(self):
        return self.__price

    def set_name(self, name):
        self.__name = name

    def set_year(self, year):
        self.__year = year

    def set_publisher(self, publisher):
        self.__publisher = publisher

    def set_genre(self, genre):
        self.__genre = genre

    def set_price(self, price):
        self.__price = price


class Stadium:
    __stadium_name = None
    __open_date = None
    __country = None
    __city = None
    __capacity = None

    def __init__(self, name=None, open_date=None, country=None, city=None, capacity=None):
        self.__stadium_name = name
        self.__open_date = open_date
        self.__country = country
        self.__city = city
        self.__capacity = capacity

    def __str__(self):
        return f"\nName: {self.__stadium_name}, Open date: {self.__open_date}, Country: {self.__country}, City: {self.__city}, Capacity: {self.__capacity}\n"

    def get_name(self):
        return self.__stadium_name

    def get_open_date(self):
        return self.__open_date

    def get_country(self):
        return self.__country

    def get_city(self):
        return self.__city

    def get_capacity(self):
        return self.__capacity

    def set_name(self, name):
        self.__stadium_name = name

    def set_open_date(self, open_date):
        self.__open_date = open_date

    def set_country(self, country):
        self.__country = country

    def set_city(self, city):
        self.__city = city

    def set_capacity(self, capacity):
        self.__capacity = capacity


class Fraction:
    __total_objects = 0
    __numerator = 0
    __denominator = 0

    def __init__(self) -> None:
        Fraction.__total_objects += 1

    def __str__(self) -> str:
        return f"\n{self.__numerator}/{self.__denominator}\n"

    @staticmethod
    def get_total_objects():
        return Fraction.__total_objects


class TemperatureFormater:
    __total_counts = 0

    @staticmethod
    def celsius_to_fahrenheit(celsius):
        TemperatureFormater.__total_counts += 1
        return (celsius * 1.8) + 32

    @staticmethod
    def fahrenheit_to_celsius(fahrenheit):
        TemperatureFormater.__total_counts += 1
        return (fahrenheit - 32) / 1.8

    @staticmethod
    def get_total_counts():
        return TemperatureFormater.__total_counts


class UnitConverter:
    @staticmethod
    def inches_to_centimeters(inches):
        return inches * 2.54

    @staticmethod
    def centimeters_to_inches(centimeters):
        return centimeters / 2.54

    @staticmethod
    def feet_to_meters(feet):
        return feet * 0.3048

    @staticmethod
    def meters_to_feet(meters):
        return meters / 0.3048

    @staticmethod
    def miles_to_kilometers(miles):
        return miles * 1.60934

    @staticmethod
    def kilometers_to_miles(kilometers):
        return kilometers / 1.60934