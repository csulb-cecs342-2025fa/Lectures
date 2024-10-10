from typing import *

def filter[T](pred : Callable[[T], bool], values : Iterable[T]) -> Iterable[T]:
    result : list[T] = []
    for val in values:
        if pred(val):
            result.append(val)
    return result

if __name__ == "__main__":
    nums = [1, 2, 3, 4, 5]
    print(filter(lambda x: x % 2 == 0, nums))