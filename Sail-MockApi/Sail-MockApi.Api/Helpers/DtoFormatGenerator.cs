using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Sail_MockApi.Api.Helpers;

public static class DtoFormatGenerator
{
    public static object CreateFormat(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        //. Create instance of provided type
        dynamic? instance = Activator.CreateInstance(type);
        
        //. Set default values for the properties using reflection
        SetDefaultValues(instance);
        
        //. Serialize the instance to JSON string
        string json = JsonConvert.SerializeObject(instance, Formatting.Indented);

        return instance;
    }

    private static void SetDefaultValues(object? instance)
    {
        if (instance == null) return;

        // Get all properties of the type
        var properties = instance.GetType().GetProperties();

        foreach (var property in properties)
        {
            // Skip properties that cannot be written to or are null
            if (!property.CanWrite) continue;

            // Check if the property is a collection type (like List<T>, IEnumerable<T>)
            if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string))
            {
                // Create an instance of the collection
                var listType = typeof(List<>).MakeGenericType(property.PropertyType.GetGenericArguments()[0]);
                var listInstance = Activator.CreateInstance(listType);
                property.SetValue(instance, listInstance);

                // Populate the list with default items
                PopulateListWithDefaults(listInstance, property.PropertyType.GetGenericArguments()[0]);
            }
            else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
            {
                // If the property is a reference type (and not a string), create an instance and recursively set default values
                var nestedInstance = Activator.CreateInstance(property.PropertyType);
                property.SetValue(instance, nestedInstance);
                SetDefaultValues(nestedInstance); // Recursively set default values for nested properties
            }
            else
            {
                // For value types (or strings), just set the default value
                property.SetValue(instance, GetDefaultValue(property.PropertyType));
            }
        }
    }
    private static void PopulateListWithDefaults(object? listInstance, Type itemType)
    {
        // Check if it's a List<T>
        if (listInstance is IList list)
        {
            // Add a default instance of the item type to the list
            var defaultItem = Activator.CreateInstance(itemType);
            list.Add(defaultItem);
        }
    }

    private static object? GetDefaultValue(Type type)
    {
        if (type.IsValueType)
        {
            return Activator.CreateInstance(type);
        }

        return null;
    }
}