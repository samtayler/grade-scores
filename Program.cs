using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gradescores
{
    class Program
    {
        private static Func<string, string, int, NameAndScore> nameAndScoreConstructor = (firstName, lastName, score) =>
        {
            return new NameAndScore(firstName, lastName, score);
        };

        static void Main(string[] args)
        {
            TextWriterTraceListener myWriter = new TextWriterTraceListener(System.Console.Out);
            Debug.Listeners.Add(myWriter);

            if (args.Count() != 1)
            {
                Debug.WriteLine("Invalid number of arguments. There can only be one!");
                return;
            }

            string filePath = args[0];
            try
            {
                var listOfNamesAndScores = FileToNamesAndScoresParser.FileToNamesAndScores(filePath, nameAndScoreConstructor);
                listOfNamesAndScores.Sort();
                WriteObjectsToFile(filePath, listOfNamesAndScores);
            }
            catch (IOException ioEx)
            {
                Debug.WriteLine(ioEx.Message);                
            }
            catch (InvalidDataException ex)
            {
                Debug.WriteLine(ex.Message);                
            }

        }

        private static void WriteObjectsToFile(string filePath, List<NameAndScore> listOfNamesAndScores)
        {
            var newFilePath = Path.GetDirectoryName(filePath) + @"\" + Path.GetFileNameWithoutExtension(filePath);
            newFilePath += "-graded.txt";

            Debug.WriteLine($"New filename is {newFilePath}");

            using (var fStream = File.CreateText(newFilePath))
            {
                foreach (var nameScore in listOfNamesAndScores)
                {
                    fStream.WriteLine(nameScore.ToString());
                    Debug.WriteLine($"Write {nameScore} to file.");
                }
            }
            Debug.WriteLine("Operation Complete.");
        }
    }
}
