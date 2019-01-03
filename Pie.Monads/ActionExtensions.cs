using System;

namespace Pie.Monads
{
    public static class ActionExtensions
    {
        public static Func<T, Unit> ToFunction<T>(this Action<T> action) =>
            t1 => { action(t1); return Functional.unit; };

        public static Func<TT1, TT2, Unit> ToFunction<TT1, TT2>(this Action<TT1, TT2> action) =>
            (t1, t2) => { action(t1, t2); return Functional.unit; };

        public static Func<TT1, TT2, TT3, Unit> ToFunction<TT1, TT2, TT3>(this Action<TT1, TT2, TT3> action) =>
            (t1, t2, t3) => { action(t1, t2, t3); return Functional.unit; };
    
        public static Func<TT1, TT2, TT3, TT4, Unit> ToFunction<TT1, TT2, TT3, TT4>(this Action<TT1, TT2, TT3, TT4> action) =>
            (t1, t2, t3, t4) => { action(t1, t2, t3, t4); return Functional.unit; };
    }
}