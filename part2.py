def task1(start, end):
    a, b = 0, 1
    while a <= end:
        if a >= start:
            yield a
        a, b = b, a + b


def task2(list1, list2):
    len1, len2 = len(list1), len(list2)
    max_len = max(len1, len2)
    for i in range(max_len):
        val1 = list1[i] if i < len1 else 0
        val2 = list2[i] if i < len2 else 0
        yield val1 + val2

def task3():
    def square_val(val):
        return val ** 2

    def cube_val(val):
        return val ** 3

    def calculate(list_to_work, function_to_call):
        return [function_to_call(x) for x in list_to_work]