using System.Text.Json;
using System.IO;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Reflection;

namespace cours_project;

public class FlatManager
{
    private readonly FlatStorageService _storageService;
    private Flat[] cachedFlats;
     public FlatManager(string filePath)
    {
        _storageService = new FlatStorageService(filePath);
        cachedFlats = _storageService.ReadAllFlats();
    }

    public void ShowAllFlats()
    {
        if (!cachedFlats.Any())
        {
            ConsoleMessages.WriteErrorMessage("Нет данных для отображения.");
            return;
        }

       Console.WriteLine("Список квартир:");
        for (int i = 0; i < cachedFlats.Length; i++) Console.WriteLine($"{i + 1}. {cachedFlats[i].Info()}");
    }

    public void ShowChart()
    {
        FlatChartService.ShowChart(cachedFlats);
    }

    
    public void AddFlat()
    {
        Flat flat = new Flat();
        InputService.InputFlat(flat);

        Flat[] newFlats = new Flat[cachedFlats.Length + 1];
        for (int i = 0; i < cachedFlats.Length; i++)
        {
            newFlats[i] = cachedFlats[i];
        }
        newFlats[newFlats.Length - 1] = flat;

        cachedFlats = newFlats;
        _storageService.SaveAllFlats(cachedFlats);

        ConsoleMessages.WriteSuccessMessage("Данные успешно добавлены.");
    }
    
    public void UpdateFlat()
    {
        if (cachedFlats.Length < 1) {ConsoleMessages.WriteErrorMessage("Нет данных для обновления."); return;}
        ShowAllFlats();
        int index = InputService.InputIndex(cachedFlats.Length);

        Flat flat = new Flat();
        InputService.InputFlat(flat);

        cachedFlats[index] = flat;
        _storageService.SaveAllFlats(cachedFlats);

        ConsoleMessages.WriteSuccessMessage("Данные успешно обновлены.");
    }

    public void DeleteFlat()
    {
        if (cachedFlats.Length < 1) {ConsoleMessages.WriteErrorMessage("Нет данных для удаления."); return;}
        
        ShowAllFlats();
        int index = InputService.InputIndex(cachedFlats.Length);

        Flat[] newFlats = new Flat[cachedFlats.Length - 1];

        int newIndex = 0;

        for (int i = 0; i < cachedFlats.Length; i++)
        {
            if (i == index) continue;
            newFlats[newIndex++] = cachedFlats[i];
        }
        cachedFlats = newFlats;
        _storageService.SaveAllFlats(newFlats);

        ConsoleMessages.WriteSuccessMessage("Данные успешно удалены.");
    }

    public void SearchFlats()
    {
        Console.Clear();
        if (cachedFlats.Length < 1)
        {
            ConsoleMessages.WriteErrorMessage("Нет данных для поиска.");
            return;
        }

        Console.WriteLine("Введите номер параметра по которому хотите выполнить поиск:");
        Console.WriteLine(cachedFlats[0].GetPropertiesNames());

        int index = InputService.InputIndex(cachedFlats[0].GetPropertiesCount());
        PropertyInfo[] properties = Flat.GetProperties();
        PropertyInfo selectedProperty = properties[index];

        Console.WriteLine($"Введите значение для поиска:");
        string searchValue = InputService.InputString();

        var matchedFlats = cachedFlats.Where(flat =>
        {
            var propertyValue = selectedProperty.GetValue(flat)?.ToString();
            return propertyValue != null && propertyValue.Contains(searchValue, StringComparison.OrdinalIgnoreCase);
        }).ToArray();

        if (matchedFlats.Any())
        {
            ConsoleMessages.WriteSuccessMessage("Найденные квартиры:");
            foreach (var flat in matchedFlats) Console.WriteLine(flat.Info());
        }
        else ConsoleMessages.WriteErrorMessage("По заданному параметру ничего не найдено.");
    }

    public void SotrFlats()
    {
        if (cachedFlats.Length < 2) {ConsoleMessages.WriteErrorMessage("Нет данных для сортировки."); return;}

        Console.WriteLine("Введите номер параметра по которому хотите выполнить сортировку:");
        Console.WriteLine(cachedFlats[0].GetPropertiesNames());

        int index = InputService.InputIndex(cachedFlats[0].GetPropertiesCount());
        Sorter.InsertionSort(cachedFlats,index);

        _storageService.SaveAllFlats(cachedFlats);
    }
}
