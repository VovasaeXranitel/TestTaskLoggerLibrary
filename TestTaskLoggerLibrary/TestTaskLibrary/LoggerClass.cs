using System;
using System.IO;
using System.Text;

namespace TestTaskLibrary
{

    public static class LoggerClass
    {
        /// <summary>
        /// Path to the folder with all logs
        /// </summary>
        private const string PathToLogsFolder = @"/Users/georgelofenfeld/Desktop/rider_projects/TestTaskLoggerLibrary/logs";

        //Maximum size of file
        private const int MaxFileSize = 500000000;

        /// <summary>
        /// Checking the existence of a folder for all logs. If the folder does not exist, then the method creates such a folder
        /// </summary>
        private static void LogsFolderCheck()
        {
            if (!Directory.Exists(PathToLogsFolder))
            {
                //The directory doesn't exist - so let's create it
                Directory.CreateDirectory(PathToLogsFolder);
            }
        }

        /// <summary>
        /// Checks the existence of a folder with logs for a specific module. If there is no such folder for the module, it creates it.
        /// </summary>
        /// <param name="moduleName">The name of the module. It is necessary to search or create a folder</param>
        private static void ModuleFolderCheck(string moduleName)
        {
            //Creating a string with the path to the module folder and normalizing the path
            string pathToModuleFolder = Path.Combine(PathToLogsFolder, moduleName);
            pathToModuleFolder = Path.GetFullPath(pathToModuleFolder);

            if (!Directory.Exists(pathToModuleFolder))
            {
                //The directory exists, no additional actions are required
                Directory.CreateDirectory(pathToModuleFolder);
            }
        }

        /// <summary>
        /// The method of checking the existence of files
        /// </summary>
        /// <param name="moduleName">Name of a module</param>
        /// <param name="fileName">Name of a file</param>
        private static string FileCheck(string moduleName, string fileName)
        {

            // We get the path to the file whose name was passed by the parameter
            string filePath = Path.Combine(PathToLogsFolder, moduleName, fileName);

            // Normalization
            filePath = Path.GetFullPath(filePath);

            // Information about the file will be stored here to check the size
            FileInfo file = new FileInfo(filePath);

            // If the file exists and does not exceed the size, we leave the method and return checked file, otherwise we create a new one
            if (File.Exists(filePath) && file.Length < MaxFileSize) return filePath;

            filePath = Path.Combine(PathToLogsFolder, moduleName);

            // Current date
            DateTime date = DateTime.Today;

            // Variable for file numbering
            int fileNumber = 0;

            // The number of files in the directory
            int fileCount = Directory.GetFiles(filePath).Length;

            // If there are not zero files in the directory, the number of the new file = number + 1
            if (fileCount > 0) fileNumber = fileCount + 1;

            // New file name
            string fileNewName = string.Concat(date.ToString("dd-MM-yyyy"), ".", fileNumber, ".txt");

            // New file path
            string fileNewPath = Path.Combine(PathToLogsFolder, moduleName, fileNewName);

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
        /// <param name="userId">GUID User ID</param>
        /// <param name="moduleName">Name of a module</param>
        /// <param name="userAction">Action taken by the user</param>
        /// <param name="fileName">The name of the file where the logs were originally written (written)</param>
        public static void LogWrite(Guid userId, string moduleName, string fileName, string userAction)
        {
            //We call the method to check the existence of the main directory with all the logs, so that if it does not exist, the method will create it
            LogsFolderCheck();

            //Similar to the method for the main directory, we call the check for the folder with module logs
            ModuleFolderCheck(moduleName);

            //We get the path to a new or existing file into which we will write logs
            string resultFilePath = FileCheck(moduleName, fileName);

            //We write a message about the action performed by the user to a file
            File.AppendAllText(resultFilePath, string.Concat(DateTime.Now.Date.ToString("g"), " ; ", moduleName, " ; ", userId.ToString(), " ; ", userAction, "\n"));
        }

        public static string[] LogRead(string moduleName, string fileName)
        {
            //We call the method to check the existence of the main directory with all the logs, so that if it does not exist, the method will create it
            LogsFolderCheck();

            //Similar to the method for the main directory, we call the check for the folder with module logs
            ModuleFolderCheck(moduleName);

            //We get the path to a new or existing file into which we will write logs
            string resultFilePath = FileCheck(moduleName, fileName);

            //We get an array, which contains all lines of our file
            string[] dataFromFile = File.ReadAllLines(resultFilePath);

            return dataFromFile;

        }
    }
}
