// A first-class member can:
// 1. Be assigned to a variable.

let max a b = if a > b then a else b
// int->int->int

let remainder dividend divisor = dividend % divisor
// int->int->int

let f = max
// f is a *variable*. What is its type?
// int->int->int

// What can we do with a function variable? Call it!
f 10 5 |> printfn "%d is the max"


// 2. Be passed as a parameter to a function.
let useIntegers a b f =
    if a < 0 then 
        b + 1
    else
        f a b

// Perform type inference on this function and its parameters.
// What type is.... a?
//             .... b?
//             .... f?
//             .... useIntegers?


// 3. Be returned from a function.
let selectIntFunction a =
    if a < 0 then
        max
    else
        remainder

// int->(int->int->int)

// What type is selectIntFunction?

// Putting it together...
5
|> selectIntFunction
|> useIntegers 5 4
|> printfn "result: %d"








// 4. Be created dynamically at run-time.
let input = System.Console.ReadLine() |> int
let runtimeFunction =
    if input > 0 then
        // the "fun" keyword creates an anonymous function.
        // it has no names, just arguments and a body.
        // like all functions, it returns the last statement in the body.
        (fun x y -> x + y)
    else
        (fun x y -> x - y)

// What type is runtimeFunction?
// Do we know *right now* which function body will be called?
// But can we use it anyway? Yes!

runtimeFunction
|> useIntegers -5 10
|> printfn "runtime result: %d"