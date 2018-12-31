# Either
`Either<TLeft, TRight>` is a monadic type that incorporates the context of possible failures to values. It is tipically used to apply functions to value and to provide useful information regarding the possible failures.

An `Either<TLeft, TRight>` value can either be a `TRight` value , signifying a right value, or a success, or a `TLeft` value, signifying a failure.

## Building right and left values
Right and left values can be built in 2 different ways:

* by casting, either implicitly or explicitly, the type `Either<TL, TR>` to `TL` or `TR`;
* with the helper methods `Right()` and `Left()`.

### Building right and left values via casting
The type `Either<TLeft, TRight>` can be casted from a `TLeft` or a `TRight` value.

For example, the following produces a right value:

```csharp
Either<Error, string> value = "some value";
```

while the next builds a left value:

```csharp
Either<int, string> value = -1;
```

Implicit casting can be conveniently used when returning from functions. For example, the following is a method that returns either an integer or an error value of type `Error`:

```csharp
Either<Error, int> MethodThayMayFail(string argument)
{
    if(argument.IsNullOrEmpty())
        return new Error("Argument cannot be null");

    return 42;
}
```

Casting cannot be used when working with `Either` values whose left and right types are the same, for example with `Either<string, string`.

### Building right and left values with `Right` and `Left`

Usually, it is best to use the factory methods `Right()` and `Left()`. These can be made available with the directive: 

```csharp
using static Monads.Functional;
```

This approach is especially useful when it's not immediately clear that the left type represents a failure. Say for example that a method returns a string message in case of failures, and an integer in case of success. The use of `Right` and `Left` makes it clear when the code deals with failures and successful values:

```csharp
using static Monads.Functional;

public class Example
{
    Either<string, int> MethodThayMayFail(string argument)
    {
        if(argument.IsNullOrEmpty())
            return Left("Argument cannot be null");

        return Right(42);
    }
}

```

This also works when the left and the right values have the same type:

```csharp
using static Monads.Functional;

public class Example
{
    Either<string, string> MethodThayMayFail(string argument)
    {
        if(argument.IsNullOrEmpty())
            return Left("Argument cannot be null");

        return Right(argument.ToLower());
    }
}

```

## Match
C# has no proper pattern matching to match left and right values. As an alternative, `Either` exposes the method `Match`.<br />
`Match` must be provided with 2 actions: the one to be applied in case `Either` containes a left value, and the one to be run if `Either` contains a right value. For example:

```csharp
Either<Error, string> SomeMethod() =>
    Right("foobar");

var value = SomeMethod();

value.Match(
    left => { Console.WriteLine("it contains left value"); },
    right => { Console.WriteLine($"The contained value is {right}"); });
```

outputs `The contained value is foobar`.

If `Match` is provided 2 `Func`s instead of 2 `Action`s, it returns the value of the matching one:

```csharp
Either<string, int> Divide(int a, int b)
{
    if (b == 0)
        return Left("cannot divide by zero");

    return a / b;
}

var result = Divide(10, 2).Match( 
    _ => -1, 
    r => r);
Console.WriteLine(result);        // => 5


var error = Divide(10, 0).Match( 
    l => $"Got the error: {l}", 
    _ => "should not happen");
Console.WriteLine(error);         // => "Got the error: cannot divide by zero"
```


## Map

`Either<TL, TR>` is a Functor, therefore it is possible to apply functions to its content. If the either contains a right value, the function is applied to it; otherwise, the left value is returned.

```csharp
Either<string, int> value = Right(10);
Either<string, int> error = Left("an error");


value.Map(v => v * 2);   // Right(20)
error.Map(v => v * 2);   // Left("an error")
```

`Map` is similar to LINQ's `Select` method.<br /> A function `Func<X, Y>` from type `X` to `Y` applied with `Map()` to an `Either<T, X>` returns an `Either<T, Y>`. For example:

```csharp
Either<string, int> article1 = Right(10);
Either<string, int> article2 = Left("Not found");

Uri BuildUri(int articleId) =>
    new Uri("https://api.com/articles/{articleId}");

article1.Map(BuildUri);    //  new Uri("https://api.com/articles/10")
article2.Map(BuildUri);    //  "Not found"
```

