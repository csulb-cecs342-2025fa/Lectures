isEven(X) :- mod(X, 2) is 0.
isNegative(X) :- X < 0.

max(X, Y, X) :- X >= Y.
max(X, Y, Y) :- X < Y.

absoluteValue(X, X) :- X >= 0.
absoluteValue(X, -X) :- X < 0.

squared(X, R) :- R is X * X.

annualInterest(Principal, Rate, NumYears, Balance) :-
    CompoundRate is (1 + Rate) ** NumYears,
    Balance is Principal * CompoundRate.


% Factorial of 0 is 1.
% Factorial of N is factorial of (N-1) times N.
factorial(0, 1).
factorial(N, R) :-
    N1 is N - 1,
    factorial(N1, R1),
    R is N * R1.

% A student is on the presidents list with a GPA >= 3.5.
isPresidentsList(student(ID, Major, Gpa)) :-
    Gpa >= 3.5.

% A student on the presidents list is very busy; as are 
% engineering students with a GPA >= 2.5.
isVeryBusy(student(ID, Major, Gpa)) :-
    isPresidentsList(student(ID, Major, Gpa)).

isVeryBusy(student(ID, Major, Gpa)) :-
    (Major = cs ; Major = ce; Major = ee; Major = me; Major = ae),
    Gpa >= 2.5.
