import numpy as np
from itertools import product

input = """-1,-1,1
-2,-2,2
-3,-3,3
-2,-3,1
5,6,-4
8,0,7"""

rot1 = np.array([[0,0,-1],[0,1,0],[1,0,0]])
rot2 = np.array([[1,0,0],[0,0,-1],[0,1,0]])
rot3 = np.array([[0,-1,0],[1,0,0],[0,0,1]])
rots = []
for x,y,z in product(range(4), range(4), range(4)):
    rot = rot1^x * rot2^y * rot3^z
    rots.append(rot)

rots = list(set(rots))
print('rots: ', len(rots))

mx0 = np.array(list(map(lambda x: eval(f'[{x}]'), input.split('\n'))))

print(mx0)
print(rot1)
print(np.multiply(rot1,mx0[-1]))
print(rot1 ^ mx0[-1])


