open System
open System.IO

// The >> operator is called the function composition operator. Function composition
// comes from mathematics, where we say: "f compose g(x) = f(g(x))"

// In F#, suppose we have g: string->int
//                    and f: int->float
let g x =
    String.length x

let f x =
    sqrt (float x)

// We can "compose" the two functions to get a single function from 'a->'c with  the
// >> operator.
let h = g >> f

// And then invoke h, because it is a function:
h "Hello" |> printfn "%f"

// The >> operator is supposed to look like "this followed by this"; so h is g, followed
// by f, where f uses the output of g. h then takes the same input as g.

// More generically, the type of the operator >> is this:
// ('a->'b)->('b->c)->('a->'c)

// Given a function from a to b, and another from b to c, compose a new function from a to c.

// Note that composition does not invoke any functions, it just creates a new function
// that can be invoked later.

// Example: given a list of strings, compute a list of square roots of the length of those strings.
let strings = ["Neal"; "Mehrdad"; "Birgit"; "Burkhard"]
// One way: multiple maps
let lengths1 = strings |> List.map String.length |> List.map float |> List.map sqrt
// Another way: one map, anonymous function
let lengths2 = strings |> List.map (fun s -> s |> String.length |> float |> sqrt)
// Best? way: with composition
let lengths3 = strings |> List.map (String.length >> float >> sqrt)
// Or even:
let sqrtLen = String.length >> float >> sqrt
let lengths4 = strings |> List.map sqrtLen

printfn "%A" lengths4



// PUTTING IT ALL TOGETHER
// We've expanded our functional programming toolkit with many options in F#.
// Here's how they can help us write easier, cleaner code.

// Imperative style
let folderSize1 folder =
    let files = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories)

    let mutable sum = 0L
    let mutable i = 0
    while i < files.Length do
        let file = files.[i]
        let fileInfo = new FileInfo(file)
        let length = fileInfo.Length
        sum <- sum + length
        i <- i + 1
    sum
    
    // Ugly, and error prone!
    
    
// Functional style 1:
let folderSize2 folder =
    let files = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories)

    let fileInfos = Array.map (fun (file : string) -> new FileInfo(file)) files
    let lengths = Array.map (fun (file : FileInfo) -> file.Length) fileInfos
    let sum = Array.sum lengths
    sum

    // A little better, but pretty verbose, and not taking advantage of type inference.

// Functional style 2:
let folderSize3 folder =
    Array.sum 
        (Array.map
            (fun (file : FileInfo) -> file.Length)
            (Array.map
                (fun (file : string) -> new FileInfo(file))
                (Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories))))

    // No ugly temp variables, but horribly ugly indentation and confusing order of execution!
       
// Functional style 3:
let folderSize4 folder =
    let getFiles path =
        Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
    
    folder 
    |> getFiles
    |> Array.map (fun f -> new FileInfo(f))
    |> Array.map (fun f -> f.Length)
    |> Array.sum

    // Now we're getting somewhere. Type inference and piping make this very easy to read and understand.

// One final approach, with composition:
let folderSizeComposed = // NO PARAMETER!!
    let getFiles path = 
        Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)

    getFiles
    >> Array.map (fun f -> new FileInfo(f))
    >> Array.map (fun f -> f.Length)
    >> Array.sum
    

    // The shortest of all answers. Even though there's no parameter, 
    // folderSizeComposed is a function because it is equal to the result of 
    // a composition chain. folderSizeComposed takes the same input as 
    // getFiles (a string) and returns the same output as the 
    // final Array.sum (an int64)

folderSizeComposed "." 
|> printfn "%d bytes in the current working directory, %s" 
<| Environment.CurrentDirectory