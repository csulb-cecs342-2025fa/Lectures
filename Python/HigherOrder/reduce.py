from typing import *

# An iterative implementation of reduce in Python.
# Given a list of T, and a function to aggregate (combine) two Ts into a new T,
# aggregate the first two elements of the list, then aggregate each other element with
# the previous aggregate result.
def reduce[T](aggregate : Callable[[T, T], T], values : list[T]) -> T:
    accumulator = aggregate(values[0], values[1])
    for i in range(2, len(values)):
        accumulator = aggregate(accumulator, values[i])
    return accumulator

if __name__ == "__main__":
    nums = [1, 2, 3, 4, 5]
    print(reduce(lambda x, y: x + y, nums))