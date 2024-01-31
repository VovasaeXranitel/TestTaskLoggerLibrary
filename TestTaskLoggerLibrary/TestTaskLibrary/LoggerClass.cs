using System;
using System.IO;

namespace TestTaskLibrary
{

    public class LoggerClass
    {
        /// <summary>
        /// Checking the existence of a folder for all logs. If the folder does not exist, then the method creates such a folder
        /// </summary>
        /// <param name="pathToLogsFolder">Path to the required folder</param>
        /// <returns>A boolean value that we use in subsequent methods to check whether a given method worked</returns>
        public bool LogsFolderCheck(string pathToLogsFolder)
        {
            if (Directory.Exists(pathToLogsFolder))
            {
                //If the folder exists, simply return true
                return true;
            }
            else 
            {
                //If the folder does not exist, then create the folder and still return true
                Directory.CreateDirectory(pathToLogsFolder);
                return true;
            }
        }

        /// <summary>
        /// Checks the existence of a folder with logs for a specific module. If there is no such folder for the module, it creates it.
        /// </summary>
        /// <param name="pathToLogsFolder">The path to the folder with all logs in general. It is needed to create a path to the folder with the module.</param>
        /// <param name="moduleName">The name of the module. It is necessary to search or create a folder</param>
        /// <returns>A boolean value that we use in subsequent methods to check whether a given method worked</returns>
        public bool ModuleFolderCheck(string pathToLogsFolder, string moduleName)
        {
            //Creating a string with the path to the module folder and normalizing the path
            string pathToModuleFolder = Path.Combine(pathToLogsFolder, moduleName);
            Path.GetFullPath(pathToModuleFolder);

            if(Directory.Exists(pathToModuleFolder))
            {
                //The folder exists, nothing needs to be created, return true
                return true;
            }
            else
            {
                //The folder does not exist.Creating it and returning trueThe folder does not exist.Creating it and returning true
                Directory.CreateDirectory(pathToModuleFolder);
                return true;
            }
        }

        /// <summary>
        /// The method of checking the existence of files
        /// </summary>
        /// <param name="pathToLogsFolder">The path to the root directory</param>
        /// <param name="moduleName">Name of a module</param>
        /// <param name="fileName">Name of a file</param>
        /// <param name="maxFileSize">Maximum file size</param>
        public string FileCheck(string pathToLogsFolder, string moduleName, string fileName, int maxFileSize)
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

        public void LogWrite(Guid userID, string moduleName, string userAction)
        {

        }


       

    }
}
