using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gradescores
{    
    public class NameAndScore : IEquatable<NameAndScore>, IComparable<NameAndScore>
    {
        public NameAndScore(string firstName, string lastName, int score)
        {
            FirstName = firstName;
            LastName = lastName;
            Score = score;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public int Score { get; }

        /// <summary>
        /// Compare by Score descending
        /// Then by Last name ascending
        /// Then by First name ascending
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(NameAndScore other)
        {
            if (other.Score.CompareTo(Score) == 0)
            {
                if (LastName.ToLower().CompareTo(other.LastName.ToLower()) == 0)
                {
                    return FirstName.ToLower().CompareTo(other.FirstName.ToLower());
                }
                else
                {
                    return LastName.ToLower().CompareTo(other.LastName.ToLower());
                }
            }
            else
            {
                return other.Score.CompareTo(Score);
            }

        }

        public bool Equals(NameAndScore other)
        {
            return FirstName.ToLower().Equals(other.FirstName.ToLower()) &&
                   LastName.ToLower().Equals(other.LastName.ToLower()) &&
                   Score.Equals(other.Score);
        }

        public override string ToString() => $"{LastName}, {FirstName}, {Score}";
    }
}
