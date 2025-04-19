// using System.Text.Json;
// using System.IO;
// using System.Text.RegularExpressions;
// using System.ComponentModel;
// using System.Reflection;

// // namespace cours_project;

// public class FlatManager
// {
//     private const string filePath = @"..\..\..\file.json";

//     public FlatManager() 
//     {
//         EnsureFileExists();
//     }

//     private void EnsureFileExists() 
//     {
//         if (!File.Exists(filePath)) File.Create(filePath).Close();
//     }

//     public void Create()
//     {
//         Flat flat = new Flat();
//         InputData(flat);
//         Console.Write("\nДля отмены операции введите '0'. ");

//         var flats = ReadAllFlats();

//         foreach (var existingFlat in flats)
//         {
//             if (IsDuplicate(existingFlat, flat))
//             {
//                 Console.WriteLine("Запись уже существует.");
//                 return;
//             }
//         }

//         string confirmation = Console.ReadLine();
//         if (confirmation == "0")
//         {
//             Console.WriteLine("Добавление записи отменено.");
//             return;
//         }
//         else
//         {
//             try
//             {
//                 string jsonFlat = JsonSerializer.Serialize(flat, JsonSettings.LinesOptions) + Environment.NewLine;
//                 File.AppendAllText(filePath, jsonFlat);
//                 Console.WriteLine("Запись успешно добавлена.");
//             }
//             catch (IOException exception) {Console.WriteLine($"Ошибка при записи в файл: {exception.Message}");}
//         }
//     }

//     public bool Read()
//     {
//         var flats = ReadAllFlats();
//         if (flats.Any())
//         { 
//             Console.WriteLine("Список квартир:");
//             for (int i = 0; i < flats.Count; i++) Console.WriteLine($"{i + 1}. {flats[i].Info()}");
//             return true;
//         }
//         else 
//         {
//             Console.WriteLine("Данные отсутствуют.");
//             return false;
//         }
        
//     }

//     public void Update()
//     {
//         var flats = ReadAllFlats();

//         if (!Read()) return;

//         int index;
//         while (true)
//         {
//             Console.Write("\nДля отмены операции введите '0'. ");
//             Console.Write("\nВведите номер записи для редактирования: ");
//             string input = Console.ReadLine();

//             if (input == "0")
//             {
//                 Console.WriteLine("Изменение отменено.");
//                 return;
//             }

//             bool isNumber = int.TryParse(input, out index);

//             if (!isNumber) Console.WriteLine("Ошибка: введённое значение не является числом.");
//             else
//             {
//                 if (index >= 1 && index <= flats.Count) {index--; break;}
//                 else Console.WriteLine("Ошибка: номер должен быть в диапазоне от 1 до " + flats.Count);
//             }

//         }

//         Flat updatedFlat = new Flat();
//         InputData(updatedFlat);
//         flats[index] = updatedFlat;

//         SaveAllFlats(flats);
//         Console.WriteLine("Запись успешно обновлена.");
//     }


//     public void Delete()
//     {
//         var flats = ReadAllFlats();

//         if (!Read()) return;

//         int index;
//         while (true)
//         {
//             Console.Write("\nДля отмены операции введите '0'. ");
//             Console.Write("\nВведите номер записи для удаления: ");
//             string input = Console.ReadLine();

//             if (input == "0")
//             {
//                 Console.WriteLine("Удаление отменено.");
//                 return;
//             }

//             bool isNumber = int.TryParse(input, out index);

//             if (!isNumber) Console.WriteLine("Ошибка: введённое значение не является числом.");
//             else
//             {
//                 if (index >= 1 && index <= flats.Count) { index--; break; }
//                 else Console.WriteLine("Ошибка: номер должен быть в диапазоне от 1 до " + flats.Count);
//             }
//         }
//         flats.RemoveAt(index);
//         SaveAllFlats(flats);
//         Console.WriteLine("Запись успешно удалена.");
//     }

//     public void ShowConsoleChart()
//     {
//         var flats = ReadAllFlats();

//         if (!flats.Any())
//         {
//             Console.WriteLine("Нет данных для построения графика.");
//             return;
//         }

//         Console.WriteLine("\nГрафик оплаты по числу проживающих:\n");

//         double max = 0;
//         foreach (var flat in flats)
//         {
//             double totalPayment = flat.PeopleCount * flat.TariffPerPerson;
//             if (totalPayment > max) max = totalPayment;
//         }

//         // Цвета ANSI
//         ConsoleColor[] colors = Enum.GetValues<ConsoleColor>();

//         int colorIndex = 0;

//         foreach (var flat in flats)
//         {
//             double totalPayment = flat.PeopleCount * flat.TariffPerPerson;
//             int chartBarLength = (int)(totalPayment / max * 100);

//             Console.ForegroundColor = colors[colorIndex % colors.Length]; // устанавливаем цвет
//             string chartBar = new string('*', chartBarLength);

//             Console.WriteLine($"{flat.TenantFullName,-20} | {chartBar}\u001b[0m {totalPayment:F2}");

//             Console.ResetColor(); // сбрасываем цвет на стандартный
//             colorIndex++; // Переход к следующему цвету
//         }

//         Console.WriteLine();
//     }


//     public void SearchFlats()
//     {
//         Console.WriteLine("Выберите параметр для поиска:");
//         Console.WriteLine("1. ФИО арендатора");
//         Console.WriteLine("2. Адрес квартиры");
//         Console.WriteLine("3. Количество жильцов");
//         Console.WriteLine("0. Отмена");

//         string? choice = Console.ReadLine();

//         switch (choice)
//         {
//             case "1":
//                 Console.WriteLine("Введите ФИО арендатора:");

//                 string tenantName = Console.ReadLine();
//                 if (tenantName != null) tenantName = tenantName.ToLower();

//                 if (!string.IsNullOrEmpty(tenantName))
//                 {
//                     var flats = ReadAllFlats();
//                     var filteredFlats = flats.Where(flat => flat.TenantFullName.ToLower().Contains(tenantName)).ToList();

//                     if (filteredFlats.Any()) foreach (var flat in filteredFlats) Console.WriteLine(flat.Info());
//                     else Console.WriteLine("По вашему запросу ничего не найдено.");
//                 }
//                 else Console.WriteLine("Ошибка: ФИО не может быть пустым.");
//                 break;

//             case "2":
//                 Console.WriteLine("Введите адрес квартиры:");

//                 string address = Console.ReadLine();
//                 if (address != null) address = address.ToLower();

//                 if (!string.IsNullOrEmpty(address))
//                 {
//                     var flats = ReadAllFlats();
//                     var filteredFlats = flats.Where(flat => flat.FlatAddress.ToLower().Contains(address)).ToList();

//                     if (filteredFlats.Any()) foreach (var flat in filteredFlats) Console.WriteLine(flat.Info());
//                     else Console.WriteLine("По вашему запросу ничего не найдено.");
//                 }
//                 else Console.WriteLine("Ошибка: адрес не может быть пустым.");
//                 break;

//             case "3":
//                 Console.WriteLine("Введите количество жильцов:");
//                 string? input = Console.ReadLine();
//                 if (int.TryParse(input, out int peopleCount))
//                 {
//                     var flats = ReadAllFlats();
//                     var filteredFlats = flats.Where(flat => flat.PeopleCount == peopleCount).ToList();

//                     if (filteredFlats.Any())
//                     {
//                         Console.WriteLine("\nНайденные квартиры:");
//                         foreach (var flat in filteredFlats)
//                         {
//                             Console.WriteLine(flat.Info());
//                         }
//                     }
//                     else Console.WriteLine("По вашему запросу ничего не найдено.");
//                 }
//                 else Console.WriteLine("Ошибка: введённое значение не является числом.");
//                 break;

//             case "0":
//                 Console.WriteLine("Поиск отменён.");
//                 break;

//             default:
//                 Console.WriteLine("Некорректный выбор. Попробуйте снова.");
//                 break;
//         }
//     }
//     private static void InputData(Flat flat)
//     {
//         while (true)
//         {
//             Console.WriteLine("Введите ФИО арендатора через пробел: ");
//             try { flat.TenantFullName = Console.ReadLine(); break; }
//             catch (ArgumentException exception) { Console.WriteLine(exception.Message); }
//         }
//         while (true)
//         {
//             Console.WriteLine("Введите адрес квартиры: ");
//             try { flat.FlatAddress = Console.ReadLine(); break; }
//             catch (ArgumentException exception) { Console.WriteLine(exception.Message); }
//         }
//         while (true)
//         {
//             Console.WriteLine("Введите количество жильцов: ");
//             try { flat.PeopleCount = Convert.ToInt32(Console.ReadLine()); break; }
//             catch (ArgumentException exception) { Console.WriteLine(exception.Message); }
//         }
//         while (true)
//         {
//             Console.WriteLine("Введите тариф за человека: ");
//             try { flat.TariffPerPerson = Convert.ToDouble(Console.ReadLine()); break; }
//             catch (ArgumentException exception) { Console.WriteLine(exception.Message); }
//         }
//     }

//     private List<Flat> ReadAllFlats()
//     {
//         List<Flat> flats = new();
//         var lines = File.ReadAllLines(filePath);

//         foreach (var line in lines)
//         {
//             try
//             {
//                 var flat = JsonSerializer.Deserialize<Flat>(line);
//                 if (flat != null) flats.Add(flat);
//             }
//             catch (Exception exception) { Console.WriteLine($"Ошибка при чтении строки: {exception.Message}"); }
//         }

//         return flats;
//     }
//     public void SortByParameter()
//     {
//         List<Flat> flats = ReadAllFlats();

//         if (flats.Count == 0)
//         {
//             Console.WriteLine("Нет данных для сортировки.");
//             return;
//         }

//         Console.WriteLine("Выберите параметр для сортировки:");
//         Console.WriteLine("1. ФИО арендатора");
//         Console.WriteLine("2. Адрес квартиры");
//         Console.WriteLine("3. Количество жильцов");
//         Console.WriteLine("4. Тариф за человека");
//         Console.WriteLine("0. Отмена");

//         string? choice = Console.ReadLine();

//         if (choice == "0")
//         {
//             Console.WriteLine("Сортировка отменена.");
//             return;
//         }

//         // В зависимости от выбора вызываем нужную сортировку
//         switch (choice)
//         {
//             case "1":
//                 InsertionSortByFullName(flats);
//                 break;
//             case "2":
//                 InsertionSortByAddress(flats);
//                 break;
//             case "3":
//                 InsertionSortByPeopleCount(flats);
//                 break;
//             case "4":
//                 InsertionSortByTariff(flats);
//                 break;
//             default:
//                 Console.WriteLine("Некорректный выбор.");
//                 return;
//         }

//         SaveAllFlats(flats);
//         Console.WriteLine("Сортировка выполнена и сохранена.");
//     }

//     private void InsertionSortByFullName(List<Flat> flats)
//     {
//         for (int i = 1; i < flats.Count; i++)
//         {
//             Flat current = flats[i];
//             int j = i - 1;

//             // Сравниваем строки ФИО по алфавиту
//             while (j >= 0 && string.Compare(flats[j].TenantFullName, current.TenantFullName) > 0)
//             {
//                 flats[j + 1] = flats[j];
//                 j--;
//             }
//             flats[j + 1] = current;
//         }
//     }

//     private void InsertionSortByAddress(List<Flat> flats)
//     {
//         for (int i = 1; i < flats.Count; i++)
//         {
//             Flat current = flats[i];
//             int j = i - 1;

//             while (j >= 0 && string.Compare(flats[j].FlatAddress, current.FlatAddress) > 0)
//             {
//                 flats[j + 1] = flats[j];
//                 j--;
//             }
//             flats[j + 1] = current;
//         }
//     }

//     private void InsertionSortByPeopleCount(List<Flat> flats)
//     {
//         for (int i = 1; i < flats.Count; i++)
//         {
//             Flat current = flats[i];
//             int j = i - 1;

//             while (j >= 0 && flats[j].PeopleCount > current.PeopleCount)
//             {
//                 flats[j + 1] = flats[j];
//                 j--;
//             }
//             flats[j + 1] = current;
//         }
//     }

//     private void InsertionSortByTariff(List<Flat> flats)
//     {
//         for (int i = 1; i < flats.Count; i++)
//         {
//             Flat current = flats[i];
//             int j = i - 1;

//             while (j >= 0 && flats[j].TariffPerPerson > current.TariffPerPerson)
//             {
//                 flats[j + 1] = flats[j];
//                 j--;
//             }
//             flats[j + 1] = current;
//         }
//     }



//     private void SaveAllFlats(List<Flat> flats)
//     {
//         try
//         {
//             List<string> serializedFlats = new List<string>();
//             foreach (Flat flat in flats)
//             {
//                 string json = JsonSerializer.Serialize(flat, JsonSettings.LinesOptions);
//                 serializedFlats.Add(json);
//             }
//             File.WriteAllLines(filePath, serializedFlats);
//         }
//         catch (IOException exception)
//         {
//             Console.WriteLine($"Ошибка при записи в файл: {exception.Message}");
//         }
//     }

//     private bool IsDuplicate(Flat a, Flat b)
//     {   
//         return a.TenantFullName == b.TenantFullName && a.FlatAddress == b.FlatAddress && a.PeopleCount == b.PeopleCount && a.TariffPerPerson == b.TariffPerPerson;
//     }
// }
