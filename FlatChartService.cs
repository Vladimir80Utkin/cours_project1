namespace cours_project;
public class FlatChartService
{
    public static void ShowChart(Flat[] flats)
    {
        if (!flats.Any())
        {
            ConsoleMessages.WriteErrorMessage("Нет данных для построения графика.");
            return;
        }

        double max = flats.Max(f => f.PeopleCount * f.TariffPerPerson);
        ConsoleColor[] colors = Enum.GetValues<ConsoleColor>();
        int colorIndex = 0;

        foreach (var flat in flats)
        {
            double total = flat.PeopleCount * flat.TariffPerPerson;
            int length = (int)(total / max * 100);
            Console.ForegroundColor = colors[colorIndex++ % colors.Length];
            Console.WriteLine($"{flat.TenantFullName,-20} | {new string('*', length)} {total:F2}");
            Console.ResetColor();
        }
    }
}
