using System;

namespace FileManager_2._0
{
    class Program
    {
        static void Main()
        {
            DirectoryManagementAndInteractionWithTheOutput filemanager = new DirectoryManagementAndInteractionWithTheOutput();
            while (true)
            {
                filemanager.ReadChanges(Console.ReadKey());
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}
