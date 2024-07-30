using System;
using System.Linq;
using System.Reflection;
using System.Text;

public class CsvSerializer
{
    public static string Serialize<T>(T obj)
    {
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var sb = new StringBuilder();
        foreach (var prop in properties)
        {
            var value = prop.GetValue(obj)?.ToString() ?? string.Empty;
            sb.Append(value).Append(",");
        }
        if (sb.Length > 0)
            sb.Length--;
        return sb.ToString();
    }

    public static T Deserialize<T>(string csv) where T : new()
    {
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var values = csv.Split(',');
        var obj = new T();
        for (int i = 0; i < properties.Length; i++)
        {
            var prop = properties[i];
            var value = Convert.ChangeType(values[i], prop.PropertyType);
            prop.SetValue(obj, value);
        }
        return obj;
    }
}
