using System.Text.Json;

namespace cours_project;

public class FlatStorage
{
    private readonly string _filePath = "data.json";

    public List<Flat> ReadAll()
    {
        if (!File.Exists(_filePath)) return new List<Flat>();

        string json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Flat>>(json) ?? new List<Flat>();
    }

    public void SaveAll(List<Flat> flats)
    {
        string json = JsonSerializer.Serialize(flats, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}
