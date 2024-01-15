﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NLog;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using NLog.Filters;

namespace TestTaskLibrary
{
    public class LoggerClass
    {
        /// <summary>
        /// The ID of the user who creates log entries
        /// </summary>
        public Guid UserID { get; set; }
        /// <summary>
        /// ID of the module that is currently being used
        /// </summary>
        public Guid Module { get; set; }
        /// <summary>
        /// A string with the path to the module
        /// </summary>
        public string PathToModulesDirectory { get; set; }
        /// <summary>
        /// The name of the module in the form of a string
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// The type of actions performed by the user
        /// </summary>
        public string ActionType { get; set; }
        /// <summary>
        /// The name of the file in the form of a string
        /// </summary>
        public string FileName { get; set; }
        
        //Maximum size of file
        public const int MaxFileSize = 500000000;

        /// <summary>
        /// Constructor of this class
        /// </summary>
        public LoggerClass(Guid UserID, Guid Module, string PathToModulesDirectory, string ActionType, string FileName) 
        {
            this.UserID = UserID;
            this.Module = Module;
            this.PathToModulesDirectory = PathToModulesDirectory;
            this.ModuleName = ModuleName;
            this.ActionType = ActionType;
            this.FileName = FileName;
        }

        /// <summary>
        /// Method that checks file size
        /// </summary>
        /// <param name="PathToFile">Recieving Size of file with logs</param>
        /// <returns>Variable with the test result as a Boolean variable</returns>
        public bool SizeCheck(string PathToModuleDirectory, string FileName)
        {
           //Passing the path to the file from the fields to the local variables
           PathToModuleDirectory = this.PathToModulesDirectory;
           FileName = this.FileName;
           string PathToFile = PathToModuleDirectory + "/" + FileName;

           //Recieving Size of file with logs
           FileInfo LogFileInfo = new FileInfo(PathToFile);
           long FileSize = LogFileInfo.Length;
           
           //Variable, which will be returned as a result of our check
           bool Result;
          
           //Checking file size
           if (FileSize > MaxFileSize)
           {
             //If Result is true - file is overflowed, and we need to create a new file to continue logging
             Result = true;
           }
           else
           {
             //If Result is false - we don't need to create a new file
             Result = false;
           }
           //Returning result of our check
           return Result;
        }

        /// <summary>
        /// A method that, in the absence of a directory for logging files of a certain module, creates it
        /// </summary>
        /// <param name="PathToModulesDirectory">The path to the folder with folders for modules</param>
        /// <param name="ModuleName">The name of the module in the form of a string</param>
        public void CreateNewDirectory(string PathToModulesDirectory, string ModuleName)
        {
            //Creating the expected or existing path to the module folder
            PathToModulesDirectory = this.PathToModulesDirectory;
            ModuleName = this.ModuleName;
            string PathToModuleForlder = PathToModulesDirectory + "/" + ModuleName;

            //Checking if the folder does not exist
            if (!Directory.Exists(PathToModuleForlder))
            {
                //If the folder does not exist, create a folder
                Directory.CreateDirectory(PathToModuleForlder);
            }
        }

        public string CreateNewFile(string PathToFile, string PathToModuleFolder)
        {
            bool CheckResult = SizeCheck(PathToModulesDirectory, FileName);

            string PathToNewFile;

            if (CheckResult == true)
            {
                File.Create();
            }

            return PathToNewFile;
        }

        public void FillingLoggingFile()
        {

        }
    }
}
