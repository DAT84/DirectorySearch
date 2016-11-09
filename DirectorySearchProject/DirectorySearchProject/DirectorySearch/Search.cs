using DirectorySearchProject.Histogram;
using DirectorySearchProject.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace DirectorySearchProject
{
    /// <summary>
    /// Class that encompasses the search methods.
    /// </summary>
    public class Search
    {
        /// <summary>
        /// Begins the directory search
        /// </summary>
        /// <returns>Returns collected data</returns>
        public Histogram<string> StartSearch()
        {
            Histogram<string> histogram = new Histogram<string>();
            DirectoryInfo startingDirectory = GetStartingDirectory();

            if (startingDirectory != null)
            {
                SearchFiles(startingDirectory, histogram);
                SearchDirectories(startingDirectory, histogram);
                FileUtilities.DeleteTempFolder(startingDirectory);
            }
            return histogram;
        }

        /// <summary>
        /// Searches a directory and its content.
        /// </summary>
        /// <param name="directory">The directory to search.</param>
        /// <param name="histogram">The data structure to hold the found data.</param>
        private void SearchDirectories(DirectoryInfo directory, Histogram<string> histogram)
        {
            try
            {
                foreach (DirectoryInfo directoryInfo in directory.GetDirectories())
                {
                    SearchFiles(directoryInfo, histogram);
                    SearchDirectories(directoryInfo, histogram);
                    FileUtilities.DeleteTempFolder(directoryInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        /// <summary>
        /// Searches all of the files in a given directory.
        /// </summary>
        /// <param name="directory">The directory to search.</param>
        /// <param name="histogram">The data structure to hold collected data.</param>
        private void SearchFiles(DirectoryInfo directory, Histogram<string> histogram)
        {
            try
            {
                foreach (FileInfo fileInfo in directory.GetFiles())
                {

                    bool success = FileUtilities.UnZip(fileInfo, directory);

                    if (!success)
                    {
                        //process files
                        ReadFile(fileInfo, histogram);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Unable to search files in the given directory.", ex);
            }
        }

        /// <summary>
        /// Requests a starting directory 
        /// </summary>
        /// <returns>Null or a valid starting directory</returns>
        private DirectoryInfo GetStartingDirectory()
        {
            DirectoryInfo startingDirectory = null;

            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    startingDirectory = new DirectoryInfo(dialog.SelectedPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Unable to select a starting directory.", ex);
            }

            return startingDirectory;
        }

        /// <summary>
        /// Reads a file line by line 
        /// </summary>
        /// <param name="fileInfo">The object representation of the file to read.</param>
        /// <param name="histogram">The data structure to hold corresponding data read from the file.</param>
        private void ReadFile(FileInfo fileInfo, Histogram<string> histogram)
        {
            try
            {
                using (StreamReader file = new StreamReader(fileInfo.FullName))
                {
                    string line = file.ReadLine();
                    if (FileUtilities.IsValidTextFile(line))
                    {
                        while (line != null)
                        {
                            FileUtilities.ParseLineWords(line, histogram);
                            line = file.ReadLine();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Unable to read file.", ex);
            }
        }
    }
}
