from functools import reduce
import numpy as np
from copy import deepcopy

f = open('18/18.txt', 'r')
lines = f.read().split('\n')
snailfish_numbers = list(map(eval, lines))

def simple_add_inner(sfn, i, val):
    if len(i) == 0:
        return
    if type(sfn[i[0]]) is int:
        sfn[i[0]] += val
    else:
        simple_add_inner(sfn[i[0]], i[1:], val)


def simple_add(sfn, i, fst, snd):
    if i != [0,0,0,0]:
        temp = int(''.join(list(map(str, i))), 2)
        temp -= 1
        temp = bin(temp)[2:].zfill(4)
        nxt = list(map(int, temp)) + [1]
        simple_add_inner(sfn, nxt, fst)
    if i != [1,1,1,1]:
        temp = int(''.join(list(map(str, i))), 2)
        temp += 1
        temp = bin(temp)[2:].zfill(4)
        prv = list(map(int, temp)) + [0]
        simple_add_inner(sfn, prv, snd)
    

def explode_inner(root_sfn, sfn, i):
    found = False
    if type(sfn) is list:
        if len(i) > 3:
            simple_add(root_sfn, i, sfn[0], sfn[1])
            root_sfn[i[0]][i[1]][i[2]][i[3]] = 0
            found =  True
        else:
            for s in range(2):
                if type(sfn[s]) is list:
                    found |= explode_inner(root_sfn, sfn[s], i.copy() + [s])
    return found
            
def explode(sfn):
    return explode_inner(sfn, sfn, [])


def split(sfn):
    found = False
    for i in range(2):
        if not found:
            if type(sfn[i]) is int:
                if sfn[i] >= 10:
                    sfn[i] = [int(np.floor(sfn[i]/2.0)), int(np.ceil(sfn[i]/2.0))]
                    found = True
            else: 
                found |= split(sfn[i])
    return found

def add(first, second):
    s = [first, second]
    # print('  ', first)
    # print('+ ', second)
    while explode(s) or split(s):
        continue

    # print('= ', s, '\n')
    return s

def magnitude(sfn):
    fst = sfn[0] if type(sfn[0]) is int else magnitude(sfn[0])
    snd = sfn[1] if type(sfn[1]) is int else magnitude(sfn[1])
    return 3 * fst + 2 * snd

# snailfish_sum = reduce(add, snailfish_numbers) 

# # print(snailfish_sum)
# print(magnitude(snailfish_sum))

f1 = eval('[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]')
s1 = eval('[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]')
# [print(n if n == f1 or n == s1 else ' ') for n in snailfish_numbers]
magnitudes = []
for i in range(len(snailfish_numbers)):
    for j in range(len(snailfish_numbers)):
        if i != j:
            f = deepcopy(snailfish_numbers[i])
            s = deepcopy(snailfish_numbers[j])
            m = magnitude(add(f,s))
            magnitudes.append(m)

            
# [print(n) for n in snailfish_numbers]

# test = add(snailfish_numbers[-2],snailfish_numbers[0])
# test = add(f,s)
# print(test)
# print(magnitude(test))
print(max(magnitudes))