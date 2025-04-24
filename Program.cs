namespace cours_project
{
    class Program{
        static void Main(){
            const string filePath = @"..\..\..\file.json";        
            var flatManager = new FlatManager(filePath);
            var menu = new MenuManager(flatManager);
            menu.Run();
        }
    }
}