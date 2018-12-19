namespace Monads
{
    public static partial class Functional
    {
        // ReSharper disable once InconsistentNaming
        public static Unit unit =>  Unit.Build();
    }

    public struct Unit
    {
        public static Unit Build() => default(Unit);
    }
}