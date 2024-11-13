public static class StringExtensions
{
    public static string Invert(this string str)
    {
        char[] charArray = str.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
    
    public static int CountOccurrences(this string str, char symbol)
    {
        int count = 0;
        foreach (var ch in str)
        {
            if (ch == symbol)
            {
                count++;
            }
        }
        return count;
    }
}

public static class ArrayExtensions
{
    
    public static int CountOccurrences<T>(this T[] array, T value) where T : IEquatable<T>
    {
        return array.Count(item => item.Equals(value));
    }

    
    public static T[] GetUniqueElements<T>(this T[] array)
    {
        return array.Distinct().ToArray();
    }
}

public class ExtendedDictionaryElement<T, U, V>
{
    public T Key { get; set; }
    public U Value1 { get; set; }
    public V Value2 { get; set; }

    public ExtendedDictionaryElement(T key, U value1, V value2)
    {
        Key = key;
        Value1 = value1;
        Value2 = value2;
    }
}

public class ExtendedDictionary<T, U, V> : IEnumerable<ExtendedDictionaryElement<T, U, V>>
{
    private List<ExtendedDictionaryElement<T, U, V>> elements = new List<ExtendedDictionaryElement<T, U, V>>();

    
    public void Add(T key, U value1, V value2)
    {
        elements.Add(new ExtendedDictionaryElement<T, U, V>(key, value1, value2));
    }
    
    public bool Remove(T key)
    {
        var element = elements.Find(e => EqualityComparer<T>.Default.Equals(e.Key, key));
        if (element != null)
        {
            elements.Remove(element);
            return true;
        }
        return false;
    }
    
    public bool ContainsKey(T key)
    {
        return elements.Any(e => EqualityComparer<T>.Default.Equals(e.Key, key));
    }

    
    public bool ContainsValue(U value1, V value2)
    {
        return elements.Any(e => EqualityComparer<U>.Default.Equals(e.Value1, value1) && EqualityComparer<V>.Default.Equals(e.Value2, value2));
    }

    
    public ExtendedDictionaryElement<T, U, V> this[T key]
    {
        get
        {
            return elements.Find(e => EqualityComparer<T>.Default.Equals(e.Key, key));
        }
    }
    
    public int Count => elements.Count;

   
    public IEnumerator<ExtendedDictionaryElement<T, U, V>> GetEnumerator()
    {
        return elements.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class Program
{
    public static void Main()
    {
      
        string str = "hello world";
        Console.WriteLine(str.Invert()); 
        Console.WriteLine(str.CountOccurrences('o')); 
        
        int[] numbers = { 1, 2, 2, 3, 3, 3, 4 };
        Console.WriteLine(numbers.CountOccurrences(3));  
        var uniqueNumbers = numbers.GetUniqueElements();
        Console.WriteLine(string.Join(", ", uniqueNumbers));
        
        var extendedDict = new ExtendedDictionary<int, string, double>();
        extendedDict.Add(1, "Value1", 10.5);
        extendedDict.Add(2, "Value2", 20.5);

        Console.WriteLine(extendedDict.Count); 
        Console.WriteLine(extendedDict.ContainsKey(1));  
        Console.WriteLine(extendedDict.ContainsValue("Value2", 20.5));  
        bool removed = extendedDict.Remove(2);
        Console.WriteLine($"\nElement removed: {removed}");
        
        var element = extendedDict[1];
        Console.WriteLine($"{element.Key} - {element.Value1} - {element.Value2}");
        
        foreach (var elem in extendedDict)
        {
            Console.WriteLine($"{elem.Key}: {elem.Value1}, {elem.Value2}");
        }
    }
}


