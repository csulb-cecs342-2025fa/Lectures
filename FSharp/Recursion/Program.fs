// Tail recursion.

// A classic recursive function: factorial(n) = n * factorial(n-1)
let rec factorial n =
    match n with 
    | 1 -> 1
    | _ -> n * factorial (n - 1)



// Tail-recursive implementation of factorial.
// The typical tail recursive function introduces another parameter to the recursion.
// We don't want to bother other programmers with this knowledge, so we "hide" the parameter
// an inner function that we start ourselves.
let factorialTail n =
    // The parameter we introduce is called an "accumulator". It represents "the answer to the question prior to this iteration.";
    // AKA, "what have we already concluded about the values that came before?"
    // In factorial, acc is the product of all the numbers that came "before" n in the sequence
    let rec factorialTail' n acc =
        match n with 
        // If n == 1, then acc is the product of all the numbers that came before 1, so acc is the actual answer.
        | 1 -> acc
        // Otherwise, multiply n by acc to form the new acc, and do a tail call on the next number in the sequence.
        | _ -> factorialTail' (n - 1) (n * acc)

    // With the inner function declared, the outer function then "kicks off" the recursion
    // by providing a value for the accumulator that is the answer to the base case.
    // When n = 1, factorial n = 1, so the accumulator starts at 1.
    factorialTail' n 1




// sumList: return the sum of the values in a list.
let rec sumList coll =
    match coll with 
    | [] -> 0
    | h :: t -> h + sumList t
// what type is h? what type is t?
// what is the type of sumList?

// int list -> int


// Is this function tail recursive?


// sumListTail: a tail-recursive implementation.

let sumListTail coll =
    // acc is "the sum of the elements that we already iterated over."
    let rec sumListTail' coll acc =
        match coll with 
        // If we've reached the end of the list, then acc holds the sum of all elements that came before the end.
        | [] -> acc
        // Otherwise, the sum of prior elements plus the current head becomes the new sum of everything before the tail.
        | h :: t -> sumListTail' t (h + acc)
                 
    // The sum of the empty list is 0.    
    sumListTail' coll 0


// Exercises: implement tail recursive versions of these functions.

// Count the number of instances of x in coll.
let rec count x coll =
    match coll with
    | []                -> 0
    | h :: t when h = x -> 1 + count x t
    | _ :: t            -> count x t

let countTail x coll =
    // acc is the number of occurrences of x found earlier in the list.
    let rec countTail' x coll acc =
        match coll with 
        // acc is *all* occurrences of x earlier in the list.
        | [] -> acc
        // if h equals x, then (acc + 1) is the number of occurrences of x that came before the tail.
        | h :: t when h = x -> countTail' x t (acc + 1)
        // otherwise, acc is the number of occurrences of x that came before the tail.
        | _ :: t            -> countTail' x t acc

    // The empty list has 0 occurrences of x.
    countTail' x coll 0

// Determine the index of the first occurrence of x.
let rec indexOf x coll =
    match coll with 
    | [] -> -1
    | h :: _ when h = x -> 0
    | _ :: t -> let r = indexOf x t
                if r = -1 then -1
                else r + 1

let indexOfTail x coll =
    
    // acc represents...
    let indexOfTail' x coll acc = 
        0

    0

// Harder challenge: 
// Reverse a list.
let rec reverse coll =
    match coll with 
    | [] -> []
    | [x] -> [x]
    | h :: t -> reverse t @ [h] // @ is "append"

let reverseTail coll =
    // acc represents the reverse of the list that came before.
    let rec reverseTail' coll acc = 
        match coll with 
        // acc is the reverse of the entire list.
        | [] -> acc
        // h must come immediately *before* the reverse of the list that came before it;
        // so h followed by acc is the new "reverse of the list that came before t".
        | h :: t -> let newAcc = h :: acc
                    reverseTail' t newAcc

    reverseTail' coll []

// Hardest yet:
// Find all the even integers in a list.
let rec findEvens coll =
    match coll with 
    | [] -> []
    | h :: t when h % 2 = 0 -> h :: findEvens t
    | _ :: t -> findEvens t


let mapTail transform coll =
    let rec mapTail' transform coll acc =
        match coll with 
        | [] -> acc
        | h :: t -> let newHead = transform h
                    mapTail' transform t (acc @ [newHead])

    mapTail' transform coll []
