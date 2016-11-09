using DirectorySearchProject.Constants;
using DirectorySearchProject.Histogram;
using System;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace DirectorySearchProject.Utility
{
    /// <summary>
    /// Contains utlities that pertain to file and directory manipulation.
    /// </summary>
    public static class FileUtilities
    {
        /// <summary>
        /// Name of a defined temporary folder used to hold unzipped artifacts.
        /// </summary>
        private static string TempDirectory = ConfigurationManager.AppSettings[AppConfigKeys.TempFolder];

        /// <summary>
        /// Contstructs the current temp folder relative to the passed in directory.
        /// </summary>
        /// <param name="directoryInfo">The passed in directory</param>
        /// <returns></returns>
        public static string GetTempFolderPath(DirectoryInfo directoryInfo)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                builder.Append(directoryInfo.FullName);
                builder.Append(FileConstants.filePathDelimiter);
                builder.Append(TempDirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Unable to build temp folder path.", ex);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Removes the temporary directory relative to the passed in directory.
        /// </summary>
        /// <param name="directoryInfo">The parent directory</param>
        public static void DeleteTempFolder(DirectoryInfo directoryInfo)
        {
            try
            {
                string tempDirectoryPath = GetTempFolderPath(directoryInfo);

                if (Directory.Exists(tempDirectoryPath))
                {
                    Directory.Delete(tempDirectoryPath, true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Unable delete temp directory.", ex);
            }
        }

        /// <summary>
        /// Creates the temporary directory that holds unzipped artifacts.
        /// </summary>
        /// <param name="directoryInfo">The parent directory</param>
        /// <returns></returns>
        public static DirectoryInfo CreateTempFolder(DirectoryInfo directoryInfo)
        {
            string tempDirectoryPath = string.Empty;

            try
            {
                tempDirectoryPath = GetTempFolderPath(directoryInfo);

                if (!Directory.Exists(tempDirectoryPath))
                {
                    directoryInfo.CreateSubdirectory(TempDirectory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Unable to create the temporary folder.", ex);
            }

            return new DirectoryInfo(tempDirectoryPath);
        }

        /// <summary>
        /// Attempts to unzip a given artifact.
        /// </summary>
        /// <param name="zipFile">The intended artifact to unzip.</param>
        /// <param name="directoryInfo">The parent directory.</param>
        /// <returns>Whether or not the unzip attempt was successful.</returns>
        public static bool UnZip(FileInfo zipFile, DirectoryInfo directoryInfo)
        {
            bool success = true;
            try
            {
                DirectoryInfo tempDirectory = CreateTempFolder(directoryInfo);
                ZipFile.ExtractToDirectory(zipFile.FullName, tempDirectory.FullName);
            }
            catch
            {
                success = false;
            }
            return success;
        }

        /// <summary>
        /// Attempts to validate whether or not the file is a text file that can be parsed. 
        /// </summary>
        /// <param name="line">The line to test for a given file.</param>
        /// <returns>Whether or not the test was successful.</returns>
        public static bool IsValidTextFile(string line)
        {
            bool isValid = true;

            try
            {
                foreach (char character in line.ToCharArray())
                {
                    if (character >= 128)
                    {
                        isValid = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Unable to evaluate the file line.", ex);
            }

            return isValid;
        }

        /// <summary>
        /// Parses a given string into words and loads them into a datastructure.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="histogram"></param>
        public static void ParseLineWords(string line, Histogram<string> histogram)
        {
            try
            {
                string[] words = line.Split(FileConstants.wordDelimiter);

                foreach (string word in words)
                {
                    if (!string.IsNullOrWhiteSpace(word))
                    {
                        histogram.Increment(word.ToLower().Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Unable to parse file.", ex);
            }
        }
    }
}
