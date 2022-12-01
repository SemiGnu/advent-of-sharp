from functools import reduce

def line_to_array(line) :
    nums = []
    for c in line:
        nums.append(int(c))
    return nums

f = open("11.txt", "r")
lines = f.read().split("\n")
array = list(map(line_to_array, lines))

def adjacent(y, x):
    return [
        (y-1, x-1),
        (y-1, x),
        (y-1, x+1),
        (y, x-1),
        (y, x+1),
        (y+1, x-1),
        (y+1, x),
        (y+1, x+1),
        ]

def flash(y, x, array, skip):
    if y < 0 or y >= len(array) or x < 0 or x >= len(array[0]):
        return 0
    if not skip:
        array[y][x] += 1
    flashes = 0
    if array[y][x] == 10:
        array[y][x] += 1
        flashes += 1
        for adj in adjacent(y,x):
            flashes += flash(adj[0], adj[1], array, skip)
    return flashes

def increment(array):
    for y in range(len(array)):
        for x in range(len(array[y])):
            array[y][x] += 1

def reset(array):
    resets = 0
    for y in range(len(array)):
        for x in range(len(array[y])):
            if array[y][x] > 9:
                array[y][x] = 0
                resets += 1
    return resets

                
def step(array):
    flashes = 0
    skip = False
    last_flashes = 1
    while last_flashes > 0:
        last_flashes = 0
        for y in range(len(array)):
            for x in range(len(array[y])):
                last_flashes += flash(x,y,array, skip)
        flashes += last_flashes
        skip = True
    resets = reset(array)
    return resets
    # return flashes
                

# steps = 100
# flashes = 0
# for i in range(steps):
#     flashes += step(array)
# print(flashes)

steps = 1
while (step(array) != 100):
    steps += 1

print(steps)
