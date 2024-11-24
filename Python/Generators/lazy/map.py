from collections.abc import Callable, Iterable, Iterator

# A MapGenerator generates transformed values from a source sequence, using a given
# transform function.
class MapGenerator[TData, TResult](Iterable[TResult]):
    # You provide a function from TData->TResult, and a sequence of TData.
    def __init__(self, transform : Callable[[TData], TResult] , sequence : Iterable[TData]):
        self._transform = transform
        self._source = sequence

    def __iter__(self):
        return MapGenerator.MapIterator(self._transform, iter(self._source))

    # The MapIterator returns the next element from the source sequence, after it is
    # passed to the transform function.  
    class MapIterator[T]:
        def __init__(self, transform : Callable[[TData], TResult], iterator : Iterator[T]):
            self._transform = transform
            self._iterator = iterator

        def __next__(self):
            # Calling next() on our iterator will raise StopIteration if 
            # the source iterator is exhausted, stopping the map iteration correctly.
            print("Mapping:")
            n = next(self._iterator)
            t = self._transform(n)
            print(f"Mapped: {n} -> {t}")
            return t

