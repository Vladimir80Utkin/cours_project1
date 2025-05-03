using System.Reflection;
using System.ComponentModel;

namespace cours_project
{
    public class InputService
    {
        public static void InputFlat(Flat flat)
        {
            foreach (var property in typeof(Flat).GetProperties())
            {
                string displayName;
                var attribute = property.GetCustomAttribute<DisplayNameAttribute>();
                if (attribute != null) displayName = attribute.DisplayName;
                else displayName = property.Name;

                while (true)
                {
                    Console.WriteLine($"Введите {displayName}:");
                    string input = Console.ReadLine();

                    try
                    {
                        var converter = TypeDescriptor.GetConverter(property.PropertyType);
                        var value = converter.ConvertFromString(input);
                        property.SetValue(flat, value);
                        break;
                    }
                    catch (TargetInvocationException exception) when (exception.InnerException != null) {ConsoleMessages.WriteErrorMessage(exception.InnerException.Message);}
                    catch (Exception) {ConsoleMessages.WriteErrorMessage($"Некорректный формат для \"{displayName}\". Попробуйте снова.");}

                }
            }
        }

        public static int InputIndex(int maxIndex)
        {
            while (true)
            {
                Console.WriteLine($"Введите индекс (от 1 до {maxIndex}): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int index) && index >= 1 && index <= maxIndex) return index - 1;
                else ConsoleMessages.WriteErrorMessage("Некорректный индекс. Повторите ввод.");
            }
        }

        public static string InputString()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input)) return input;
                else ConsoleMessages.WriteErrorMessage("Ввод не может быть пустым. Повторите ввод.");
            }
        }
    }
}
