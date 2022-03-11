// Tail recursion.

// sumList: return the sum of the values in a list.
let rec sumList coll =
    match coll with 
    | [] -> 0
    | h :: t -> h + sumList t
// what type is h? what type is t?
// what is the type of sumList?

// Is this function tail recursive?


// sumListTail: a tail-recursive implementation.
// The typical tail recursive function introduces another parameter to the recursion.
// We don't want to bother other programmers with this knowledge, so we "hide" the parameter
// an inner function that we start ourselves.

let sumListTail coll =
    // The parameter we introduce is called an "accumulator". It represents "the answer to the question prior to this iteration.";
    // AKA, "what have we already concluded about the values that came before?"
    // In this case, acc is "the sum of the elements that we already iterated over."
    let rec sumListTail' coll acc =
        match coll with 
        | [] -> acc   // if we've already iterated over the entire list, then the final sum of the list
                      // is equal to the accumulator, which is "the sum of the elements that we already iterated over".
        | h :: t -> sumListTail' t (h + acc)
                      // otherwise, we recurse to the next iteration.
                      // we add h + acc to find the sum of the elements up to and including the current one.
                      // that becomes the new accumulator to use when summing the tail of the list.
                    
    // With the inner function declared, the outer function then "kicks off" the recursion
    // by providing a value for the accumulator that represents "the answer to the question when no work has been done."
    // If we haven't examined any values in the list, the answer to "sumList" is 0.
    sumListTail' coll 0


// Exercises: implement tail recursive versions of these functions.

// Count the number of instances of x in coll.
let rec count x coll =
    match coll with
    | []                -> 0
    | h :: t when h = x -> 1 + count x t
    | _ :: t            -> count x t

let countTail x coll =
    
    // Start by defining the accumulator: acc represents...
    let countTail' x coll acc =
        0
        // Then match coll and decide what to do...

    // Then initialize the recursion with the correct starting accumulator.
    0

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
    | h :: t -> reverse t @ [h]


let reverseTail coll =
    // acc represents...
    let rec reverseTail' coll acc = 
        0
    0

// Hardest yet:
// Find all the even integers in a list.
let rec findEvens coll =
    match coll with 
    | [] -> []
    | h :: t when h % 2 = 0 -> h :: findEvens t
    | _ :: t -> findEvens t

