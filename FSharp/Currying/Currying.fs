open System

let coll = [1; 2; 3; 4; 5]
coll 
|> List.filter (fun x -> x % 2 = 0)
|> printfn "%A"


// The |> operator is not some compiler voodoo magic. It is actually defined in F# itself.

let (|>) x f =
    // Given a value before the operator and a function after, call the function with the value.
    f x

// The type of |> is 
// 'a -> ('a->'b) -> 'b

let s = 9.0 |> Math.Sqrt // Call Math.Sqrt with the argument 9.0.
// For this call, 'a is float, 'b is float.
// So the call is float->(float->float)->float.
printfn "%f" s


// So what about
let t = coll |> List.filter (fun x -> x % 2 = 0)
// What type is t?


// So 'b is int list. 'a is int list. Therefore f must be int list->int list.
// That means "List.filter (fun x -> x % 2 = 0)" must be of type (int list->int list).
// But how can this be? 
// The secret is in the type of List.filter.

// Pop quiz!
// What is the type of List.filter?



// And what is the type of (fun x -> x % 2 = 0)?



// So if "List.filter" is of type                    (int->bool)->int list->int list, 
// and "(fun x -> x % 2 = 0)" is of type             (int->bool) 
// and "List.filter (fun x -> x % 2 = 0)" is of type              int list->int list,

// do you see what happened?






// What are the uses?
// Say I want to add 5 to every number in coll.
coll 
|> List.map (fun x -> 5 + x)
|> printfn "%A"

// But the + operator is a function of type int->int->int. 
// So I can partially apply (+) with a single argument to make a function
// of int->int.
coll 
|> List.map ((+) 5)
|> printfn "%A"

// Other tricks:
coll
|> List.filter ((>) 3)
|> printfn "%A"




coll
|> List.filter ((%) 2 >> (=) 0)
|> printfn "%A"

