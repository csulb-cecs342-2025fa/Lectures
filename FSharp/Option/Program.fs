// There is one union type of special importance in F#: Option<'a>
// It looks like this:
// type Option<'a> =
//     | None
//     | Some of 'a

let invalidValue = None
let validValue = Some "Neal"

// There is no "null" in F#; instead, we use Option for most use cases involving "null".

// Usage #1: a value does not exist
let lookupId name =
    match name with 
    | "Neal"  -> Some 1
    | "Karyl" -> Some 2
    | "Marco" -> Some 3
    | _       -> None

match lookupId "Michelle" with
| None -> printfn "Could not find that user"
| Some id -> printfn "That user has id %d" id

// Usage #2: error state
let safeDivide numerator denominator =
    match denominator with
    | 0 -> None
    | _ -> Some (numerator / denominator)

match safeDivide 10 0 with
    | None -> printfn "Could not divide those"
    | Some quotient -> printfn "The quotient was %d" quotient

// How is this different than null?
// Because we get compiler warnings/errors if we are given an option variable and don't
// have a match case to deal with it.

// Usage #3: optional parameters.
let printGreeting name rank =
    match rank with
    | None -> printfn $"Greetings, {name}!"
    | Some r -> printfn $"Greetings, {r} {name}!"

printGreeting "Neal" None
printGreeting "Mehrdad" (Some "Dr.")



open System
// Let's expand our math interpreter with a Log case, taking an optional base for the logarithm.
type Expression = 
    | Literal of float
    | Add of Expression * Expression
    | Sub of Expression * Expression
    | Log of Expression * Expression option // the base is the second argument. If None, implies base "e".

let rec evaluate expr =
    match expr with 
    | Literal c -> c
    | Add (expr1, expr2) -> (evaluate expr1) + (evaluate expr2)
    | Sub (expr1, expr2) -> (evaluate expr1) - (evaluate expr2)
    | Log (baseExpr, None) -> Math.Log(evaluate baseExpr)
    | Log (baseExpr, Some exponent) -> Math.Log(evaluate baseExpr, evaluate exponent)

printfn $"{evaluate (Log (Literal 10.0, None))}"
printfn $"{evaluate (Log (Literal 10.0, Some (Literal 10.0)))}"