import numpy as np

""" file = open('01/test','r') """
file = open('01/data','r')
line = file.read()
instructions = line.split(', ')


def getPosition(instructions):
    position = np.array([0,0])
    heading =  np.array([0,1])
    rightTurn = np.array([[0,-1],[1,0]])
    visited = set(position)

    for i in instructions:
        heading = np.dot(heading, rightTurn)
        if i[0] == 'L':
            heading *= -1
        for _ in range(int(i[1:])):
            position += heading
            if tuple(position) in visited:
                return position
            else:
                visited.add(tuple(position))

position = getPosition(instructions)
taxiDistance = np.abs(position[0]) + np.abs(position[1])
print(taxiDistance)

