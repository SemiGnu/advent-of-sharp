# import matplotlib.pyplot as plt
# plt.plot([1, 2, 3, 4])
# plt.ylabel('some numbers')
# plt.show()

from heapq import heappop, heappush

heap = []
heappush(heap, 23)
heappush(heap, 8)
heappush(heap, 253)
heappush(heap, 28)

print(heappop(heap))
print(heappop(heap))
print(heappop(heap))
print(heappop(heap))