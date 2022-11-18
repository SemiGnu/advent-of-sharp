from re import findall

f = open('14/14.txt', 'r')
lines = f.read().split('\n')
polymer = lines[0]
insertion_rules = {}
polymer_pairs = {}
element_count = [0 for _ in range(26)]

for line in lines[2:]:
    pair=line[0:2]
    insertion_rules[pair] = line[-1]
    polymer_pairs[pair] = len(findall(f'(?={pair})', polymer))

def step(polymer_pairs):
    new_polymer_pairs = polymer_pairs.copy()
    for pair in polymer_pairs.keys():
        new_polymer_pairs[pair] = 0
    for pair in polymer_pairs.keys():
        new_polymer_pairs[pair[0] + insertion_rules[pair]] += polymer_pairs[pair]
        new_polymer_pairs[insertion_rules[pair] + pair[1]] += polymer_pairs[pair]
    return new_polymer_pairs

steps = 40

for _ in range(steps):
    polymer_pairs = step(polymer_pairs)

for pair in polymer_pairs:
    element_count[ord(pair[1])-65] += polymer_pairs[pair]

element_count[ord(polymer[0])-65] += 1

element_count.sort()
element_count = list(filter(lambda x: x > 0, element_count))

print(element_count[-1] - element_count[0])