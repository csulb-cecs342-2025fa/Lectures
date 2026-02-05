open System // like an import statement

// F# is a multi-paradigm language. It combines aspects of imperative, functional, OO, and scripting
// languages.

// We will start learning the imperative/scripting side of F#, and later move into functional programming.

// Functions are invoked by their name followed by their arguments, with no commas or parentheses.
printfn "Hello world."
printfn "Welcome to F#!"

// Note that we haven't defined a "main" method. Like most "scripting" languages, F# files execute 
// from top to bottom.

// Use "let" to declare a variable or a function.
let pi = 3.14159
// Notice what's missing?

// F# makes heavy use of type inference to determine types. In many cases, you will never specify
// the type of a variable or function.
// The variable "pi" does have a type: it is of type "float". In F#, float is a 64-bit floating
// point number, akin to "double" in Java/C#. How was this type determined?

let x = 10
// What type is x? How was that determined?

// F# runs on the "Common Language Runtime", which is Microsoft's equivalent of the 
// Java Virtual Machine. F# supports all types that othe "CLR" languages like C# support,
// including 1-, 2-, 4-, and 8-byte integers; 4- and 8-byte floating points; booleans;
// and 2-byte character values.


// F# has a VERY STRICT type system that does almost NO automatic coercions.
// let f = x * 1.5
// Error: type "float" does not match type "int"
// In F#, math operators are only valid when the types of the operands match EXACTLY. 

// To fix this example, we call the "float" function on x to turn it into a float.
let f = float x * 1.5
// If it makes more sense, this line can be rewritten as
let f2 = (float x) * 1.5


// The funny thing about F# variables is that they are immutable.
x = 100
printfn "x is now %d" x // what does this print?

// Outside of a declaration, = is used for comparison; it is not for assignment.
// F# variables cannot be assigned new values after they are declared...
//   for a few more minutes

let age = 37
// if, elif, else statements
if age < 10 then
    printfn "You are young"
elif age < 43 then
    printfn "You are getting older..."
else
    printfn "You old"
