// Lists are fixed-size, ordered sequences of homogenous values.
let list1 = [1; 2; 3]  // square brackets and semicolons

// Lists have "properties" accessed with "." syntax, like in Java/C#. 
// Every list is either empty [];
// or has a Head element and a Tail (the list containing everything besides the Head)
printfn "list1.Head: %d" list1.Head // output: 1
printfn "list2.Tail: %O" list1.Tail  // output: [2; 3]
printfn "list2's third element: %d" list1.Tail.Tail.Head  // output: 2

// We can write a "get-index" function with mutation...
let getIndex index (coll : int list) =
    let mutable node = coll
    let mutable counter = 0
    while counter < index do
        node <- node.Tail
        counter <- counter + 1
    
    node.Head


// But it only works with int lists!
// Head to Generics project and then return here.

// Let's try "third":
let third (coll: 'a list) =
    coll.Tail.Tail.Head

// Not bad. Can we get rid of the type annotation?
(*
let third2 coll =
    coll.Tail.Tail.Head
*)
// Nope! F# can't tell what type "coll" is because many types might have a ".Tail"
// property. But there is something that can help: the List module.

let third3 coll =
    List.head (List.tail (List.tail coll))
    // The List.tail function has type "'a list -> 'a list". Therefore,
    // what type is coll?
    // And what type is returned from the function?


// We have the motivation now to introduce one of my favorite parts of F#, the humble
// pipe-forward operator |>. 
// One way to call the function "f" with parameter "x" is: f x
// Another way is with this new operator: x |> f
// See how the order is reversed? |> takes a VALUE before and applies the FUNCTION
// which follows, by inserting the value as the last argument to the function.

// Example:
10 |> printfn "%d" // turns into printfn "%d" 10

// So instead we can do:
let third4 coll =
    List.head (List.tail (coll |> List.tail))
    // which is NOT a strong example..
    // The real power comes from CHAINING the operator.

// ULTIMATE GOAL: WHEN REVIEWING THIS LESSON, THIS IS WHAT YOU WANT TO UNDERSTAND:
let third5 coll =
    coll
    |> List.tail   // List.tail coll
    |> List.tail   // List.tail (List.tail coll)
    |> List.head   // List.head (List.tail (List.tail coll))
    
    // coll is passed to List.tail; the result is passed to List.tail; the result
    // is passed to List.head.

// HOW COOL IS THAT.

// We can use this everywhere.
let inputFloat = System.Console.ReadLine() |> float // don't need parens now.

// Take the third element of the list, convert to float, square root it, log it, then print.
list1 |> third |> float |> System.Math.Sqrt |> System.Math.Log |> printfn "%f"

// Compare to the imperative (C/Java/imperative F#) way of calling that:
printfn "%f" (System.Math.Log (System.Math.Sqrt (float (third list1))))

// Putting it all together to make a generic getIndex
let getIndexGeneric index coll =
    let mutable node = coll
    let mutable counter = 0
    while counter < index do
        node <- List.tail node // the List.tail function returns the tail of a list.
                               // Its type is 'a list -> 'a list. So what type is node?
        counter <- counter + 1
    
    List.head node

// Of course, the List module already has a function to do this!
list1
|> List.item 2 // examine the function type!!
|> printfn "%d" 



let rec contains coll x =
    match coll with 
    | [] -> false
    | h :: _ when x = h -> true
    | _ :: t -> contains t x
    (*if coll = [] then  
        false
    elif List.head coll = x then
        true
    else
        contains (List.tail coll) x*)

let rec length coll =
    match coll with 
    | [] -> 0
    | _ :: t -> 1 + length t


let rec getIndexRecursive coll n =
    match n with 
    | 0 -> List.head coll
    | _ -> getIndexRecursive (List.tail coll) (n - 1)


let rec indexOf coll x =
    match coll with 
    | [] -> -1
    | h :: _ when h = x -> 0
    | _ :: t -> let r = indexOf t x
                if r = -1 then -1
                else r + 1

let rec reverse coll =
    match coll with 
    | [] -> []
    | [x] -> [x]
    | h :: t -> reverse t @ [h]