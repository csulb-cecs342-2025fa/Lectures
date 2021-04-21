% Check if a list contains the given value.
contains([X|_], X).
contains([_|T], X) :- contains(T, X).

% Check if the given list has the given length.
mylength([], 0).
mylength([_|T], L) :- mylength(T, S), L is S + 1.

% Check if the sum of the elements of a list matches the given value.
% int list -> int
sum([], 0).
sum([H|T], S) :- sum(T, D), S is D + H.




















% mylength is not tail-recursive, so let's try making it so.
mylength2([H|T], L) :- mylength2([H|T], L, 0). % the last param will accumulate.
mylength2([], L, Acc) :- L = Acc.
mylength2([_|T], L, A) :- S is A + 1, mylength2(T, L, S).





sum2([H|T], S) :- sum2([H|T], S, 0).
sum2([], S, S).
sum2([H|T], Sum, Acc) :- N is Acc + H, sum2(T, Sum, N).

% append
append([], L, L).
append([H|T], L, [H|Result]) :- append(T, L, Result).

evens([], []).
evens([H|T], [H|E]) :- H rem 2 =:= 0, evens(T, E).
evens([_|T], E) :- evens(T, E).

% ordered: if the list is in nondecreasing order.
ordered([_]). % list of 1 element is ordered.
ordered([X,Y|T]) :- X =< Y, ordered([Y|T]). % list of at least 2 elements is ordered if the first is <= second, and rest is ordered.


% quicksort: if the given list, when sorted, is equal to S.
quicksort([X|Xs], S) :-
	% Quicksort: first, partition the list into the Littles (< pivot) and Bigs (> pivot).
	% Naive QS: let the first element X be the pivot.
	partition(Xs, X, Littles, Bigs),

	% Sort the littles.
	quicksort(Littles, Ls),
	% Sort the bigs.
	quicksort(Bigs, Bs),
	% The final sorted form of the list goes:
	% (SortedLittles), X, (SortedBigs).
	append(Ls, [X|Bs], S).
quicksort([], []).

% partition: if the given list, when partitioned around Y, results in
% one list of elements smaller than Y, and one list of elements larger
% than Y.
% First parameter: the list to partition.
% Second: the pivot.
% Third: the list of values smaller than the pivot.
% Fourth: the list of values larger than the pivot.
partition([], _, [], []).

% The first element belongs in Littles if it is <= the pivot.
partition([X|Xs], Y, [X|Ls], Bs) :- X =< Y, partition(Xs, Y, Ls, Bs).

% The first element belongs in Bigs if it is > the pivot.
partition([X|Xs], Y, Ls, [X|Bs]) :- X > Y, partition(Xs, Y, Ls, Bs).
