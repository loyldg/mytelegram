namespace MyTelegram.Schema;

internal class MyReflectionHelper
{
    public static Func<T> CompileConstructor<T>(Type typeOfT)
    {
        var expr = Expression.New(typeOfT);
        return Expression.Lambda<Func<T>>(expr).Compile();
    }
}