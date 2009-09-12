using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using XStreamer.FileSystem.Exceptions;

namespace XStreamer.FileSystem
{
    public class VirtualFolder
    {
        private readonly IDictionary<string, string> _shares;

        public const string FolderRoot = "/";
        public const string FolderCurrent = ".";
        public const string FolderParent = "..";

        public const uint RootLevel = 0xffffffff;

        private const char PathSep = '/';
        
        private int _depth = 0;
        private string _currentFolder = FolderRoot;

        public VirtualFolder(IDictionary<string, string> shares)
        {
            _shares = shares;
        }

        public string CurrentFolder
        {
            get { return _currentFolder; }
        }

        public void UpWorkingFolder(uint levels)
        {
            if (levels == 0)
                return;

            if (_depth > 0)
            {
                int lastSlash = CurrentFolder.LastIndexOf(PathSep);
                _currentFolder = CurrentFolder.Substring(0, lastSlash);
                _depth--;
            }
        }

        public void SetWorkingFolder(string folder)
        {
            // handle NO-OP 
            if (folder == FolderCurrent)
                return;

            if (folder == FolderParent)
            {
                UpWorkingFolder(1);
            }
            else if (folder == FolderRoot)
            {
                UpWorkingFolder(RootLevel);
            }
            else
            {
                if (!FolderExists(folder))
                {
                    throw new FolderDoesNotExistException(folder, CurrentFolder);
                }

                if (_depth == 0)
                {
                    _currentFolder = folder;
                }
                else
                {
                    _currentFolder = CurrentFolder + (PathSep + folder);
                }
                _depth++;
            }

        }

        public bool FolderExists(string folder)
        {
            if (_depth == 0)
            {
                // look for a share
                return _shares.ContainsKey(folder);
            }
         
            // check whether the directory exists on the file system
            return Directory.Exists(PhysicalPathForFolder(_currentFolder + "/" + folder));
        }

        public static string ShareNameForPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("The argument must contain a valid path", "path");

            int slashCount = 0;

            // use linq to select only the first part of the path and remove
            // all the slashes
            string share = new string(path.TakeWhile(x =>
                                                 {
                                                     if (x == '/')
                                                         slashCount++;
                                                     return slashCount < 2;
                                                 })
                                  .SkipWhile(c => c == '/').ToArray());

            if (string.IsNullOrEmpty(share))
                throw new PathHasNoShareException(path);

            return share;
        }

        public static string PhysicalPathForFolder(string folder)
        {
            string share = ShareNameForPath(folder);

            return string.Empty;
        }

        //public IEnumerable<string> FileList()
        //{
        //    if (_currentDirectory == FolderRoot)
        //    {
        //        return _shares.Select(share => share.Key);
        //    }
        //}


    }
}
