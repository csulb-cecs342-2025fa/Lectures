open System

// A "higher order function" is one that uses another function as one of its parameters.
// What's the use? Like all functions, it's about abstracting patterns to make code more robust and reusable.

// Find the first even integer in a list.
let rec findFirstEven coll =
    match coll with 
    | []                    -> failwith "can't find an integer from an empty list"
    | h :: _ when h % 2 = 0 -> h
    | _ :: t                -> findFirstEven t

[1; 3; 5; 6; 7; 8]
|> findFirstEven
|> printfn "first even: %d"

// int list -> int

// How about... findFirstOdd?
let rec findFirstOdd coll =
    match coll with 
    | []                    -> failwith "can't find an integer from an empty list"
    | h :: _ when h % 2 = 1 -> h
    | _ :: t                -> findFirstOdd t


// How about... findFirstVowel?
let rec findFirstVowel coll =
    match coll with 
    | []                    -> failwith "can't find a character from an empty list"
    | h :: _ when h = 'a' || h = 'e' || h = 'i' || h = 'o' || h = 'u' -> h
    | _ :: t                -> findFirstVowel t


// What can we abstract about these functions?
// They each iterate through a list of SOME TYPE; test the values of that type under a CONDITION; 
// and return a value of that type.
// A CONDITION is really just a FUNCTION that returns boolean (a "predicate").
let rec findFirst pred coll =
    match coll with
    | []                 -> failwith "can't find the first in an empty list"
    | h :: _ when pred h -> h
    | _ :: t             -> findFirst pred t

// ('a->bool)->'a list->'a

// Your first higher-order function!


findFirst (fun x -> x % 2 = 0) [1; 3; 5; 6; 7; 8; 10; 11]

findFirst (fun x -> x % 2 = 1) [1; 3; 5; 6; 7; 8; 10; 11]










// Now we can recreate our very-specific functions (and more!) with a higher order function.
[4; 2; 6; 5; 3; 4; 1]
|> findFirst (fun i -> i % 2 = 1)
|> printfn "First odd: %d"

[4; 2; 6; 5; 3; 4; 1]
|> findFirst (fun i -> i % 2 = 0)
|> printfn "First even: %d"

['n'; 'e'; 'a'; 'l']
|> findFirst (fun c -> c = 'a' || c = 'e' || c = 'i' || c = 'o' || c = 'u')
|> printfn "First vowel: %O"

["Neal"; "Anthony"; "Mehrdad"; "Claus"; "Jaclyn"]
|> findFirst (fun s -> String.length s > 5)
|> printfn "First long name: %s"


// ANY time we have a list of values and want to know the first value that satisfies some condition...
// we can use findFirst!
// (Really we would use List.find, which is this function built into F#)
[("Victor", 3.3); ("Ada", 3.8); ("Barry", 2.9)]
|> List.find (fun (n, gpa) -> gpa >= 3.5) // (n, gpa) means the param is a tuple with 2 values
|> printfn "First dean's list student: %O"





// Now we study 3 very important and famous higher-order functions; the foundation of functional programming.

// 1. Filter: given a predicate and a list of values, return a new list of only those values 
// that pass the predicate.

List.filter (fun s -> String.length s > 5) ["Neal"; "Anthony"; "Mehrdad"; "Claus"; "Jaclyn"]
|> printfn "All long names: %O"

// type of List.filter
// ('a->bool) -> 'a list-> 'a list
// 













// What type is List.filter?
// ('a->bool) -> 'a list -> 'a list



// Can we write it?
// To filter a list, we test the head. If it passes the predicate, we keep it, and follow it
// with the filter of the tail.
let rec filter pred coll =
    match coll with
    | [] -> []
    | h :: t when pred h -> h :: filter pred t
    | _ :: t             -> filter pred t














// Can we write it with TAIL recursion? 
let filterTail pred coll =
    let rec filterTailImpl pred coll acc = // acc = the result of filtering the items before this point.
        match coll with
        | []                 -> List.rev acc
        // problem: how to combine h with acc?
        | h :: t when pred h -> filterTailImpl pred t (h :: acc) // uh oh... look at the result of this...
        | _ :: t             -> filterTailImpl pred t acc

    filterTailImpl pred coll []



["Neal"; "Anthony"; "Mehrdad"; "Claus"; "Jaclyn"]
|> filterTail (fun s -> String.length s > 5)
|> printfn "All long names???: %A"


// 2. Map: given a transform function and a list of values, pass each value from the list to the transform,
// and put the results of those transforms into a new list.
["Neal"; "Anthony"; "Mehrdad"; "Claus"; "Jaclyn"]
|> List.map (fun s -> s.[0]) // transform the string to its first letter
|> printfn "All first initials: %A"

// What type is List.map?
// ('a->'b) -> 'a list -> 'b list


// Recursive implementation.
let rec map transform coll =
    match coll with 
    | [] -> []
    | h :: t -> (transform h) :: (map transform t)


// As a tail recursive function:
let mapTail transform coll =
    let rec mapTailImpl transform coll acc =
        match coll with 
        | []     -> List.rev acc
        | h :: t -> mapTailImpl transform t (transform h :: acc)
    mapTailImpl transform coll []


// 3a. Reduce: given an aggregate function and a list of values, apply the aggregate to the first
// two values in the list, then to that result and the next value, then to that result and the next value,
// ... until the list is reduced to a single aggregated value.
["Neal"; "Anthony"; "Mehrdad"; "Claus"; "Jaclyn"]
|> List.reduce (+)
|> printfn "Names, concatenated: %A"

// What type is reduce?


// Tail recursive implementation.
let reduce aggregate coll =
    let rec reduceImpl aggregate coll acc =
        match coll with
        | [] -> acc
        | h :: t -> reduceImpl aggregate t (aggregate acc h)

    if List.isEmpty coll then
        failwith "Cannot reduce an empty list"
    else
        reduceImpl aggregate (List.tail coll) (List.head coll)



// 3b. Fold: given a combiner function 'a->'b->'a, an initial value of 'a, and a list of 'b,
// call the combiner on the starting value and the first element of the list; then again to that
// result and the next value, ..., until the list is folded into a single value of type 'a.
let fold' combiner initial coll =
    let rec foldImpl combiner coll acc =
        match coll with 
        | [] -> acc
        | h :: t -> foldImpl combiner t (combiner acc h)

    foldImpl combiner coll initial

// This function is sometimes called "foldl" [fold left] because we start at the left (first element). 
// Another implementation, foldr, starts at the right:
let rec foldr combiner initial coll =
    match coll with 
    | [] -> initial
    | h :: t-> combiner (foldr combiner initial t) h


// There are LOTS of fun fold tricks, but I can't show them all to you...
let reduce' aggregate coll =
    List.fold aggregate (List.head coll) (List.tail coll) 

let reverse' coll =
    List.fold (fun t h -> h :: t) [] coll

// :D



