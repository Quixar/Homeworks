# tuple
t1 = 3, 2, 1
t2 = 3, 4, 5
t3 = 3, 6, 7

s1 = set(t1)
s2 = set(t2)
s3 = set(t3)


def task1(*elem):
    return s1 & s2 & s3

def task2(*elem):
    return s1 - (s2 | s3)

def task3(*elem):
    return [a for a, b, c in zip(t1, t2, t3) if a == b == c]

print(task1(s1, s2, s3))
print(task2(s1, s2, s3))
print(task3(t1, t2, t3))

# dictionary

def task4():
    d = {}

    name = input("Enter name: ")
    height = int(input("Enter height: "))
    d[name] = height

    if name in d:
        del d[name]

    if name in d:
        print(name, d[name])

    new_name = input("Enter new name: ")
    new_height = int(input("Enter new height: "))
    if name in d:
        d[name] = new_height
        d[new_name] = d.pop(name)

    return d

def task5():
    d = {}

    origin = input("Enter word: ")
    translation = int(input("Enter translation for this word: "))
    d[origin] = translation

    if origin in d:
        del d[origin]

    if origin in d:
        print(origin, d[origin])

    new_origin = input("Enter new word: ")
    new_translation = int(input("Enter new translation for this word: "))
    if origin in d:
        d[origin] = new_translation
        d[new_origin] = d.pop(origin)

    return d


def task6():
    d = {}

    name = input("Enter name: ")
    phone = input("Enter phone: ")
    email = input("Enter email: ")
    job_title = input("Enter job title: ")
    office_number = input("Enter office number: ")
    skype = input("Enter skype: ")

    d[name] = [phone, email, job_title, office_number, skype]

    if name in d:
        del d[name]

    if name in d:
        print(name, d[name])

    new_name = input("Enter new name: ")
    new_phone = input("Enter new phone: ")
    new_email = input("Enter new email: ")
    new_job_title = input("Enter new job title: ")
    new_office_number = input("Enter new office number: ")
    new_skype = input("Enter new skype: ")

    if name in d:
        d[name] = [new_phone, new_email, new_job_title, new_office_number, new_skype]
        d[new_name] = d.pop(name)

    return d

def task7():
    d = {}

    author = input("Enter author: ")
    title = input("Enter title: ")
    genre = input("Enter genre: ")
    year = int(input("Enter year: "))
    number_pages = int(input("Enter number of pages: "))
    publisher = input("Enter publisher: ")

    d[author] = [title, genre, year, number_pages, publisher]

    if author in d:
        del d[author]

    if author in d:
        print(author, d[author])

    new_author = input("Enter new author: ")
    new_title = input("Enter new title: ")
    new_genre = input("Enter new genre: ")
    new_year = int(input("Enter new year: "))
    new_number_pages = int(input("Enter number of pages: "))
    new_publisher = input("Enter publisher: ")

    if author in d:
        d[author] = [new_title, new_genre, new_year, new_number_pages, new_publisher]
        d[new_author] = d.pop(author)

    return d