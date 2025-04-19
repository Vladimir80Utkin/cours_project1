namespace cours_project;

public class FlatChart
{
    public void Display(List<Flat> flats)
    {
        if (!flats.Any())
        {
            Console.WriteLine("Нет данных для построения графика.");
            return;
        }

        Console.WriteLine("\nГрафик оплаты по числу проживающих:\n");

        double max = flats.Max(f => f.PeopleCount * f.TariffPerPerson);

        ConsoleColor[] colors = Enum.GetValues<ConsoleColor>();
        int colorIndex = 0;

        foreach (var flat in flats)
        {
            double total = flat.PeopleCount * flat.TariffPerPerson;
            int length = (int)(total / max * 100);

            Console.ForegroundColor = colors[colorIndex % colors.Length];
            string bar = new string('*', length);

            Console.WriteLine($"{flat.TenantFullName,-20} | {bar}\u001b[0m {total:F2}");

            Console.ResetColor();
            colorIndex++;
        }

        Console.WriteLine();
    }
}
