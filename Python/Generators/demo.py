from lazy import *

if __name__ == "__main__":
    for i in take(100, range(101, 1000000000000000000000000000000000000000000, 1)):
        print(i)


    exit(0)





    odds = filter(lambda x: x % 2 == 1, seq)
    firstFive = take(5, odds)
    squares = map(lambda x: x * x, firstFive)
    print("LET'S GOOOO")
    sumOfSquares = sum(squares)



    # Or:
    sumOfSquares = sum(
        map(lambda x: x * x, 
            take(5, 
                 filter(lambda x: x % 2 == 1, 
                        range(2, 100000, 1)
                )
            )
        )
    )

    # In in amazing language like F# this would roughly be:
    #
    # range 2 100000 1
    # |> filter (fun x -> x % 2 = 1)
    # |> take 5
    # |> map (fun x -> x * x)
    # |> sum
    # 
    # But alas...