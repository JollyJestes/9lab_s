using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

public class DataSerializer<T>
{
    public void SerializeBinary(List<T> data, string fileName)
    {
        try
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, data);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сериализации в бинарном формате: {ex.Message}");
        }
    }

    public List<T> DeserializeBinary(string fileName)
    {
        try
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (List<T>)formatter.Deserialize(fs);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при десериализации из бинарного формата: {ex.Message}");
            return new List<T>();
        }
    }

    public void SerializeJson(List<T> data, string fileName)
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(fileName, jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сериализации в JSON: {ex.Message}");
        }
    }

    public List<T> DeserializeJson(string fileName)
    {
        try
        {
            string jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<T>>(jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при десериализации из JSON: {ex.Message}");
            return new List<T>();
        }
    }

    public void SortAndPrint(List<T> data, Comparison<T> comparison)
    {
        data.Sort(comparison);

        Console.WriteLine("Отсортированные данные:");
        foreach (var item in data)
        {
            Console.WriteLine(item);
        }
    }

    public List<T> Filter(List<T> data, Func<T, bool> predicate)
    {
        return data.Where(predicate).ToList();
    }

    public void PrintFilteredData(List<T> filteredData)
    {
        Console.WriteLine("Отфильтрованные данные:");
        foreach (var item in filteredData)
        {
            Console.WriteLine(item);
        }
    }
}