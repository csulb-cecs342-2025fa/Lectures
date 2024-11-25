from collections.abc import Iterable, Iterator

# A RangeGenerator generates a sequence of integers given a start, inclusive end, and increment.
class RangeGenerator(Iterable[int]):
    def __init__(self, start : int, end : int, increment : int):
        self._start = start
        self._end = end
        self._increment = increment

    def __iter__(self) -> Iterator[int]:
        return RangeGenerator.RangeIterator(self._start, self._end, self._increment)
    
    # The RangeIterator does the work to "walk" the sequence. It generates one number
    # from the sequence for each call to __next__, and raises StopIteration when the sequence
    # is exhausted.
    class RangeIterator(Iterator[int]):
        def __init__(self, start : int, end : int, increment : int):
            self._start = start
            self._end = end
            self._increment = increment

            self._current = start - increment

        def __next__(self) -> int:
            if self._current >= self._end:
                raise StopIteration

            self._current += self._increment
            print(f"Generate: {self._current}")
            
            return self._current
        