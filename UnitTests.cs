using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gradescores
{
    public class UnitTests
    {          
        private Func<string, string, int, NameAndScore> nameAndScoreConstructor = (firstName, lastName, score) =>
        {
            return new NameAndScore(firstName, lastName, score);
        };

        [TestCase("TED, BUNDY, 88")]
        [TestCase("ALLAN, SMITH, 85")]
        [TestCase("FRANCIS, SMITH, 85")]
        public void ValidTextParsingTest(string inputString)
        {
            var splitValues = inputString.Split(',');
            var outputObject = FileToNamesAndScoresParser.ParseText(nameAndScoreConstructor, inputString);

            Assert.AreEqual(splitValues[0].Trim(), outputObject.FirstName);
            Assert.AreEqual(splitValues[1].Trim(), outputObject.LastName);
            Assert.AreEqual(splitValues[2].Trim(), outputObject.Score.ToString());
        }

        [TestCase("Wrong, 88, Order")]
        [TestCase("Too, Much, 85, Data")]
        [TestCase("bad data,")]
        [TestCase("")]       
        public void InvalidTextParsingTest(string inputString)
        {
            bool exceptionFired = false;
            try
            {
                var outputObject = FileToNamesAndScoresParser.ParseText(nameAndScoreConstructor, inputString);
            }
            catch(InvalidDataException)
            {
                exceptionFired = true;
            }
            catch (FormatException)
            {
                exceptionFired = true;
            }

            Assert.IsTrue(exceptionFired);
        }

        [Test]
        public void SortingTest()
        {
            var rawList = new List<NameAndScore>()
            {
                new NameAndScore("TED", "BUNDY", 88),
                new NameAndScore("ALLAN", "SMITH", 85),
                new NameAndScore("MADISON", "KING", 83),
                new NameAndScore("FRANCIS", "sMITH", 85),

            };

            rawList.Sort();

            Assert.That(rawList[0].Equals(new NameAndScore("TED", "BUNDY", 88)));
            Assert.That(rawList[1].Equals(new NameAndScore("ALLAN", "SMITH", 85)));
            Assert.That(rawList[2].Equals(new NameAndScore("FRANCIS", "SMITH", 85)));
            Assert.That(rawList[3].Equals(new NameAndScore("MADISON", "KING", 83)));
        }  
    }
}
