using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gradescores
{
    public class FileToNamesAndScoresParser
    {
        /// <summary>
        /// Parses a csv file of format (<string>,<string>,<int>).        
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename">csv filename</param>
        /// <param name="objectConstructor">Type T constructor method</param>
        /// <exceptions>
        /// IOException
        /// InvalidDataException
        /// </exceptions>
        /// <returns></returns>
        public static List<T> FileToNamesAndScores<T>(string filename, Func<string, string, int, T> objectConstructor)
        {
            List<T> objectCollection = new List<T>();

            using (var file = File.OpenText(filename))
            {
                while (!file.EndOfStream)
                {
                    var line = file.ReadLine();
                    objectCollection.Add(ParseText(objectConstructor, line));

                }
                file.Close();
            }
            return objectCollection;
        }

        /// <summary>
        /// Parses a csv format of 
        /// string, string, int - into a passed in object constructor method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectConstructor">A func that return an object of type T</param>
        /// <param name="line">A string of format (<string>,<string>,<int>)</param>
        /// <returns>The created object</returns>
        internal static T ParseText<T>(Func<string, string, int, T> objectConstructor, string line)
        {
            var splitValues = line.Split(',');
            if (splitValues.Count() != 3) throw new InvalidDataException("Text file is in an invalid format.");

            return objectConstructor(splitValues[0].Trim(), splitValues[1].Trim(), int.Parse(splitValues[2].Trim()));
        }
    }
}
