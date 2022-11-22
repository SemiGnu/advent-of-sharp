import numpy as np
from collections import defaultdict
import itertools

x_range = range(143,178)
y_range = range(-106, -70)

# x_range = range(20,31)
# y_range = range(-10, -4)

x_steps = {}

def get_x_steps(velocity):
    steps = []
    pos = 0
    step = 0
    while pos <= x_range[-1] and velocity > 1:
        step += 1
        pos += velocity
        velocity -= 1
        if x_range[0] <= pos <= x_range[-1]:
            steps.append(step)
    return steps

for vel in range(0, x_range[-1] + 1):
    steps = get_x_steps(vel)
    if len(steps) > 0:
        x_steps[vel] = steps

y_steps= {}

def get_y_steps(velocity):
    steps = []
    pos = 0
    step = 0
    while pos >= y_range[0]:
        step += 1
        pos += velocity
        velocity -= 1
        if y_range[0] <= pos <= y_range[-1]:
            steps.append(step)
    return steps

    
for vel in range(y_range[0], -y_range[0] + 1):
    steps = get_y_steps(vel)
    if len(steps) > 0:
        y_steps[vel] = steps

x_stop_max = int(np.floor((-1+np.sqrt(1+8*x_range[-1]))/2))
x_stop_min = int(np.ceil((-1+np.sqrt(1+8*x_range[0]))/2))

sinks = range(x_stop_min, x_stop_max + 1)

max_steps = y_range[0] * -2

steps = {}
for i in range(1, max_steps +1):
    steps[i] = ([],[])

for x, xs in x_steps.items():
    for step in xs:
        steps[step][0].append(x)
for y, ys in y_steps.items():
    for step in ys:
        steps[step][1].append(y)


vel_pairs = []
for ss, (xs, ys) in steps.items():
    vel_pairs += itertools.product(xs, ys)
    if ss >= x_stop_min:
        vel_pairs += itertools.product(sinks, ys)

vel_pairs = list(set(vel_pairs))
print(len(vel_pairs))
