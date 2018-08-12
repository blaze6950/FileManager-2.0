using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager_2._0
{
    static class PrintAll
    {
        static private StringBuilder result = new StringBuilder(400);
        static public void PrintButtons()
        {
            result.Clear();
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                result.Append("█");
            }
            result.Append("█Создать F1██Переименовать F2█Удалить F3█Копировать F4█Вырезать F5██Вставить F6█");
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                result.Append("█");
            }
        }

        static public void PrintPath(String path)
        {
            Int32 lengthPath = path.Length;
            result.Append("█");
            result.Append(path);
            for (int i = lengthPath+1; i < Console.WindowWidth-1; i++)
            {
                result.Append(" ");
            }
            result.Append("█");
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                result.Append("█");
            }
        }

        static public void PrintDirectoriesAndFiles(List<String> content, Int32 selectedItem, bool drivesOrCotdi)
        {
            int i = 0, emptyEl = 0;
            int sizeWindow = 15;
            Console.Write(result);
            if (!drivesOrCotdi)
            {
                PrintMoveBack(selectedItem);
            }
            if (selectedItem > sizeWindow)
            {
                i = (sizeWindow - selectedItem) - (sizeWindow - selectedItem) - (sizeWindow - selectedItem);
            }
            if (content.Count - i < sizeWindow)
            {
                emptyEl = sizeWindow - content.Count - i;
            }
            for (int z = 0; i < content.Count; i++, z++)
            {
                if (i == selectedItem)
                {
                    PaintElement();
                }
                DrawElement(content[i], drivesOrCotdi);
                if (z == sizeWindow) break;
            }
            for (int j = 0; j < emptyEl; j++)
            {
                DrawEmptyElement();
            }
            PrintHorizontalSeparator();
        }

        static private void PaintElement()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
        }

        static private void DrawElement(String path, bool drivesOrCotdi)
        {
            String res;
            Console.Write("█");
            if (drivesOrCotdi)
            {
                res = path;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(res);
            }
            else
            {
                res = System.IO.Path.GetFileName(path);
                if (System.IO.Path.HasExtension(path))
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                Console.Write(res);
            }
            if (res != null)
                for (int i = res.Length + 1; i < Console.WindowWidth - 1; i++)
                {
                    Console.Write(" ");
                }
            Console.ResetColor();
            Console.Write("█");
        }

        static private void PrintMoveBack(Int32 selectedItem)
        {
            Console.Write("█");
            if (selectedItem == -1)
            {
                PaintElement();
                Console.Write("../");
                Console.ResetColor();
            }
            else
            {
                Console.Write("../");
            }
            for (int i = 4; i < Console.WindowWidth - 1; i++)
            {
                Console.Write(" ");
            }
            Console.Write("█");
        }

        static private void PrintHorizontalSeparator()
        {
            result.Clear();
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                result.Append("█");
            }
            Console.Write(result);
        }

        static private void DrawEmptyElement()
        {
            Console.Write("█");
            for (int i = 1; i < Console.WindowWidth - 1; i++)
            {
                Console.Write(" ");
            }
            Console.Write("█");
        }
    }

    
}
