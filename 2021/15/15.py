import numpy as np
import matplotlib.pyplot as plt
import matplotlib as mpl
from matplotlib.colors import ListedColormap, LinearSegmentedColormap
from heapq import heappop, heappush

def line_to_array(line) :
    nums = []
    for c in line:
        nums.append(int(c))
    return nums

def line_to_array2(line, i) :
    nums = []
    for j in range(5):
        for c in line:
            n = (int(c) + i + j - 1) % 9 + 1
            nums.append(int(n))
    return nums

def lines_to_array2(ls):
    lines = []
    for i in range(5):
        lines += list(map(lambda l: line_to_array2(l, i), ls))
    return lines

f = open("15/15.txt", "r")
lines = f.read().split("\n")
array = np.array(list(map(line_to_array, lines)))
array = np.array(lines_to_array2(lines))

w = len(array[0])
h = len(array)

def adjacent(cell):
    (y, x) = cell
    adj = [
        (y-1, x),
        (y, x-1),
        (y, x+1),
        (y+1, x),
        ]
    return list(filter(lambda c: 0 <= c[0] < w and 0 <= c[1] < h, adj))
    # return list(filter(lambda c: c not in visited and 0 <= c[0] < w and 0 <= c[1] < h, adj))
    
def guess(cell):
    (y, x) = cell
    return (w - 1 - x) + (h - 1 - y)

open_set = []
heappush(open_set, (0, (0,0)))

came_from = {}

g_scores = {}
f_scores = {}
for y in range(len(array)):
    for x in range(len(array[0])):
        g_scores[y,x] = np.Inf
        f_scores[y,x] = np.Inf
g_scores[0,0] = 0
f_scores[0,0] = guess((0,0))


while len(open_set) > 0:
    (travelled,current) = heappop(open_set)
    if current == (h-1, w-1):
        break

    for adj in adjacent(current):
        tentative = g_scores[current] + array[adj]
        if tentative < g_scores[adj]:
            came_from[adj] = current
            g_scores[adj] = tentative
            f_scores[adj] = tentative + guess(adj)
            heappush(open_set, (f_scores[adj], adj))

array[0,0] = 0
path = [(h-1, w-1)]
while path[-1] != (0,0):
    path.append(came_from[path[-1]])

# path = list(reversed(path))

path_value = 0

for p in path:
    path_value += array[p]

print(array)
print(path_value)

cmap1 = LinearSegmentedColormap.from_list("gyr", ["lime","yellow","red"])
plt.imshow(array,cmap=cmap1)
plt.plot(*np.array(list(map(lambda x: list(reversed(x)),path))).T)
plt.show()
