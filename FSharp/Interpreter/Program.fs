open System

// This is a very basic math interpreter, using unions to discriminate between 
// expression operators.


// An expression is either a constant value, or an arithmetic function applied to
// one or more operands that are expressions.
type Expression = 
    | Literal of float
    | Add of Expression * Expression
    | Sub of Expression * Expression

   
// evaluate converts an Expression object into the floating-point number it represents.
let rec evaluate expr =
    match expr with 
    | Literal c -> c
    | Add (expr1, expr2) -> (evaluate expr1) + (evaluate expr2)
    | Sub (expr1, expr2) -> (evaluate expr1) - (evaluate expr2)

let demo1 = Sub (Add (Literal 10, Literal 3), Literal 2)
// Evaluate and print the expression.
printfn $"evaluate {demo1} = {evaluate demo1}"

