using Dm;
using Microsoft.EntityFrameworkCore.Dm.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Reflection;

namespace Acme.BookStore.EntityFrameworkCore;

public class MyDmDateTimeOffsetTypeMapping : DmDateTimeOffsetTypeMapping
{
    public MyDmDateTimeOffsetTypeMapping([JetBrains.Annotations.NotNull] string storeType, DbType? dbType = System.Data.DbType.DateTimeOffset)
        : base(storeType, dbType)
    {
    }

    protected MyDmDateTimeOffsetTypeMapping(RelationalTypeMappingParameters parameters)
        : base(parameters)
    {
    }

    protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
    {
        return new MyDmDateTimeOffsetTypeMapping(parameters);
    }

    public override MethodInfo GetDataReaderMethod()
    {
        return typeof(DmDataReader).GetRuntimeMethod(nameof(DmDataReader.GetDateTimeOffset), new[] { typeof(int) });
    }
}