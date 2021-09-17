open System

// Tuples and records are "multiplicative types": you must have 1 value for each
// of the types involved in the record in order to construct a value of the
// record type, e.g., you must have latitude AND longitude.

// Another category of type is an "additive" or "summation" type, in which you 
// must have only ONE of many individual values to construct a value of the
// new additive type. Additive types in F# are called discriminated unions.

// Suppose I want to represent a "contact", where a contact can either be
// a phone number (int64) or an email address (string).
// I could make a record with two fields, but then BOTH fields have to be set
// to constuct a contact. A better means is with a union.

type Contact =
    | Email of string
    | Phone of int64

// Values of type Contact are constructed by specifying one of the two
// possible cases and providing a value. Each case is called a "type constructor".

let anthony = Email "anthony.g@csulb.edu"
let burkhard = Phone 5629855555L
// Both of these variables are of type Contact. One is a string, one is an int64.

// But how do we deal with such variables if we don't know which of the cases
// they correspond with? I can't attempt to read the Email of burkhard, or the
// Phone of anthony, because those things don't exist. Pattern matching to the
// rescue!

let howToContact contact =
    match contact with
    | Email e -> "Email them at " + e
    | Phone p -> "Call them at " + string p

// REMINDER: this is the same as the shortcut function:
let howToContact2 = function
    | Email e -> "Email them at " + e
    | Phone p -> "Call them at " + string p

howToContact anthony |> printfn "%s"
howToContact2 burkhard |> printfn "%s"

// This is the only way to interact with unions: by matching their constructor
// to gain access to the associated field and deal with that.
// The nice part? If we add a new constructor, say "Address of string"...

// we now get warnings that we aren't dealing with every case of the union,
// which will help us avoid bugs and oversights in the code.


// Not all type constructors require a value; some can be empty.
type SubmissionResult = 
    | Accepted
    | RejectMessage of string

let submitWork effortLevel =
    if effortLevel >= 0.8 then
        Accepted
    else
        RejectMessage "work harder, you lazy bum"

match submitWork 0.7 with
| Accepted -> printfn "%s" "Work was accepted!"
| RejectMessage m -> printfn "Work was rejected: %s" m


// Unions can cleanly and beautifully represent computations that can fail.
// Division can fail if the denominator is 0. We can't guarantee that the result
// of dividing two integers is an integer; it could be Undefined, which is not an int.
type DivisionResult =
    | Quotient of int
    | Undefined

// so a DivisionResult is either a Quotient integer, or Undefined.
// We can now write a "safe divide":
let safeDivide dividend divisor =
    match divisor with
    | 0 -> Undefined
    | _ -> Quotient (dividend / divisor)

let good = safeDivide 10 3 // good = Quotient 3
let bad = safeDivide 10 0  // bad  = Undefined





// Discriminated unions can be used in surprisingly powerful ways. 
// I know you love binary trees... let's build one in F#!

// A binary tree is either empty, or has: a value; a tree to its left; and a tree to its right.
type BinaryTree =
    | Empty
    | Node of int * BinaryTree * BinaryTree


let exampleTree = Node (10, 
                       Node (5, 
                            Node (2, Empty, Empty),
                            Node (7, Empty, Empty)
                       ), 
                       Node (15, Empty, Empty)
                  )

let isEmpty tree =
    match tree with
    | Empty -> true
    | _     -> false

let rec height tree =
    match tree with 
    | Empty -> -1
    | Node (_, left, right) -> 1 + max (height left) (height right)

let rec findMaxValue tree =
    match tree with
    | Empty              -> System.Int32.MinValue
    | Node (i, _, Empty) -> i
    | Node (_, _, right) -> findMax right

let rec treeContains v tree =
    match tree with
    | Empty                        -> false
    | Node (i, _, _)    when v = i -> true
    | Node (i, left, _) when v < i -> treeContains v left
    | Node (_, _, right)           -> treeContains v right

exampleTree |> treeContains 15 |> printfn "Tree contains 15? %A"
