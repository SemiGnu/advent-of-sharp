from matplotlib import pyplot as plt
from heapq import heappop, heappush

def line_to_array(line) :
    nums = []
    for c in line:
        nums.append(int(c))
    return nums

f = open("15/15.txt", "r")
lines = f.read().split("\n")
array = list(map(line_to_array, lines))

w = len(array[0])
h = len(array)

draw_array = list(map(lambda y: list(map(lambda x: [25*x, 25*(10-x), 50], y)),array))
plt.imshow(draw_array, interpolation='nearest')
plt.show()

def adjacent(cell):
    (y, x) = cell
    adj = [
        (y-1, x),
        (y, x-1),
        (y, x+1),
        (y+1, x),
        ]
    return list(filter(lambda c: c not in visited and 0 <= c[0] < w and 0 <= c[1] < h, adj))
    
def guess(x,y):
    return (w - 1 - x) + (h - 1 - y)

open = []
heappush(open, (0, (0,0)))

visited = {(0,0): (0,0)}

while len(open) > 0:
    current = heappop(open)
    if current[1] == (w-1, h-1):
        break
    for adj in adjacent(current[1]):
        (x,y) = adj
        g = array[y][x] + guess(x,y)
        heappush(open, (g, adj))
        visited[adj] = current[1]

path = [(w-1, h-1)]
while path[-1] != (0,0):
    path.append(visited[path[-1]])

path = list(reversed(path))

path_value = 0

for p in path:
    path_value += array[p[1]][p[0]]
    array[p[1]][p[0]] = '#'

for y in range(h):
    s = ''
    for x in range(w):
        s += str(array[y][x])
    print(s)

print(path_value)

