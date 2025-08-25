using Dm;
using Microsoft.EntityFrameworkCore.Dm.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace Acme.BookStore.EntityFrameworkCore;

public class MyDmTimeSpanTypeMapping : DmTimeSpanTypeMapping
{
    public MyDmTimeSpanTypeMapping([JetBrains.Annotations.NotNull] string storeType, DbType? dbType = null)
        : base(storeType, dbType)
    {
    }

    protected MyDmTimeSpanTypeMapping(RelationalTypeMappingParameters parameters)
        : base(parameters)
    {
    }

    protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
    {
        return new MyDmTimeSpanTypeMapping(parameters);
    }

    public override MethodInfo GetDataReaderMethod()
    {
        return typeof(DmDataReader).GetRuntimeMethod(nameof(DmDataReader.GetDouble), new[] { typeof(int) });
    }

    public override Expression CustomizeDataReaderExpression(Expression expression)
    {
        return Expression.Call(GetTimeSpanMethod, expression);
    }

    private static readonly MethodInfo GetTimeSpanMethod
        = typeof(MyDmTimeSpanTypeMapping).GetMethod(nameof(GetTimeSpan), new[] { typeof(double) })!;

    public static TimeSpan GetTimeSpan(double value)
    {
        return TimeSpan.FromDays(value);
    }
}