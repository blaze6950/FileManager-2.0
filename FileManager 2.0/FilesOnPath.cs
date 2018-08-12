using System;
using System.Collections.Generic;
using System.IO;

namespace FileManager_2._0
{
    static class FilesOnPath
    {
        public static List<String> GetFiles(String path) // статическая функция, возвращающая коллекцию типа String. Возвращает все файлы по переданному пути в отсортированном виде
        {
            List<String> files = new List<string>();
            try
            {
                files.AddRange(Directory.GetFiles(path));
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
            catch (PathTooLongException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
            files.Sort((x, y) => String.Compare(x, y, StringComparison.Ordinal));
            return files;
        }
    }
}
