from typing import *
import math

# An iterative implementation of map in Python.
# Given a sequence of TData, and a function to transform a TData into a TResult,
# apply the transform to each element in the sequence and return a sequence of the transformed values.
def map[TData, TResult](transform : Callable[[TData], TResult], values : Iterable[TData]) -> Iterable[TResult]:
    result : list[TResult] = []
    for val in values:
        result.append(transform(val))
    return result

if __name__ == "__main__":
    nums = [1, 2, 3, 4, 5]
    print(map(math.sqrt, nums))
