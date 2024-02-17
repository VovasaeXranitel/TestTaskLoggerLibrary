using System;
using System.IO;

namespace TestTaskLibrary
{

    public static class LoggerClass
    {

        public static bool FileCheck(string pathToLogsFolder, string moduleName, string fileName, int maxFileSize)
        {
            //Creating a string with the path to the module folder and normalizing the path
            string pathToModuleFolder = Path.Combine(pathToLogsFolder, moduleName);
            Path.GetFullPath(pathToModuleFolder);

            //Create a path to a file that requires verification
            string filePath = Path.Combine(pathToLogsFolder, moduleName, fileName);
            filePath = Path.GetFullPath(filePath);

            // Information about the file will be stored here to check the size
            FileInfo file = new FileInfo(filePath);

            if (Directory.Exists(pathToLogsFolder) && Directory.Exists(pathToModuleFolder) && File.Exists(filePath) && file.Length > maxFileSize)
            { 
                return true;
            }
        }


        public static void FileCreate()
        {

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
        public static void LogWrite(Guid userID, string moduleName, string userAction, string pathToLogsFolder, string fileName)
        {
            //We call the method to check the existence of the main directory with all the logs, so that if it does not exist, the method will create it
            LogsFolderCheck(pathToLogsFolder);

            //Similar to the method for the main directory, we call the check for the folder with module logs
            ModuleFolderCheck(pathToLogsFolder, moduleName);

            //We get the path to a new or existing file into which we will write logs
            string resultFilePath = FileCheck(pathToLogsFolder, fileName, moduleName);

            //We write a message about the action performed by the user to a file
            File.AppendAllText(resultFilePath, string.Concat(DateTime.Now.Date.ToString(), ";", moduleName, ";",userID.ToString(), ";",userAction, "/n"));

        }

        /// <summary>
        /// Method for reading data from a file
        /// </summary>
        /// <param name="pathToLogsFolder"></param>
        /// <param name="moduleName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string[] LogRead(string pathToLogsFolder, string moduleName, string fileName)
        {
            //We call the method to check the existence of the main directory with all the logs, so that if it does not exist, the method will create it
            LogsFolderCheck(pathToLogsFolder);

            //Similar to the method for the main directory, we call the check for the folder with module logs
            ModuleFolderCheck(pathToLogsFolder, moduleName);

            //We get the path to a new or existing file into which we will write logs
            string resultFilePath = FileCheck(pathToLogsFolder, fileName, moduleName);

            //We get an array, which contains all lines of our file
            string[] dataFromFile = File.ReadAllLines(resultFilePath);

            //
            return dataFromFile;

        }
    }
}
