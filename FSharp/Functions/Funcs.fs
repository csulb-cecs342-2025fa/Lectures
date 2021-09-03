// We use "let" to define functions, too.
let square x = 
    x * x

// The parameters to the function follow its name, with no commas.
// The = sign starts the function body.
// There is no "return" in F#; the last expression in the function body is the return value.
// Jump to "main" below to see how to call this function.

// Note we did not define a type for x... so what type is it, and how is that determined?

// If the context of the function makes it clear, F# can infer the type of each parameter.
// In this case, F# defaults to "int" for x, because we perform x * x in the body.

// If the context is unclear, then other hints may be needed.


// What can I DO with a function?
// call!
// What do I need to know about a function to call it?
//     number and order and types of parameters
//     return type

// A function that takes an int parameter and returns int:

// int -> int

// Like variables, "square" has a type. Its type is "int -> int", which indicates a function
// taking an int variable and returning an int.



// Apply the given anuual interest rate to the given principal amount for the given number of years,
// then return the resulting balance.
let annualInterest principal rate numYears =
    principal * ((1.0 + rate) ** (float numYears))

// float -> float -> float -> float

// arithmetic can only be done on two values of the exact same type

  

// What types are the three parameters? Infer it based on the body of the function.


// The type of "interest" is float -> float -> int -> float; a function taking a float, float,
// and int; and returning float.


// Convert Fahrenheit degrees to Celcius
let toCelcius tempF = 
    (tempF - 32.0) * 5.0 / 9.0


// Challenges:
// Write a function that concatenates a string with itself.
// Hint: we use + for concatentation.
let doubleString (s : string) = 
    s + s

// Lesson: sometimes we need a type annotation.




// Write a function that returns the absolute value of an integer.
let absoluteValue x =
    if x < 0 then
        -x
    elif x = 0 then
        0
    else
        x


// Lesson: "if" is an expression, not a statement.



// This "main" is technically optional; F# will execute all statements at the leftmost
// indent from top to bottom. But it's good practice, like Python's "if __name__ == '__main__'"
[<EntryPoint>]
let main argv = 
    printfn "5 squared is %d" (square 5)
    
    printfn "$10,000 after 10 years at 0.01%% interest yields %0.2f" (annualInterest 10000.0 0.0001 10) 

    printfn "90 degrees F = %0.1f degrees C" (toCelcius 90.0)    

    0 // return an integer exit code
