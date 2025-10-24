// Inner functions can use any variable that is visible in the outer scope.
let filter predicate coll =
    let rec filter' coll acc =
        match coll with 
        | [] -> List.rev acc
        // We can use the "predicate" parameter even though it's not a 
        // parameter to filter'.
        | h :: t when predicate h = true -> filter' t (h :: acc)
        | _ :: t                         -> filter' t acc

    filter' coll []


// return true if a value larger than x is in coll.
let findSomethingLarger x coll =
    // we can use x even though it's not a parameter to the anonymous function
    List.find (fun i -> i > x) coll


// getAdder: returns a function that adds 2*x to its parameter.
let getAdder x =
    let z = x * 2
    // z is a local variable. It can only be accessed from within this function, just
    // like the parameter x.

    (fun y -> y + z)

// First: what type is getAdder?




// When we call getAdder 10, we conceptually get back: a function that adds 20 to its parameter.
let adderFunc = getAdder 10
// What type is adderFunc?


// We can invoke that function:
adderFunc 5 
|> printfn "%d" // and expect it to print 25.

// But something strange is going on...
// The variable z only exists when we're in scope of getAdder.
// By returning the anonymous function, we leave that scope, and z is no longer in memory.
// Yet z is used on line 32 (after getAdder has exited...) and the output behaves as if z still exists.
// What's going on???






// Discovering whether F# does early (deep) or late (shallow) binding.

let getAdder2 x =
    let mutable z = 2 * x // z = 20
    let closure = fun y -> y + z // create a closure around z ---- "y plus 20"
    z <- 5 * x // mutate z    z = 50

    closure // return the closure  is it now... y plus 20, or y plus 50?

let adderFunc2 = getAdder2 10
adderFunc2 5
|> printfn "%d" // what gets printed? 25, or 55?




let getAdder3 x =
    let mutable z = [1; 2]
    let closure = fun y -> y :: z // create a closure around z
    z <- [1; 2; 3] // mutate z

    closure // return the closure

let adderFunc3 = getAdder3 10
adderFunc3 5
|> printfn "%A" // what gets printed? 25, or 55?
