namespace Aoc2024_Day23;

internal static class DictionaryExtensions
{
    public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory) where TKey : notnull
    {
        if (dictionary.TryGetValue(key, out var value)) return value;
        return dictionary[key] = valueFactory();
    }
}
