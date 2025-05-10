using System.Text.Json;

namespace cours_project;
public class FlatStorageService
{
    private readonly string _filePath;

    public FlatStorageService(string filePath)
    {
        _filePath = filePath;
        if (!File.Exists(_filePath)) File.Create(_filePath).Close();
    }

    public Flat[] ReadAllFlats()
    {
        var flats = new List<Flat>();
        var lines = File.ReadAllLines(_filePath);
        foreach (var line in lines)
        {
            try
            {
                var flat = JsonSerializer.Deserialize<Flat>(line);
                if (flat != null) flats.Add(flat);
            }
            catch (Exception ex) { ConsoleMessages.WriteErrorMessage($"Ошибка при чтении: {ex.Message}"); }
        }
        return flats.ToArray();
    }

    public void SaveAllFlats(Flat[] flats)
    {
        var jsonLines = new List<string>();
        foreach (var flat in flats)
        {
            string jsonLine = JsonSerializer.Serialize(flat, JsonSettings.LinesOptions);
            jsonLines.Add(jsonLine);
        }
        File.WriteAllLines(_filePath, jsonLines);
    }

    public void AddFlat(Flat flat)
    {
        string json = JsonSerializer.Serialize(flat, JsonSettings.LinesOptions) + Environment.NewLine;
        File.AppendAllText(_filePath, json);
    }
}
