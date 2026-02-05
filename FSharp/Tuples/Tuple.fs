// Tuples are fixed-length, ordered collections of heterogenous data.
// Tuples are constructed with parentheses.

let dinner = ("green eggs", "ham") // commas separate tuples, semicolons separate lists
// The type of dinner is "string * string", indicating a tuple of two string values.

let another = (1, 2)

// Tuples can contain any data type, including other tuples.
let nested = (1, (2.0, (3, 4, 5)), (4L, "5", '6'))
// What type is nested?

// If a tuple is a pair, then you can use the functions "fst" and "snd" to retrieve 
// the first and second elements, but ONLY FOR PAIRS.
let meal1 = fst dinner
let meal2 = snd dinner

//let nestedPair = snd nested // ERROR: nested has length 3, not 2.

// So how to access the elements of a longer tuple?
let placings = ("Katie Ledecky", "Sarah Sjöström", "Emma McKeon", "Frederica Pellegrini")
let (gold, silver, bronze, _) = placings
// This is called "unpacking". The parentheses are optional. The underscore means "don't care."

// Tuples are useful for gluing together values when you don't want to have to create a new
// datatype. They have good performance in F#, but they aren't a replacement for classes.

// Tuples can be passed as parameters to functions. This should not be a shock.
let addOneToTuple tup =
    let (x,y,z) = tup
    (x+1, y+1, z+1)
// What type is "tup"?

let answer1 = addOneToTuple (5, 6, 2) // WARNING: do not equate this with "normal function calls" 
                                      // in Java, C++, etc. The parentheses here create a single
                                      // tuple value, and the function takes only one parameter.


// So what can we use tuples for, really?
// 1. To return multiple values from a function, as long as those values aren't better represented
//    by a new proper type.
// 2. Another special use we'll see later.

// Attempt to parse a string as an integer. Return a tuple with a boolean indicating whether the
// parse succeeded, and the parsed integer if the parse succeeded, otherwise 0.
let tryParseInt str =
    try
        let i = System.Int32.Parse str
        (true, i)
    with _ -> (false, 0) // this is "catch any exception, and do this->".

let (success, value) = tryParseInt "100"
if success then
    printfn "We parsed the value %d" value

let (success2, value2) = tryParseInt "99x"
if success2 then
    printfn "We then parsed the value %d" value2


// Next: records!