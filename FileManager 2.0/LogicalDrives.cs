using System;
using System.Collections.Generic;

namespace FileManager_2._0
{
    static class LogicalDrives
    {
        public static List<String> GetLogicalDrives()
        {
            List<String> files = new List<string>();
            try
            {
                files.AddRange(System.IO.Directory.GetLogicalDrives());
            }
            catch (System.IO.IOException)
            {
                Console.WriteLine("An I/O error occurs.");
            }
            catch (System.Security.SecurityException)
            {
                Console.WriteLine("The caller does not have the " +
                                         "required permission.");
            }
            return files;
        }
    }
}
