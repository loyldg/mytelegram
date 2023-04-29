using System.Linq.Expressions;

namespace MyTelegram.Domain.EventFlow;

public static class MyReflectionHelper
{
    public static Func<T> CompileConstructor<T>()
    {
        var expr = Expression.New(typeof(T));
        return Expression.Lambda<Func<T>>(expr).Compile();
    }

    public static Func<T> CompileConstructor<T>(Type typeOfT)
    {
        var expr = Expression.New(typeOfT);
        return Expression.Lambda<Func<T>>(expr).Compile();
    }

    public static Func<TInterfaceParameter, TResult> CompileConstructor<TInterfaceParameter, TResult>(
        Type typeOfTInterfaceParameterImpl,
        Type typeOfTResult)
    {
        var constructor = typeOfTResult.GetConstructor(new[] { typeOfTInterfaceParameterImpl });
        ArgumentNullException.ThrowIfNull(constructor);
        var parameter = Expression.Parameter(typeof(TInterfaceParameter));

        var body = Expression.New(constructor, Expression.Convert(parameter, typeOfTInterfaceParameterImpl));
        var lambda = Expression.Lambda<Func<TInterfaceParameter, TResult>>(body, parameter);
        return lambda.Compile();
    }

    public static Func<T1, TResult> CompileConstructor<T1, TResult>()
    {
        var typeOfT1 = typeof(T1);
        var parameter1 = Expression.Parameter(typeOfT1);
        var constructor = typeof(TResult).GetConstructor(new[] { typeOfT1 });
        ArgumentNullException.ThrowIfNull(constructor);

        var body = Expression.New(constructor, parameter1);
        var lambda = Expression.Lambda<Func<T1, TResult>>(body, parameter1);
        var method = lambda.Compile();
        return method;
    }

    public static Func<T1, TResult> CompileConstructor<T1, TResult>(Type typeOfTResult)
    {
        var typeOfT1 = typeof(T1);
        var parameter1 = Expression.Parameter(typeOfT1);
        var constructor = typeOfTResult.GetConstructor(new[] { typeOfT1 });
        ArgumentNullException.ThrowIfNull(constructor);

        var body = Expression.New(constructor, parameter1);
        var lambda = Expression.Lambda<Func<T1, TResult>>(body, parameter1);
        var method = lambda.Compile();
        return method;
    }

    public static Func<object, TResult> CompileConstructor<TResult>(Type typeOfTResult,
        Type typeOfT1)
    {
        var constructorArgumentTypes = new[] { typeOfT1 };
        var parameters = constructorArgumentTypes.Select(_ => Expression.Parameter(typeof(object))).ToArray();
        var constructor = typeOfTResult.GetConstructor(constructorArgumentTypes);
        ArgumentNullException.ThrowIfNull(constructor);

        var constructorArguments = new Expression[] { Expression.Convert(parameters[0], typeOfT1) };

        var body = Expression.New(constructor, constructorArguments);
        var lambda = Expression.Lambda<Func<object, TResult>>(body, parameters);
        var method = lambda.Compile();

        return method;
    }

    public static Func<object, object, TResult> CompileConstructor<TResult>(Type typeOfTResult,
        Type typeOfT1,
        Type typeOfT2)
    {
        var constructorArgumentTypes = new[] { typeOfT1, typeOfT2 };
        var parameters = constructorArgumentTypes.Select(_ => Expression.Parameter(typeof(object))).ToArray();
        var constructor = typeOfTResult.GetConstructor(constructorArgumentTypes);
        ArgumentNullException.ThrowIfNull(constructor);

        var constructorArguments = new Expression[]
            { Expression.Convert(parameters[0], typeOfT1), Expression.Convert(parameters[1], typeOfT2) };

        var body = Expression.New(constructor, constructorArguments);
        var lambda = Expression.Lambda<Func<object, object, TResult>>(body, parameters);
        var method = lambda.Compile();

        return method;
    }

    public static Func<object, object, object, TResult> CompileConstructor<TResult>(Type typeOfTResult,
        Type typeOfT1,
        Type typeOfT2,
        Type typeOfT3)
    {
        var constructorArgumentTypes = new[] { typeOfT1, typeOfT2, typeOfT3 };
        var parameters = constructorArgumentTypes.Select(_ => Expression.Parameter(typeof(object))).ToArray();
        var constructor = typeOfTResult.GetConstructor(constructorArgumentTypes);
        ArgumentNullException.ThrowIfNull(constructor);

        var constructorArguments = new Expression[]
        {
            Expression.Convert(parameters[0], typeOfT1),
            Expression.Convert(parameters[1], typeOfT2),
            Expression.Convert(parameters[2], typeOfT3)
        };

        var body = Expression.New(constructor, constructorArguments);
        var lambda = Expression.Lambda<Func<object, object, object, TResult>>(body, parameters);
        var method = lambda.Compile();

        return method;
    }

    public static Func<object, object, object, object, TResult> CompileConstructor<TResult>(Type typeOfTResult,
        Type typeOfT1,
        Type typeOfT2,
        Type typeOfT3,
        Type typeOfT4)
    {
        var constructorArgumentTypes = new[] { typeOfT1, typeOfT2, typeOfT3, typeOfT4 };
        var parameters = constructorArgumentTypes.Select(_ => Expression.Parameter(typeof(object))).ToArray();
        var constructor = typeOfTResult.GetConstructor(constructorArgumentTypes);
        ArgumentNullException.ThrowIfNull(constructor);

        var constructorArguments = new Expression[]
        {
            Expression.Convert(parameters[0], typeOfT1),
            Expression.Convert(parameters[1], typeOfT2),
            Expression.Convert(parameters[2], typeOfT3),
            Expression.Convert(parameters[3], typeOfT4)
        };

        var body = Expression.New(constructor, constructorArguments);
        var lambda = Expression.Lambda<Func<object, object, object, object, TResult>>(body, parameters);
        var method = lambda.Compile();

        return method;
    }

    public static Func<T1, T2, TResult> CompileConstructor<T1, T2, TResult>(Type typeOfTResult,
        Type typeOfT1,
        Type typeOfT2)
    {
        var inputArgumentTypes = new[] { typeof(T1), typeof(T2) };
        var constructorArgumentTypes = new[] { typeOfT1, typeOfT2 };
        var parameters = constructorArgumentTypes.Select(Expression.Parameter).ToArray();
        var constructor = typeOfTResult.GetConstructor(constructorArgumentTypes);
        ArgumentNullException.ThrowIfNull(constructor);

        //var constructorArguments = new Expression[] { Expression.Convert(parameters[0], typeOfT1), Expression.Convert(parameters[1], typeOfT2) };
        var constructorArguments = new Expression[parameters.Length];
        for (var i = 0; i < constructorArguments.Length; i++)
        {
            if (constructorArgumentTypes[i] == inputArgumentTypes[i])
            {
                constructorArguments[i] = parameters[i];
            }
            else
            {
                constructorArguments[i] = Expression.Convert(parameters[1], constructorArgumentTypes[i]);
            }
        }

        var body = Expression.New(constructor, constructorArguments);
        var lambda = Expression.Lambda<Func<T1, T2, TResult>>(body, parameters);
        var method = lambda.Compile();

        return method;
    }

    //public static Func<T1, T2, T3, T4, T5, TResult> CompileConstructor<T1, T2, T3, T4, T5, TResult>(
    //    Type typeOfT1Impl,
    //    Type typeOfT2Impl,
    //    Type typeOfT3Impl,
    //    Type typeOfT4Impl,
    //    Type typeOfT5Impl,
    //    Type typeOfTResult)
    //{
    //    var implTypes = new[] { typeOfT1Impl, typeOfT2Impl, typeOfT3Impl, typeOfT4Impl, typeOfT5Impl };
    //    var inputTypes = new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) };
    //    var constructor = typeOfTResult.GetConstructor(implTypes);
    //    ArgumentNullException.ThrowIfNull(constructor);

    //    var parameters = inputTypes.Select(Expression.Parameter).ToArray();
    //    var constructorArguments = new Expression[implTypes.Length];
    //    for (var i = 0; i < constructorArguments.Length; i++)
    //    {
    //        if (inputTypes[i] == implTypes[i])
    //        {
    //            constructorArguments[i] = parameters[i];
    //        }
    //        else
    //        {
    //            constructorArguments[i] = Expression.Convert(parameters[i], implTypes[i]);
    //        }
    //    }
    //    var body = Expression.New(constructor, constructorArguments);
    //    var lambda = Expression.Lambda<Func<T1, T2, T3, T4, T5, TResult>>(body, parameters);
    //    return lambda.Compile();
    //}

    public static Func<T1, T2, T3, T4, T5, TResult> CompileConstructor<T1, T2, T3, T4, T5, TResult>(
        Type? typeOfT1Impl = null,
        Type? typeOfT2Impl = null,
        Type? typeOfT3Impl = null,
        Type? typeOfT4Impl = null,
        Type? typeOfT5Impl = null,
        Type? typeOfTResultImpl = null
    )
    {
        var inputTypes = new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) };
        var implTypes = new[] { typeOfT1Impl, typeOfT2Impl, typeOfT3Impl, typeOfT4Impl, typeOfT5Impl };
        var constructArgumentTypes = new Type[inputTypes.Length];
        for (var i = 0; i < constructArgumentTypes.Length; i++)
        {
            constructArgumentTypes[i] = implTypes[i] ?? inputTypes[i];
        }

        var typeOfResult = typeOfTResultImpl ?? typeof(TResult);

        var constructor = typeOfResult.GetConstructor(constructArgumentTypes);
        if (constructor == null)
        {
            constructor = typeOfResult.GetConstructors()[0];
        }

        ArgumentNullException.ThrowIfNull(constructor);

        var parameters = inputTypes.Select(Expression.Parameter).ToArray();
        var constructorArguments = new Expression[inputTypes.Length];
        for (var i = 0; i < constructorArguments.Length; i++)
        {
            constructorArguments[i] =
                implTypes[i] == null ? parameters[i] : Expression.Convert(parameters[i], implTypes[i]!);
        }

        var body = Expression.New(constructor, constructorArguments);
        var lambda = Expression.Lambda<Func<T1, T2, T3, T4, T5, TResult>>(body, parameters);
        return lambda.Compile();
    }
}
