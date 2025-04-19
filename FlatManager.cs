namespace cours_project;

public class FlatManager
{
    private readonly FlatStorage _storage = new();
    private readonly FlatSorter _sorter = new();
    private readonly FlatChart _chart = new();

    public void Create()
    {
        Flat flat = new Flat();
        InputData(flat);
        Console.Write("\nДля отмены операции введите '0'. ");

        var flats = _storage.ReadAll();

        if (flats.Any(existing => IsDuplicate(existing, flat)))
        {
            Console.WriteLine("Запись уже существует.");
            return;
        }

        string confirmation = Console.ReadLine();
        if (confirmation == "0")
        {
            Console.WriteLine("Добавление записи отменено.");
            return;
        }

        flats.Add(flat);
        _storage.SaveAll(flats);
        Console.WriteLine("Запись успешно добавлена.");
    }

    public bool Read()
    {
        var flats = _storage.ReadAll();

        if (flats.Any())
        {
            Console.WriteLine("Список квартир:");
            for (int i = 0; i < flats.Count; i++) Console.WriteLine($"{i + 1}. {flats[i].Info()}");
            return true;
        }

        Console.WriteLine("Данные отсутствуют.");
        return false;
    }

    public void Update()
    {
        var flats = _storage.ReadAll();

        if (!Read()) return;

        Console.Write("\nДля отмены операции введите '0'. ");
        Console.Write("\nВведите номер записи для редактирования: ");

        if (!TryGetIndex(flats.Count, out int index)) return;

        Flat updatedFlat = new Flat();
        InputData(updatedFlat);
        flats[index] = updatedFlat;

        _storage.SaveAll(flats);
        Console.WriteLine("Запись успешно обновлена.");
    }

    public void Delete()
    {
        var flats = _storage.ReadAll();

        if (!Read()) return;

        Console.Write("\nДля отмены операции введите '0'. ");
        Console.Write("\nВведите номер записи для удаления: ");

        if (!TryGetIndex(flats.Count, out int index)) return;

        flats.RemoveAt(index);
        _storage.SaveAll(flats);
        Console.WriteLine("Запись успешно удалена.");
    }

    public void ShowConsoleChart()
    {
        var flats = _storage.ReadAll();
        _chart.Display(flats);
    }

    public void SortByParameter()
    {
        var flats = _storage.ReadAll();

        if (!flats.Any())
        {
            Console.WriteLine("Нет данных для сортировки.");
            return;
        }

        Console.WriteLine("Выберите параметр для сортировки:");
        Console.WriteLine("1. ФИО арендатора");
        Console.WriteLine("2. Адрес квартиры");
        Console.WriteLine("3. Количество жильцов");
        Console.WriteLine("4. Тариф за человека");
        Console.WriteLine("0. Отмена");

        string? choice = Console.ReadLine();

        if (choice == "0")
        {
            Console.WriteLine("Сортировка отменена.");
            return;
        }

        bool sorted = _sorter.Sort(flats, choice);
        if (sorted)
        {
            _storage.SaveAll(flats);
            Console.WriteLine("Сортировка выполнена и сохранена.");
        }
        else Console.WriteLine("Некорректный выбор.");
    }

    public void SearchFlats()
    {
        Console.WriteLine("Выберите параметр для поиска:");
        Console.WriteLine("1. ФИО арендатора");
        Console.WriteLine("2. Адрес квартиры");
        Console.WriteLine("3. Количество жильцов");
        Console.WriteLine("0. Отмена");

        string? choice = Console.ReadLine();
        var flats = _storage.ReadAll();

        switch (choice)
        {
            case "1":
                Console.WriteLine("Введите ФИО арендатора:");
                string name = Console.ReadLine()?.ToLower() ?? "";
                PrintSearchResult(flats.Where(f => f.TenantFullName.ToLower().Contains(name)).ToList());
                break;

            case "2":
                Console.WriteLine("Введите адрес квартиры:");
                string address = Console.ReadLine()?.ToLower() ?? "";
                PrintSearchResult(flats.Where(f => f.FlatAddress.ToLower().Contains(address)).ToList());
                break;

            case "3":
                Console.WriteLine("Введите количество жильцов:");
                if (int.TryParse(Console.ReadLine(), out int peopleCount))
                    PrintSearchResult(flats.Where(f => f.PeopleCount == peopleCount).ToList());
                else Console.WriteLine("Ошибка: введённое значение не является числом.");
                break;

            case "0":
                Console.WriteLine("Поиск отменён.");
                break;

            default:
                Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                break;
        }
    }

    // --- Вспомогательные методы --- //

    private static void InputData(Flat flat)
    {
        while (true)
        {
            Console.WriteLine("Введите ФИО арендатора через пробел:");
            try { flat.TenantFullName = Console.ReadLine(); break; }
            catch (ArgumentException e) { Console.WriteLine(e.Message); }
        }

        while (true)
        {
            Console.WriteLine("Введите адрес квартиры:");
            try { flat.FlatAddress = Console.ReadLine(); break; }
            catch (ArgumentException e) { Console.WriteLine(e.Message); }
        }

        while (true)
        {
            Console.WriteLine("Введите количество жильцов:");
            try { flat.PeopleCount = Convert.ToInt32(Console.ReadLine()); break; }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        while (true)
        {
            Console.WriteLine("Введите тариф за человека:");
            try { flat.TariffPerPerson = Convert.ToDouble(Console.ReadLine()); break; }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }

    private static bool TryGetIndex(int count, out int index)
    {
        string? input = Console.ReadLine();
        if (input == "0")
        {
            Console.WriteLine("Операция отменена.");
            index = -1;
            return false;
        }

        bool isNumber = int.TryParse(input, out index);

        if (!isNumber || index < 1 || index > count)
        {
            Console.WriteLine($"Ошибка: введите число от 1 до {count}.");
            return TryGetIndex(count, out index);
        }

        index--; // коррекция индекса
        return true;
    }

    private static void PrintSearchResult(List<Flat> results)
    {
        if (results.Any())
        {
            Console.WriteLine("\nНайденные квартиры:");
            foreach (var flat in results) Console.WriteLine(flat.Info());
        }
        else Console.WriteLine("По вашему запросу ничего не найдено.");
    }

    private static bool IsDuplicate(Flat a, Flat b)
    {
        return a.TenantFullName == b.TenantFullName &&
               a.FlatAddress == b.FlatAddress &&
               a.PeopleCount == b.PeopleCount &&
               a.TariffPerPerson == b.TariffPerPerson;
    }
}
