using Microsoft.EntityFrameworkCore.Dm.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;

namespace Acme.BookStore.EntityFrameworkCore;

public class MyDmTypeMappingSource : DmTypeMappingSource
{
    private MyDmDateTimeOffsetTypeMapping _datetimeoffset = new MyDmDateTimeOffsetTypeMapping("DATETIME WITH TIME ZONE", DbType.DateTimeOffset);
    private MyDmDateTimeOffsetTypeMapping _datetimeoffset3 = new MyDmDateTimeOffsetTypeMapping("DATETIME(3) WITH TIME ZONE", DbType.DateTimeOffset);
    private MyDmTimeSpanTypeMapping _intervaldt = new MyDmTimeSpanTypeMapping("INTERVAL DAY TO SECOND");
    private Dictionary<string, RelationalTypeMapping> _storeTypeMappings;
    private Dictionary<Type, RelationalTypeMapping> _clrTypeMappings;

    public MyDmTypeMappingSource([JetBrains.Annotations.NotNull] TypeMappingSourceDependencies dependencies, [JetBrains.Annotations.NotNull] RelationalTypeMappingSourceDependencies relationalDependencies)
            : base(dependencies, relationalDependencies)
    {
        _storeTypeMappings = new Dictionary<string, RelationalTypeMapping>(StringComparer.OrdinalIgnoreCase)
    {
        { "datetime with time zone", _datetimeoffset },
        { "timestamp with time zone", _datetimeoffset },
        { "datetime(3) with time zone", _datetimeoffset3 },
        { "timestamp(3) with time zone", _datetimeoffset3 },
        { "interval day to second", _intervaldt },
    };
        _clrTypeMappings = new Dictionary<Type, RelationalTypeMapping>
    {
        { typeof(DateTimeOffset), _datetimeoffset },
        { typeof(TimeSpan), _intervaldt }
    };
    }

    protected override RelationalTypeMapping FindMapping(in RelationalTypeMappingInfo mappingInfo)
    {
        Type clrType = mappingInfo.ClrType;
        string storeTypeName = mappingInfo.StoreTypeName;
        string storeTypeNameBase = mappingInfo.StoreTypeNameBase;
        if (storeTypeName != null && _storeTypeMappings.ContainsKey(storeTypeName))
            return _storeTypeMappings.GetValueOrDefault(storeTypeName)?.Clone(mappingInfo);
        if (clrType != null && _clrTypeMappings.ContainsKey(clrType))
            return _clrTypeMappings.GetValueOrDefault(clrType)?.Clone(mappingInfo);

        return base.FindMapping(mappingInfo);
    }
}