using System;

namespace HaroutTatarianGameProject
{
    public class Score : IComparable<Score>
    {
        public string Name { get; set; }
        public int Points { get; set; }

        public Score(string name, int points)
        {
            Name = name;
            Points = points;
        }

        /// <summary>
        /// Checks if score is better than the other.
        /// if previously exists, remains the same
        /// </summary>
        /// <param name="other">Score to compare</param>
        /// <returns></returns>
        public int CompareTo(Score other)
        {
            int result = -1;

            if (Equals(other))
            {
                result = 0;
            }
            else if (other.Points == Points || other.Points > Points)
            {
                result = 1;
            }

            return result;
        }

        /// <summary>
        /// Check if score is identical to the other
        /// </summary>
        /// <param name="score">Score to compare to</param>
        /// <returns>True if identical, otherwise false</returns>
        public bool Equals(Score score)
        {
            bool result = false;

            if (score is Score other)
            {
                result = Name == other.Name && Points == other.Points;
            }

            return result;
        }
    }
}
