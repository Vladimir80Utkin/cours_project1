using Newtonsoft.Json;
namespace cours_project
{
    class Program{
        static void Main(){
            try
            {
                Flat flat1 = new Flat("Ivan Ivanov", "1234 Elm Street");
                Console.WriteLine("фио: " + flat1.TenantFullName);
                Console.ReadKey();      
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine("Ошибка: " + exception.Message);
                Console.ReadKey();
            }
        }

        static void ShowMenu(){
            Console.Clear();
            Console.WriteLine("F1 - Ввод данных");
            Console.WriteLine("F2 - Запись данных в файл");
            Console.WriteLine("F3 - Чтение данных из файла");
            Console.WriteLine("F4 - Вывод данных на экран");
            Console.WriteLine("F5 - Дополнение данных");
            Console.WriteLine("F6 - Удаление данных");
            Console.WriteLine("F7 - Поиск информации");
            Console.WriteLine("F8 - Сортировка данных (вставками)");
            Console.WriteLine("F9 - Построение диаграммы");
            Console.WriteLine("ESC - Выход");
        }
    }
}