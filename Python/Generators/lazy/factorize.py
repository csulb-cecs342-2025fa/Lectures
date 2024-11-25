from collections.abc import Iterable, Iterator

# A FactorGenerator generates a sequence of the integer factors (divisors) of a given integer.
class FactorGenerator(Iterable[int]):
    def __init__(self, value : int):
        self._value = value

    def __iter__(self) -> Iterator[int]:
        return FactorGenerator.FactorIterator(self._value)
    
    # The RangeIterator does the work to "walk" the sequence. It generates one number
    # from the sequence for each call to __next__, and raises StopIteration when the sequence
    # is exhausted.
    class FactorIterator(Iterator[int]):
        def __init__(self, value : int):
            self._value = value
            self._current = 0

        def __next__(self) -> int:
            if self._current >= self._value:
                raise StopIteration
            
            self._current += 1
            while self._value % self._current != 0:
                self._current += 1    

            print(f"Factored: {self._current}")
            return self._current
