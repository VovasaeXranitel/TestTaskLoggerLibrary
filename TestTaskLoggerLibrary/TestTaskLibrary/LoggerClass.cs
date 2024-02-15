using Microsoft.Extensions.Options;
using NLog.Filters;
using NLog.LayoutRenderers;
using System;
using System.Diagnostics.Eventing.Reader;
using System.IO;

namespace TestTaskLibrary
{

    public static class LoggerClass
    {
        /// <summary>
        /// Checking the existence of a folder for all logs. If the folder does not exist, then the method creates such a folder
        /// </summary>
        /// <param name="pathToLogsFolder">Path to the required folder</param>
        public static void LogsFolderCheck(string pathToLogsFolder)
        {
            if (Directory.Exists(pathToLogsFolder))
            {
                //The directory exists, no additional actions are required
                return;
            }
            else
            {
                //The directory doesn't exist - so let's create it
                Directory.CreateDirectory(pathToLogsFolder);
            }
        }

        /// <summary>
        /// Checks the existence of a folder with logs for a specific module. If there is no such folder for the module, it creates it.
        /// </summary>
        /// <param name="pathToLogsFolder">The path to the folder with all logs in general. It is needed to create a path to the folder with the module.</param>
        /// <param name="moduleName">The name of the module. It is necessary to search or create a folder</param>
        public static void ModuleFolderCheck(string pathToLogsFolder, string moduleName)
        {
            //Creating a string with the path to the module folder and normalizing the path
            string pathToModuleFolder = Path.Combine(pathToLogsFolder, moduleName);
            Path.GetFullPath(pathToModuleFolder);

            if (Directory.Exists(pathToModuleFolder))
            {
                //The directory exists, no additional actions are required
                return;
            }
            else
            {
                //The directory doesn't exist - so let's create it
                Directory.CreateDirectory(pathToModuleFolder);
            }
        }

        /// <summary>
        /// The method of checking the existence of files
        /// </summary>
        /// <param name="pathToLogsFolder">The path to the root directory</param>
        /// <param name="moduleName">Name of a module</param>
        /// <param name="fileName">Name of a file</param>
        /// <param name="maxFileSize">Maximum file size</param>
        public static string FileCheck(string pathToLogsFolder, string moduleName, string fileName, int maxFileSize)
        {

            // We get the path to the file whose name was passed by the parameter
            string filePath = Path.Combine(pathToLogsFolder, moduleName, fileName);

            // Normalization
            filePath = Path.GetFullPath(filePath);

            // Information about the file will be stored here to check the size
            FileInfo file = new FileInfo(filePath);

            // If the file exists and does not exceed the size, we leave the method and return checked file, otherwise we create a new one
            if (File.Exists(filePath) && file.Length < maxFileSize) return filePath;

            // Current date
            DateTime date = DateTime.Today;

            // Variable for file numbering
            int fileNumber = 0;

            // The number of files in the directory
            int fileCount = Directory.GetFiles(filePath).Length;

            // If there are not zero files in the directory, the number of the new file = number + 1
            if (fileCount > 0) fileNumber = fileCount + 1;

            // New file name
            string fileNewName = date + "." + fileNumber.ToString();

            // New file path
            string fileNewPath = Path.Combine(pathToLogsFolder, moduleName, fileNewName);

            // Normalization
            fileNewPath = Path.GetFullPath(fileNewPath);

            // Creating a new file
            File.Create(fileNewPath);

            //Returning the path to the created file
            return fileNewPath;

        }

        /// <summary>
        ///  Method for writing a message to a log file
        /// </summary>
        /// <param name="userID">GUID User ID</param>
        /// <param name="moduleName">Name of a module</param>
        /// <param name="userAction">Action taken by the user</param>
        /// <param name="pathToLogsFolder">Path to the main folder containing all logs</param>
        /// <param name="fileName">The name of the file where the logs were originally written (written)</param>
        /// <param name="maxFileSize">Maximum log file size</param>
        public static void LogWrite(Guid userID, string moduleName, string userAction, string pathToLogsFolder, string fileName, int maxFileSize)
        {
            //We call the method to check the existence of the main directory with all the logs, so that if it does not exist, the method will create it
            LogsFolderCheck(pathToLogsFolder);

            //Similar to the method for the main directory, we call the check for the folder with module logs
            ModuleFolderCheck(pathToLogsFolder, moduleName);

            //We get the path to a new or existing file into which we will write logs
            string resultFilePath = FileCheck(pathToLogsFolder, fileName, moduleName, maxFileSize);

            //We write a message about the action performed by the user to a file
            File.AppendAllText(resultFilePath, DateTime.Now.Date.ToString() + moduleName + userID.ToString() + userAction + "");

        }

        public static string[] LogRead(string pathToLogsFolder, string moduleName, string fileName, int maxFileSize)
        {
            //We call the method to check the existence of the main directory with all the logs, so that if it does not exist, the method will create it
            LogsFolderCheck(pathToLogsFolder);

            //Similar to the method for the main directory, we call the check for the folder with module logs
            ModuleFolderCheck(pathToLogsFolder, moduleName);

            //We get the path to a new or existing file into which we will write logs
            string resultFilePath = FileCheck(pathToLogsFolder, fileName, moduleName, maxFileSize);

            //We get an array, which contains all lines of our file
            string[] dataFromFile = File.ReadAllLines(resultFilePath);

            return dataFromFile;

        }
    }
}
