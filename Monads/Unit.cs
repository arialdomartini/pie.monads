namespace Monads
{
    public struct Unit
    {
        public static Unit Build() => default(Unit);

        // ReSharper disable once InconsistentNaming
        public static Unit unit => Build();
    }
}