
ConsoleKeyInfo key;
do
{
    ShowMenu();
    key = Console.ReadKey(true);
    switch (key.Key)
    {
        
    }
    
} 
while (key.Key != ConsoleKey.Escape);

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