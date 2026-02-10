using System;
using System.IO;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;

namespace VideoAssetManager.Encoding
{
    /// <summary>
    /// A helper class for Directory operations.
    /// </summary>
    public static class DirectoryHelper
    {
        public static void CreateIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public static void DeleteIfExists(string directory)
        {
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory);
            }
        }

        public static void DeleteIfExists(string directory, bool recursive)
        {
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, recursive);
            }
        }
        public static void CopyFiles(string sourdeFolder, string prefix,string destinationFolder)
        {
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }
            string searchPattern = $"{prefix}*.jpg";
            string searchPattern_p = $"{prefix}*.png";

            string[] files = Directory.GetFiles(sourdeFolder, searchPattern);
            if(files.Length==0)
                files = Directory.GetFiles(sourdeFolder, searchPattern_p);

            // Copy each file to the destination folder
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string destinationFilePath = Path.Combine(destinationFolder, fileName);
                File.Copy(file, destinationFilePath, true);

            }
        }

        public static void CopyFilesFromDirectory(string sourdeFolder, string destinationFolder)
        {
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }
            string[] files = Directory.GetFiles(sourdeFolder);

            // Copy each file to the destination folder
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string destinationFilePath = Path.Combine(destinationFolder, fileName);
                File.Copy(file, destinationFilePath, true); 
              
            }
        }

        public static void CreateIfNotExists(DirectoryInfo directory)
        {
            if (!directory.Exists)
            {
                directory.Create();
            }
        }

        public static bool IsSubDirectoryOf([NotNull] string parentDirectoryPath, [NotNull] string childDirectoryPath)
        {
            Guard.Against.Null(parentDirectoryPath, nameof(parentDirectoryPath));
            Guard.Against.Null(childDirectoryPath, nameof(childDirectoryPath));

            return IsSubDirectoryOf(
                new DirectoryInfo(parentDirectoryPath),
                new DirectoryInfo(childDirectoryPath)
            );
        }

        public static bool IsSubDirectoryOf([NotNull] DirectoryInfo parentDirectory,
            [NotNull] DirectoryInfo childDirectory)
        {
            Guard.Against.Null(parentDirectory, nameof(parentDirectory));
            Guard.Against.Null(childDirectory, nameof(childDirectory));

            if (parentDirectory.FullName == childDirectory.FullName)
            {
                return true;
            }

            var parentOfChild = childDirectory.Parent;
            return parentOfChild != null && IsSubDirectoryOf(parentDirectory, parentOfChild);
        }

        //public static IDisposable ChangeCurrentDirectory(string targetDirectory)
        //{
        //    var currentDirectory = Directory.GetCurrentDirectory();

        //    if (currentDirectory.Equals(targetDirectory, StringComparison.OrdinalIgnoreCase))
        //    {
        //        return NullDisposable.Instance;
        //    }

        //    Directory.SetCurrentDirectory(targetDirectory);

        //    return new DisposeAction(() => { Directory.SetCurrentDirectory(currentDirectory); });
        //}

        public static long GetFolderSize(string folderPath)
        {
            Guard.Against.NullOrWhiteSpace(folderPath, nameof(folderPath));
            long folderSize = 0;
            if (Directory.Exists(folderPath) == false) return folderSize;

            var directoryInfo = new DirectoryInfo(folderPath);
            
            var files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
            
            Parallel.ForEach<FileInfo, float>(files, // source collection
                () => 0, // method to initialize the local variable
                (file, loop, subtotal) => // method invoked by the loop on each iteration
                {
                    subtotal += GetFileLength(file); //modify local variable
                    return subtotal; // value to be passed to next iteration
                },
                // Method to be executed when each partition has completed.
                // finalResult is the final value of subtotal for a particular partition.
                (finalResult) => Interlocked.Add(ref folderSize, (long)finalResult)
            );
            
            return folderSize;
        }
        
        static long GetFileLength(System.IO.FileInfo fi)  
        {  
            long retval;  
            try  
            {  
                retval = fi.Length;  
            }  
            catch (System.IO.FileNotFoundException)  
            {  
                // If a file is no longer present,  
                // just add zero bytes to the total.  
                retval = 0;  
            }  
            return retval;  
        }  
    }
}