using System;

namespace TestTaskLibrary
{
    public class temp
    {
        private const string pathToLogs = @"C:\Logs";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="nameOfModule"></param>
        /// <param name="userAction"></param>
        /// <returns>False - если запись неуспешна</returns>
        public void Log(Guid userId, string nameOfModule, string userAction)
        {
            // Тут вызываем инкапсулированные методы
        }
        
        // Проверка существования C:\Logs
        // Проверка существаония модуля (инкапсулировать)
        // Проверка существования файла (инкапсулировать) -- стандарт названия yyyy.mm.dd.number.txt
        // Проверка размера файла, если он существует (инкапсулировать)
        // Запись действия (инкапсулировать) с временем действия (это вызов объекта)
        // Структура - DateTime.Now ; nameOfModule ; userId ; userAction
    }
}

// Корень

///// Подкаталог (модуль) 1
/////// файл 1
/////// файл 2
/////// файл 3

///// Подкаталог (модуль) 2
/////// файл 1
/////// файл 2
/////// файл 3

////// Подкаталог (модуль) 3
/////// файл 1
/////// файл 2
/////// файл 3