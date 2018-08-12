using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FileManager_2._0
{
    class DirectoryManagementAndInteractionWithTheOutput
    {
        protected List<String> _drives;
        protected ContentsOfTheDirectoryInfo _cotdi;
        protected Boolean IsChanged;
        protected int SelectedItem { get; set; } = -1;

        public DirectoryManagementAndInteractionWithTheOutput()
        {
            _drives = LogicalDrives.GetLogicalDrives();
            _cotdi = new ContentsOfTheDirectoryInfo();
        }

        public bool ReadChanges(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.DownArrow)
            {
                ActionDownArrow();
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                ActionUpArrow();
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                ActionEnter();
            }
            else if (key.Key == ConsoleKey.F1)
            {
                ActionF1();
            }
            else if (key.Key == ConsoleKey.F2)
            {
                ActionF2();
            }
            else if (key.Key == ConsoleKey.F3)
            {
                ActionF3();
            }
            else if (key.Key == ConsoleKey.F4)
            {
                ActionF4();
            }
            else if (key.Key == ConsoleKey.F5)
            {
                ActionF5();
            }
            else if (key.Key == ConsoleKey.F6)
            {
                ActionF6();
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                ActionEscape();
            }
            if (IsChanged)
            {
               Print();
                IsChanged = false;
            }
            return true;
        }

        private void ActionDownArrow()
        {
            if (_cotdi.Path != String.Empty)
            {
                if (SelectedItem < _cotdi.GetCount())
                {
                    SelectedItem++;
                    IsChanged = true;
                }
            }
            else
            {
                if (SelectedItem < _drives.Count - 1)
                {
                    SelectedItem++;
                    IsChanged = true;
                }
            }
        }
        private void ActionUpArrow()
        {
            if (_cotdi.Path != String.Empty)
            {
                if (SelectedItem > -1)
                {
                    SelectedItem--;
                    IsChanged = true;
                }
            }
            else
            {
                if (SelectedItem > 0)
                {
                    SelectedItem--;
                    IsChanged = true;
                }
            }
        }
        private void ActionEnter()
        {
            if (SelectedItem == -1 && _cotdi.Path != String.Empty)
            {
                if (!_cotdi.MoveBack())
                {
                    _cotdi.Path = String.Empty;
                }
                IsChanged = true;
                SelectedItem = -1;
            }
            else if (_cotdi.Path != String.Empty && System.IO.Path.HasExtension(_cotdi.GetFullNameOfTheItem(SelectedItem)))
            {
                OpenFile();
                IsChanged = true;
            }
            else if (_cotdi.Path != String.Empty)
            {
                _cotdi.MoveTo(_cotdi.GetFullPathForTheItem(SelectedItem));
                IsChanged = true;
                SelectedItem = -1;
            }
            else
            {
                if (SelectedItem == -1) return;
                _cotdi.Path = _drives[SelectedItem];
                SelectedItem = -1;
                IsChanged = true;
            }
        }
        private void ActionF1() // Создать
        {
            if (_cotdi.Path != String.Empty)
            {
                Console.WriteLine("Введите название :");
                String name = Console.ReadLine();
                _cotdi.CreateDirectory(name);
                IsChanged = true;
            }
        }
        private void ActionF2() // Переименовать
        {
            if (_cotdi.Path != String.Empty && SelectedItem > -1)
            {
                _cotdi.RenameItem(SelectedItem);
                IsChanged = true;
            }
        }
        private void ActionF3() // Удалить
        {
            if (_cotdi.Path != String.Empty && SelectedItem > -1)
            {
                _cotdi.RemoveItem(SelectedItem);
                IsChanged = true;
            }
        }
        private void ActionF4() // Копировать
        {
            if (_cotdi.Path != String.Empty && SelectedItem > -1)
            {
                _cotdi.CopyItem(SelectedItem);
                IsChanged = true;
            }
        }
        private void ActionF5() // Вырезать
        {
            if (_cotdi.Path != String.Empty && SelectedItem > -1)
            {
                _cotdi.CutItem(SelectedItem);
                IsChanged = true;
            }
        }
        private void ActionF6() // Вставить
        {
            if (_cotdi.Path != String.Empty)
            {
                _cotdi.PasteItem();
                IsChanged = true;
            }
        }
        private void ActionEscape()
        {
            Console.WriteLine("Для выхода из программы нажмите крестик вверху справа!");
        }

        private void OpenFile()
        {
            Process.Start(_cotdi[SelectedItem]);
        }
        private void Print()
        {
            Console.Clear();
            PrintAll.PrintButtons();
            PrintAll.PrintPath(_cotdi.Path);
            if (_cotdi.Path == String.Empty)
            {
                PrintAll.PrintDirectoriesAndFiles(_drives, SelectedItem, true);
            }
            else
            {
                PrintAll.PrintDirectoriesAndFiles(_cotdi.Content, SelectedItem, false);
            }
        }
    }
}
