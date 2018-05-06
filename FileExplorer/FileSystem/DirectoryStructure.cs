﻿using FileExplorer.FileSystem.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileExplorer.FileSystem
{
    public static class DirectoryStructure
    {
        public static List<Data.DataItem> GetLogicalDrives()
        {
            
            return Directory.GetLogicalDrives().Select(drive => new DataItem { FullPath = drive, Type = DataType.Drive }).ToList();
        }

        public static string GetFileOrFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            var normalizedPath = path.Replace('/', '\\');
            var lastIndex = normalizedPath.LastIndexOf('\\');

            if (lastIndex <= 0)
            {
                return path;
            }

            return path.Substring(lastIndex + 1);
        }

        public static List<DataItem> GetDirectoryContents(string fullPath)
        {
            var items = new List<DataItem>();

            try
            {
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                {
                    items.AddRange(dirs.Select(dir => new DataItem
                    {
                        FullPath = dir,
                        Type = DataType.Folder
                    }));
                }
            }
            catch
            {
                //TODO: handle exception.
            }

            try
            {
                var files = Directory.GetFiles(fullPath);

                if (files.Length > 0)
                {
                    items.AddRange(files.Select(dir => new DataItem
                    {
                        FullPath = dir,
                        Type = DataType.File
                    }));
                }
            }
            catch
            {
                //TODO: handle exception.
            }

            return items;
        }
    }
}