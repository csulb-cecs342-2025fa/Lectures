// Another syntax for writing functions using "pattern matching"
let equalsOne x =
    if x = 1 then
        true
    else 
        false

printfn $"{equalsOne 10}" // %O is for any objet; prints false.
// This syntax is unwieldy. When we have a pattern of "if this thing matches this pattern, then do this",
// we can use a special syntax.

let equalsTwo x =
    match x with
    | 2 -> true
    | _ -> false // _ means "match anything"

printfn $"{equalsTwo 2}" // prints true


// Works with strings too
let getPrice food =
    match food with
    | "banana" -> 0.79
    | "watermelon" -> 3.49
    | "tofu" -> 1.09
    | _ -> nan



// We can do lots of complicated functions using pattern matching instead of nested if-elifs.
let rec factorial n = 
    match n with
    | 0 -> 1
    | x -> x  * factorial (x - 1)

let rec fibonacci n =
    match n with 
    | 0 -> 0
    | 1 -> 1
    | x -> fibonacci (x - 1) + fibonacci (x - 2)


// FINALLY, we can pattern match with tuples too.
let userAccount1 = ("Naomi Nagata", "Engineering")
let userAccount2 = ("James Holden", "Command")
let userAccount3 = ("Roberta Draper", "Security")

let canAccessReactor account =
    match account with
    | (_, "Engineering") -> true
    | (_, "Command") -> true
    | _ -> false

// Sometimes we want to add a boolean condition to a match; we do this with "when".
let memberOf department account =
    match account with
    | (_, dep) when dep = department -> true
    | _ -> false