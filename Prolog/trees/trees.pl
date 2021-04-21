% A tree is either empty, or is a node(Value, Left, Right).

isEmptyTree(empty).
isNonEmptyTree(node(_, _, _)).




inOrderPrint(empty).
inOrderPrint(node(Value, Left, Right)) :-
    inOrderPrint(Left),
    write(Value), write(' '),
    inOrderPrint(Right).

treeHeight(empty, -1).
treeHeight(node(_, Left, Right), H) :-
    treeHeight(Left, LeftHeight),
    treeHeight(Right, RightHeight),
    H is max(LeftHeight, RightHeight) + 1.

bstContains(node(X, _, _), X).
bstContains(node(Value, Left, _), X) :-
    Value > X,
    bstContains(Left, X).
bstContains(node(Value, _, Right), X) :-
    Value < X,
    bstContains(Right, X).