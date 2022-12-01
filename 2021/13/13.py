from itertools import takewhile, dropwhile
from math import ceil

f = open("13/13.txt", "r")
lines = f.read().split("\n")
array = list(map(lambda l: tuple(map(int,l.split(","))), list(takewhile(lambda x: x != "", lines))))
instructions = list(map(lambda l: (l[11], int(l[13:])), list(dropwhile(lambda x: len(x) == 0 or x[0] != 'f', lines))))

def get_size(array):
    w = max(list(map(lambda x: x[0], array)))
    h = max(list(map(lambda x: x[1], array)))
    return (w,h)

def perform(instruciton, array):
    if instruciton[0] == 'y':
        return fold_up(instruciton[1], array)
    else:
        return fold_left(instruciton[1], array)

def fold_up(row, array):
    h = max(list(map(lambda x: x[1], array)))
    h = max(row, ceil(h / 2.0))
    new_array = [] 
    for (x,y) in array:
        if y <= row:
            new_array.append((x, h-(row - y)))
        else:
            new_array.append((x, h-(y - row)))
    return list(set(new_array))

def fold_left(column, array):
    w = max(list(map(lambda x: x[0], array)))
    w = max(column, ceil(w / 2.0))
    new_array = [] 
    for (x,y) in array:
        if x <= column:
            new_array.append((w-(column - x), y))
        else:
            new_array.append((w-(x - column), y))
    return list(set(new_array))

def print_array(array):
    (w,h) = get_size(array)
    matrix = [['.' for _ in range(w+1)] for _ in range(h+1)] 
    for (x, y) in array:
        matrix[y][x] = '#'
    for line in matrix:
        print(''.join(line))
    print("")

for i in instructions:
    array = perform(i, array)
    
print_array(array)
print(len(array))