using System;
using System.Collections.Generic;
///<summary>
/// Created by Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// </summary>
namespace InvoiceMaker.ViewModel.Util
{
    class InvoiceParser
    {
        /// <summary>
        /// Trim any leading or tailing blanks from each item in the array.
        /// </summary>
        /// <param name="source">An array.</param>
        /// <returns>A updated array.</returns>
        private static string[] TrimResult(string[] source)
        {
            for (int i = 0; i < source.Length; i++)
            {
                source[i] = source[i].Trim();
            }
            return source;
        }
        /// <summary>
        /// Remove any blank lines from an array.
        /// </summary>
        /// <param name="source">An array.</param>
        /// <returns>Updated array.</returns>
        private static string[] RemoveBlankLines(string[] source) {

            List<string> tmp = new List<string>();

            foreach (string item in source)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    tmp.Add(item);
                }
            }
            source = tmp.ToArray();
            return source;
        }
        /// <summary>
        /// Parse the file into a array.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Array of strings</returns>
        public static string[] ParseInvoice(string fileName)
        {
            string tmp;
            string[] fileContents;

            tmp = fileName;
            try
            {
                fileContents = ReadFileUltility.ReadFileContent(fileName);
                fileContents = TrimResult(fileContents);
                fileContents = RemoveBlankLines(fileContents);
            }catch (Exception e)
            {
                throw new Exception("Parser error\n" + e);
            }
            return fileContents;
        }
    }
}
