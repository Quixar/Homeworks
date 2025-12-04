import time

def task1(start, end):
    for num in range(start, end + 1):
        if num % 2 != 0:
            yield num

def task2(data, start, end):
    for item in data:
        if not (start <= item <= end):
            yield item


def task3():
    def horizontal_line(symbol):
        print(symbol * 20)

    def show_line(symbol, function_to_call):
        function_to_call(symbol)

    show_line('-', horizontal_line)


def task4():
    def decorator(func):
        def wrapper():
            start_time = time.time()
            result = func()
            end_time = time.time()
            print(f"Execution time: {end_time - start_time} seconds")
            return result

        return wrapper

    @decorator
    def get_even_numbers():
        return [x for x in range(100000) if x % 2 == 0]

    print(get_even_numbers())


def task5():
    def decorator(func):
        def wrapper(*args, **kwargs):
            start_time = time.time()
            result = func(*args, **kwargs)
            end_time = time.time()
            print(f"Execution time: {end_time - start_time} seconds")
            return result

        return wrapper

    @decorator
    def get_even_numbers(start, end):
        return [x for x in range(start, end + 1) if x % 2 == 0]

    print(get_even_numbers(0, 100000))


task1()
print("-" * 20)
task2()
print("-" * 20)
task3()
print("-" * 20)
task4()
print("-" * 20)
task5()