namespace cours_project;
public class MenuManager
{
    private readonly FlatManager flatManager;

    public MenuManager(FlatManager manager)
    {
        flatManager = manager;
    }

    public void Run()
    {
        while (true)
        {
            ShowMenu();
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.D1:
                    flatManager.Create();
                    break;
                case ConsoleKey.D2:
                    flatManager.Update();
                    break;
                case ConsoleKey.D3:
                    flatManager.Read();
                    break;
                case ConsoleKey.D4:
                    flatManager.Delete();
                    break;
                case ConsoleKey.D5:
                    flatManager.ShowConsoleChart();
                    break;
                case ConsoleKey.D6:
                    flatManager.SearchFlats();
                    break;
                case ConsoleKey.Escape:
                    return;
                default:
                    Console.WriteLine("Неверная клавиша. Повторите ввод.");
                    break;
            }

            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey(true);
        }
    }

    private void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("1 - Добавить данные");
        Console.WriteLine("2 - Изменить данные");
        Console.WriteLine("3 - Показать данные");
        Console.WriteLine("4 - Удалить данные");
        Console.WriteLine("5 - Построить график платежей");
        Console.WriteLine("6 - Найти по параметру");
        Console.WriteLine("7 - Отсотрировать (вставками)");
        Console.WriteLine("ESC - Выход");
        Console.WriteLine(" ");
    }
}
