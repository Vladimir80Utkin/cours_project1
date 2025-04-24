namespace cours_project;
public static class ConsoleMessages
{
    public static void WriteSuccessMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n{message}\n");
        Console.ResetColor();
    }

    public static void WriteErrorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n{message}\n");
        Console.ResetColor();
    }
}
