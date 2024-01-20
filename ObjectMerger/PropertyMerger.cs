using System.Dynamic;

namespace ObjectMerger;

public static class PropertyMerger
{
    public static ExpandoObject CreateFromList(IEnumerable<string> properties, string className = "Detail")
    {
        var ps = properties.Select(p => new KeyValuePair<string, object?>(p, null));
        return CreateDynamicObject(className, ps);
    }
    public static ExpandoObject MergeWithList<T>(T t, string newClassName, IEnumerable<string> properties, IEnumerable<string>? excludedProperties = null)
    {
        var dt = CreateFromList(properties);
        return ObjectMerge(t, dt, newClassName, excludedProperties);
    }
    public static ExpandoObject ObjectMerge<T1, T2>(T1 t1, T2 t2, string newClassName, IEnumerable<string>? t1excludedProperties = null, IEnumerable<string>? t2excludedProperties = null)
    {
        var ps = new Dictionary<string, object?>[] { GetProperties(t1, t1excludedProperties), GetProperties(t2, t2excludedProperties) };
        var properties = ps.SelectMany(dict => dict);
        return CreateDynamicObject(newClassName, properties);
    }

    public static Dictionary<string, object?> GetProperties<T>(T t, IEnumerable<string>? excludedProperties = null) =>
        typeof(T).GetProperties()
        .Where(p => excludedProperties == null || !excludedProperties.Contains(p.Name))
        .ToDictionary(k => k.Name, v => v.GetValue(t));

    public static ExpandoObject CreateDynamicObject(string className,
    IEnumerable<KeyValuePair<string, object?>> properties)
    {
        dynamic obj = new ExpandoObject();
        obj.ClassName = className;
        AssignProperties(obj, properties);
        return obj;
    }

    public static void AssignProperties(ExpandoObject obj, IEnumerable<KeyValuePair<string, object?>> properties)
    {
        foreach (var item in properties)
            ((IDictionary<string, object?>)obj)[item.Key] = item.Value;
    }
}