namespace Monads
{
    public static class Functional
    {
        public static Either.Right<TR> Right<TR>(TR value) =>
            new Either.Right<TR>(value);

        public static Either.Left<TL> Left<TL>(TL value) =>
            new Either.Left<TL>(value);
    }
}