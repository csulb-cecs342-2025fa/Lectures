from .range import *
from .take import *
from .map import *
from .factorize import *

# This list controls what functions are imported when someone writes "from lazy import *"
# We use it to "hide" the Generator classes. You'll need to update this list as you write more functions.
__all__ = ["range", "factorize", "take", "map"]


# Helper functions to construct Generators by calling functions with friendly names.
def range(start : int, end : int, incr : int) -> RangeGenerator:
    return RangeGenerator(start, end, incr)

def factorize(value : int) -> FactorGenerator:
    return FactorGenerator(value)

# Any function that does a lazy operation on an existing sequence always takes that sequence
# as the final parameter, just to be consistent.
def take[T](count : int, source : Iterable[T]) -> TakeGenerator[T]:
    return TakeGenerator(count, source)

def map[TData, TResult](transform : Callable[[TData], TResult], source : Iterable[TData]) -> MapGenerator[TData, TResult]:
    return MapGenerator(transform, source)

