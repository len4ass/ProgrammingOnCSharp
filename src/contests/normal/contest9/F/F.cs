using System;

internal class Singleton<T> where T : class, new()
{
    private static T _obj;
    public static T Instance
    {
        get => _obj ??= new T();
        set => throw new NotSupportedException("This operation is not supported");
    }
}