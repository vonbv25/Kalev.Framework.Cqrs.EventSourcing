

public static class DataMapper<T> where T : new()
{
    //private static Dictionary<Type, Dictionary<string, PropertyInfo>> _propertyReflectionCache = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
    //private static Dictionary<Type, FieldInfo[]> _fieldReflectionCache = new Dictionary<Type, FieldInfo[]>();
    //public static T MapFrom(IAggregateRoot aggregateRoot)
    //{
    //    T TDataModel = new T();

    //    PropertyInfo[] propertyInfos = GetPropertyInfo

    //    //aggregateRoot.GetType()
    //    //                .GetFields()
    //    //                .Where(m => m.GetCustomAttributes(typeof(DataPropertyAttribute), false)
    //    //                .Count() > 0)
    //    //                .ToList()
    //    //                .ForEach(property => {

    //    //                    var dataModelProperty = TDataModel.GetType().GetProperty(property.Name);
    //    //                    dataModelProperty.SetValue(property.va)

    //    //                });

    //}

    ///// <summary>
    ///// Finds a property in the reflection cache
    ///// </summary>
    //private static PropertyInfo GetPropertyInfo(Type t, string name)
    //{
    //    if (_propertyReflectionCache.ContainsKey(t))
    //    {
    //        if (_propertyReflectionCache[t].ContainsKey(name))
    //        {
    //            return _propertyReflectionCache[t][name];
    //        }

    //        _propertyReflectionCache[t].Add(name, t.GetProperty(name, BindingFlags.Public | BindingFlags.Instance));
    //        return _propertyReflectionCache[t][name];
    //    }

    //    Dictionary<string, PropertyInfo> dictionary = new Dictionary<string, PropertyInfo>();
    //    _propertyReflectionCache.Add(t, dictionary);
    //    dictionary.Add(name, t.GetProperty(name, BindingFlags.Public | BindingFlags.Instance));

    //    return _propertyReflectionCache[t][name];
    //}

    ///// <summary>
    ///// Finds a collection of field information from the reflection cache
    ///// </summary>
    //private static FieldInfo[] GetFieldSetInfo(Type t)
    //{
    //    if (_fieldReflectionCache.ContainsKey(t))
    //    {
    //        return _fieldReflectionCache[t];
    //    }

    //    _fieldReflectionCache.Add(t, t.GetFields(BindingFlags.Public | BindingFlags.Instance));

    //    return _fieldReflectionCache[t];
    //}



}