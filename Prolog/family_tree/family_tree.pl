parent(tom, bob).
parent(bob, pat).
parent(bob, ada).
parent(pat, jim).
parent(ada, michelle).

male(tom).
male(bob).
male(jim).
male(pat).
female(ada).
female(michelle).

sibling(X, Y) :- parent(Z, X), parent(Z, Y), X \= Y.
brother(X, Y) :- male(X), sibling(X, Y).

% X is an ancestor of Y.
ancestor(X, Y) :- parent(X, Y).
ancestor(X, Y) :- parent(X, Z), ancestor(Z, Y).

% X is the aunt of Y.
aunt(X, Y) :- female(X), parent(P, Y), sibling(P, X).

% X is the cousin of Y.
cousin(X, Y) :- parent(P1, X), parent(P2, Y), sibling(P1, P2).
