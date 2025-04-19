namespace cours_project
{
    class Program{
        static void Main(){        
            var flatManager = new FlatManager();
            var menu = new MenuManager(flatManager);
            menu.Run();
        }
    }
}