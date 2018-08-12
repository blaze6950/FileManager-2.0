using System;
using System.Collections.Generic;
using System.IO;
using System.Security;

namespace FileManager_2._0
{
    class ContentsOfTheDirectory
    {
        protected List<String> _content = new List<string>();
        public List<string> Content
        {
            get { return _content; }
        }
        // ReSharper disable once InconsistentNaming
        protected String _path = String.Empty;
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
                InitializeContent();
            }
        }
        public String GetFullPathForTheItem(Int32 index) // возвращает полный путь для элемента по переданному индексу
        {
            String res = String.Empty;
            try
            {
                res = System.IO.Path.GetFullPath(Content[index]);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (SecurityException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (PathTooLongException e)
            {
                Console.WriteLine(e.Message);
            }
            return res;
        }
        public String GetFullNameOfTheItem(Int32 index) // возвращает полное имя без пути(т.е. только имя) для элемента по переданному индексу
        {
            String res = String.Empty;
            try
            {
                if (System.IO.Path.HasExtension(Content[index]))
                {
                    res = System.IO.Path.GetFileName(Content[index]);
                }
                else
                {
                    res = System.IO.Path.GetDirectoryName(Content[index]);
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (SecurityException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (PathTooLongException e)
            {
                Console.WriteLine(e.Message);
            }
            return res;
        }

        public bool MoveBack()
        {
            try
            {
                Path = Directory.GetParent(Path).FullName;
            }
            catch (NullReferenceException)
            {
                return false;
            }
            return true;
        }
        public void MoveTo(String newPath)
        {
            Path = newPath;
        }

        public Int32 GetCount()
        {
            return Content.Count;
        }
        protected void InitializeContent() // инициализирует коллекцию новыми значениями. Метод вызывается, когда был обновлен путь
        {
            if (Path == String.Empty) return;
            Content.Clear();
            Content.AddRange(FoldersOnPath.GetDirectories(Path));
            Content.AddRange(FilesOnPath.GetFiles(Path));
        }
        public String this[int index]    // Indexer declaration  
        {
            get
            {
                return Content[index];
            }

            set
            {
                Content[index] = value;
            }
        }
    }
}
