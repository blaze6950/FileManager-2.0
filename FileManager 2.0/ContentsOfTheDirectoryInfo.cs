using System;
using System.IO;
using System.Security;

namespace FileManager_2._0
{
    class ContentsOfTheDirectoryInfo : ContentsOfTheDirectory
    {
        private string BufferedItem { get; set; } = String.Empty;
        private bool IsCopy { get; set; }


        public FileSystemInfo GetInfoForItem(Int32 index)
        {
            if (System.IO.Path.HasExtension(Content[index]))
            {
                return GetFileInfo(Content[index]);
            }
            else
            {
                return GetDirectoryInfo(Content[index]);
            }
        }
        static public FileSystemInfo GetInfoForItem(String path)
        {
            if (System.IO.Path.HasExtension(path))
            {
                return GetFileInfo(path);
            }
            else
            {
                return GetDirectoryInfo(path);
            }
        }
        public void CreateDirectory(String name)
        {
            String newDirectory = Path + @"\" + name;
            try
            {
                if (!Directory.Exists(newDirectory))
                {
                    Directory.CreateDirectory(newDirectory);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (PathTooLongException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
            InitializeContent();
        }
        public void RemoveItem(Int32 index)
        {
            if (System.IO.Path.HasExtension(Content[index]))
            {
                RemoveFile(Content[index]);
            }
            else
            {
                RemoveDirectory(Content[index]);
            }
            InitializeContent();
        }
        public void CopyItem(Int32 index)
        {
            Console.Write("Вы скопировали объект!");
            Console.ReadKey();
            BufferedItem = Content[index];
            IsCopy = true;
        }
        public void CutItem(Int32 index)
        {
            Console.Write("Вы вырезали объект!");
            Console.ReadKey();
            BufferedItem = Content[index];
            IsCopy = false;
        }
        public void PasteItem()
        {
            if (BufferedItem == String.Empty) return;
            try
            {
                if (IsCopy)
                {
                    if (System.IO.Path.HasExtension(BufferedItem))
                    {
                        GetFileInfo(BufferedItem).CopyTo(Path + @"\" + System.IO.Path.GetFileName(BufferedItem), true);
                    }
                    else
                    {
                        Copy(BufferedItem, Path + @"\" + System.IO.Path.GetFileName(BufferedItem));
                    }
                    BufferedItem = String.Empty;
                    IsCopy = false;
                }
                else
                {
                    if (System.IO.Path.HasExtension(BufferedItem))
                    {
                        GetFileInfo(BufferedItem).MoveTo(Path + @"\" + System.IO.Path.GetFileName(BufferedItem));
                    }
                    else
                    {
                        GetDirectoryInfo(BufferedItem).MoveTo(Path + @"\" + System.IO.Path.GetFileName(BufferedItem));
                    }
                    BufferedItem = String.Empty;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
            InitializeContent();
        }
        public void RenameItem(Int32 index)
        {
            Console.WriteLine("Введите новое имя : ");
            try
            {
                String newName = Console.ReadLine();
                if (System.IO.Path.HasExtension(Content[index]))
                {
                    newName = Path + @"\" + newName + System.IO.Path.GetExtension(Content[index]);
                    GetFileInfo(Content[index]).MoveTo(newName);
                }
                else
                {
                    newName = Path + @"\" + newName;
                    GetDirectoryInfo(Content[index]).MoveTo(newName);
                }
                InitializeContent();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }




        static private void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }
        static private void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(System.IO.Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
        static private void RemoveFile(String path)
        {
            Console.WriteLine("Вы хотите удалить этот файл?(Enter - да; Escape - нет)");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Enter:
                    break;
                case ConsoleKey.Escape:
                    return;
                default:
                    return;
            }
            try
            {
                GetFileInfo(path).Delete();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (SecurityException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static private void RemoveDirectory(String path)
        {
            Console.WriteLine("Эта папка может быть не пуста. При удалении будут удалены все файлы, находящиеся в ней! Все равно удалить?(Enter - да; Escape - нет)");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Enter:
                    break;
                case ConsoleKey.Escape:
                    return;
                default:
                    return;
            }
            try
            {
                GetDirectoryInfo(path).Delete(true);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (SecurityException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static private FileInfo GetFileInfo(String path)
        {
            return new FileInfo(path);
        }
        static private DirectoryInfo GetDirectoryInfo(String path)
        {
            return new DirectoryInfo(path);
        }

    }
}
