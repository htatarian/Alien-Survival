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
