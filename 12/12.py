f = open("12/12.txt", "r")
lines = f.read().split("\n")
array = list(map(lambda l: l.split("-"), lines))

caves = {}
for c in array:
    if c[0] in caves and c[1] != "start":
        caves[c[0]].append(c[1])
    elif c[1] != "start":
        caves[c[0]] = [c[1]]
    if c[1] in caves and c[0] != "start":
        caves[c[1]].append(c[0])
    elif c[0] != "start":
        caves[c[1]] = [c[0]]

small_caves = list(filter(lambda x: x.islower() and x != "start" and x != "end", caves.keys()))

def path(node, current_path, paths):
    current_path.append(node)
    if (node == "end"):
        paths.append(current_path)
        return
    for cave in caves[node]:
        visits = len(list(filter(lambda x, c=cave: x == c, current_path)))
        dupes = list(map(lambda x: len([1 for cp in current_path if cp == x]) < 2, small_caves))
        curious = all(dupes)
        is_valid = cave.isupper() or visits == 0 or curious
        if is_valid:
            path(cave, current_path.copy(), paths)

paths = []
path("start", [], paths)
print(len(paths))