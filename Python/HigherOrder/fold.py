from typing import *

# An iterative implementation of fold in Python.
# Given a starting state of type S, a list of T, and a function to combine (combine) 
# an S and a T to make a new S; we combine the starting S with the first element of the list,
# then repeatedly combine the new S with the next element of the list.
def fold[T, S](combiner : Callable[[S, T], S], initial : S, values : list[T]) -> S:
    accumulator = combiner(initial, values[0])
    for i in range(1, len(values)):
        accumulator = combiner(accumulator, values[i])
    return accumulator

# reduce can be rewritten with fold, where the initial state is the first element of the list.
def reduce[T](aggregate : Callable[[T, T], T], values : list[T]) -> T:
    fold(aggregate, values[0], values[1:])

if __name__ == "__main__":
    nums = [1, 2, 3, 4, 5]
    print(fold(lambda l, h: [h] + l, [], nums))