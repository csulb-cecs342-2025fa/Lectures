def function1():
    x = [1, 2, 3]
    x.append(4)
    return x[1]

if __name__ == "__main__":
    r = function1()
    # Calling function1() will construct a list of 4 values.
    # What happens to that list when function1() is done?
    print(r)