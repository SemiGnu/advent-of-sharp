import numpy as np

x_range = range(143,178)
y_range = range(-106, -70)

x_range = range(20,31)
y_range = range(-10, -4)

x_steps = [[] for _ in range(x_range[-1] + 1)]

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

for vel in range(x_range[-1], 0, -1):
    x_steps[vel] = get_x_steps(vel)


max_steps = max([steps for velocity in x_steps for steps in velocity])

valid_steps = list(set([steps for velocity in x_steps for steps in velocity]))

print(max_steps)


# x_max_steps = max([max(x + [0]) for x in x_steps])
# # x_max_steps = max([(lambda x = x: x[-1] if len(x) > 0 else 0) for x in x_steps])

# x_steps_t = [[] for _ in range(x_max_steps + 1)]
# for vel in range(len(x_steps)):
#     for step in x_steps[vel]: 
#         x_steps_t[step].append(vel)

