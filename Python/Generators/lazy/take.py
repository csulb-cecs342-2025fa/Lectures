from collections.abc import Iterable, Iterator

# A TakeGenerator generates only the first N elements of a source sequence. 
class TakeGenerator[T](Iterable[T]):
    # You provide a count, and a sequence of T.
    def __init__(self, count : int, sequence : Iterable[T]):
        self._count = count
        self._source = sequence

    def __iter__(self) -> Iterator[T]:
        return TakeGenerator.TakeIterator(self._count, iter(self._source))
    
    # The TakeIterator returns the next element from the source sequence, but
    # only until we've returned as many items as we wanted to take.
    class TakeIterator[T](Iterator[T]):
        def __init__(self, count : int, iterator : Iterator[T]):
            self._iterator = iterator
            self._taken = 0
            self._count = count

        def __next__(self) -> T:
            if self._taken >= self._count:
                raise StopIteration

            self._taken += 1
            print(f"Take {self._taken}:")
            n = next(self._iterator)
            print(f"Took: {n}")
            return n