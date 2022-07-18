using System.Dynamic;

namespace ObjectMerger;

public class PropertyMerger
{
    public static ExpandoObject ObjectMerge<T1, T2>(T1 t1, T2 t2, string newClassName, IEnumerable<string>? t1ExcludedProperies = null, IEnumerable<string>? t2ExcludedProperies = null)
    {
        var ps = new Dictionary<string, object?>[] { GetProperties(t1, t1ExcludedProperies), GetProperties(t2, t2ExcludedProperies) };
        var properties = ps.SelectMany(dict => dict);
        return CreateDynamicObject(newClassName, properties);
    }

    public static Dictionary<string, object?> GetProperties<T>(T t, IEnumerable<string>? excludedProperies = null) =>
        t.GetType().GetProperties()
        .Where(p => excludedProperies == null || !excludedProperies.Contains(p.Name))
        .ToDictionary(k => k.Name, v => v.GetValue(t));

    public static ExpandoObject CreateDynamicObject(string className,
    IEnumerable<KeyValuePair<string, object?>> properties)
    {
        dynamic obj = new ExpandoObject();
        obj.ClassName = className;
        AssignProperties(obj, properties);
        return obj;
    }

    private static void AssignProperties(ExpandoObject obj, IEnumerable<KeyValuePair<string, object?>> properties)
    {
        foreach (var item in properties)
            ((IDictionary<string, object?>)obj)[item.Key] = item.Value;
    }
}