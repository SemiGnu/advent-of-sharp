import numpy as np
from collections import defaultdict


def line_to_array(line) :
    nums = []
    for c in line:
        nums.append(int(c))
    return nums

f = open("09/09t.txt", "r")
lines = f.read().split("\n")
array = np.array(list(map(line_to_array, lines)))

basin_map = {}

(h, w) = array.shape

basin_number = 0


def adjacent(cell):
    (y, x) = cell
    adj = [
        (y-1, x),
        (y, x-1),
        (y, x+1),
        (y+1, x),
        ]
    return list(filter(lambda c: 0 <= c[0] < h and 0 <= c[1] < w, adj))

def fill_basin(cell):
    val = array[cell]
    if array[cell] == 9 or cell in basin_map:
        return
    basin_map[cell] = basin_number
    for adj in adjacent(cell):
        fill_basin(adj)

for y in range(h):
    for x in range(w):
        if array[x,y] < 9 and (x,y) not in basin_map:
            fill_basin((x,y))
            basin_number += 1

tr = defaultdict(list)
for k, v in basin_map.items():
    tr[v].append(k)

print(tr)