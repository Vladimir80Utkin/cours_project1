namespace cours_project;
public class InputService
{
    public static void InputFlat(Flat flat)
    {
        while (true)
        {
            Console.WriteLine("Введите ФИО арендатора через пробел: ");
            try { flat.TenantFullName = Console.ReadLine(); break; }
            catch (ArgumentException exception) { ConsoleMessages.WriteErrorMessage(exception.Message); }
        }
        while (true)
        {
            Console.WriteLine("Введите адрес квартиры: ");
            try { flat.FlatAddress = Console.ReadLine(); break; }
            catch (ArgumentException exception) { ConsoleMessages.WriteErrorMessage(exception.Message); }
        }
        while (true)
        {
            Console.WriteLine("Введите количество жильцов: ");
            try { flat.PeopleCount = Convert.ToInt32(Console.ReadLine()); break; }
            catch (ArgumentException exception) { ConsoleMessages.WriteErrorMessage(exception.Message); }
        }
        while (true)
        {
            Console.WriteLine("Введите тариф за человека: ");
            try { flat.TariffPerPerson = Convert.ToDouble(Console.ReadLine()); break; }
            catch (ArgumentException exception) { ConsoleMessages.WriteErrorMessage(exception.Message); }
        }
    }

     public static int FlatIndex(int maxIndex)
        {
            while (true)
            {
                Console.WriteLine($"Введите индекс квартиры (от 1 до {maxIndex}): ");
                string input = Console.ReadLine();
                
                if (int.TryParse(input, out int index) && index >= 1 && index <= maxIndex) return index - 1;
                else ConsoleMessages.WriteErrorMessage("Некорректный индекс. Повторите ввод.");
                
            }
        }
}
