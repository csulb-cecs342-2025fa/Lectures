open System

// Tuples and records are "multiplicative types": you must have 1 value for each
// of the types involved in the record in order to construct a value of the
// record type, e.g., you must have latitude AND longitude.

// Another category of type is an "additive" or "summation" type, in which you 
// must have only ONE of many individual values to construct a value of the
// new additive type. Additive types in F# are called discriminated unions.

// Suppose I want to represent a "Shape", where a Shape can either be
// a Rectangle, a Circle, or a Triangle. I could do this with records, but then
// I would have to have fields for all the possible shapes, and only one of
// them would be filled in at a time, which is a bit wasteful and awkward. With
// a discriminated union, I can just say that a Shape is either a Rectangle, a Circle,
// or a Trapezoid, and then I can give each of those cases the fields that they
// need, without having to worry about the other cases.

type Shape =
    | Rectangle of width: float * height: float
    | Circle of radius: float
    | Trapezoid of base1: float * base2: float * height: float

// Values of type Shape are constructed by specifying one of the three
// possible cases and providing a value. Each case is called a "type constructor".
let r = Rectangle (10.0, 20.0)
let c = Circle 5.0
let t = Trapezoid (10.0, 20.0, 5.0)

// If we examine the types of these names, we see they are only known to be of type Shape,
// not Rectangle, Circle, or Trapezoid. That can make it awkward to work with them,
// because we (the compiler) don't know which of the three cases they correspond with.

// let w = r.width // error: no field "width" on type "Shape"

// The way to work with union values is with pattern matching. 
// Take a Shape object, and match it against each of the possible cases.
// Whatever case it matches, implement some logic specific to that case.
let areaOfShape s =
    match s with 
    | Rectangle (w, h) -> w * h
    | Circle r -> Math.PI * r * r
    | Trapezoid (b1, b2, h) -> (b1 + b2) * h / 2.0

printfn $"Area of r: {areaOfShape r}" // output == ???

// This is the only way to interact with unions: by matching their constructor
// to gain access to the associated field and deal with that.
// The nice part? If we add a new constructor, say "Triangle of float * float"...
type Shape2 =
    | Rectangle of width: float * height: float
    | Circle of radius: float
    | Trapezoid of base1: float * base2: float * height: float
    | Triangle of baseLength: float * height: float

// we now get warnings that we aren't dealing with every case of the union,
// which will help us avoid bugs and oversights in the code.
let areaOfShape2 (s : Shape2) =
    match s with  // WARNING: INCOMPLETE PATTERN MATCH, MISSING CASE Triangle 
    | Rectangle (w, h) -> w * h
    | Circle r -> Math.PI * r * r
    | Trapezoid (b1, b2, h) -> (b1 + b2) * h / 2.0


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
// This protects us from using a quotient that is undefined, because we must have
// separate match cases for a good result vs a bad result:
match bad with 
| Undefined -> printf "That division failed"
| Quotient q -> printf $"That division succeed and equals {q}"




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
    | Node (_, _, right) -> findMaxValue right

let rec treeContains v tree =
    match tree with
    | Empty                        -> false
    | Node (i, _, _)    when v = i -> true
    | Node (i, left, _) when v < i -> treeContains v left
    | Node (_, _, right)           -> treeContains v right

exampleTree |> treeContains 15 |> printfn "Tree contains 15? %A"
