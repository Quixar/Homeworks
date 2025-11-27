import random

matrix = [[random.randint(0, 100) for _ in range(5)] for _ in range(5)]

for row in matrix:
    print(row)

def task1(matrix):
    for i in range(5):
        for j in range(i + 1, 5):
            matrix[i][j], matrix[j][i] = matrix[j][i], matrix[i][j]
    return matrix

def task2(matrix):
    for i in range(5):
        main = matrix[i][i]
        side = matrix[i][4 - i]

        matrix[i][i] = side
        matrix[i][4 - i] = main
    return matrix


def task3(matrix):
    flat_list = [element for row in matrix for element in row]

    min_val = min(flat_list)
    max_val = max(flat_list)

    ind_min = flat_list.index(min_val)
    ind_max = flat_list.index(max_val)

    start_index = min(ind_min, ind_max)
    end_index = max(ind_min, ind_max)

    elements_between = flat_list[start_index + 1: end_index]
    result_sum = sum(elements_between)

    return result_sum

print()
for row in task1(matrix):
    print(row)

print()
for row in task2(matrix):
    print(row)