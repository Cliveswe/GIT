using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
///<summary>
/// Created by Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// </summary>
namespace InvoiceMaker.ViewModel.Util
{
    class ReadFileUltility
    {

        /// <summary>
        /// The given path to a file is open and all its contents are read to an array.
        /// This is a batch reader.
        /// </summary>
        /// <param name="fileName">Path and file name to be read.</param>
        /// <returns>An array of string containing the contents of the file line by line.</returns>
        public static string[] ReadFileContent(string fileName)
        {
            string[] readAllData;
           
            string msg = "File: \"" + fileName + "\" is not valid.\nError: ";
            try
            {
                //Opens a specified file, returns the character data as an array of strings, 
                //and then closes the file
                readAllData = File.ReadAllLines(@fileName);
            }
            catch (ArgumentException e)
            {
                throw new Exception("Path null or zero length\n" + msg + e);
            }
            catch (IOException e)
            {
                throw new Exception("An I/O error occurred while opening the file.\n" + msg + e);
            }
            catch (SecurityException e)
            {
                throw new Exception("The caller does not have the required permission.\n" + msg + e);
            }

            return readAllData;
        }
    }
}
