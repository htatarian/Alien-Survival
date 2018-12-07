using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HaroutTatarianGameProject
{
    public class LeaderBoard
    {
        private List<Score> scores;
        private readonly string path;

        public LeaderBoard(string path)
        {
            this.path = path;
            scores = new List<Score>();
            LoadScores(path);
        }
        public void Add(Score score)
        {
            if (IsHighScore(score))
            {
                scores.Add(score);
                scores.Sort();
                RefreshScores(path);
            }
        }

        public bool IsHighScore(Score score)
        {
            return scores.Where(s => s.Points >= score.Points).Count() != 10 && scores.Where(s=> s.Equals(score)).Count() == 0;
        }

        private void LoadScores(string path)
        {
            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                List<string> scoreStr = text.Split(',').ToList();
                foreach (string s in scoreStr)
                {
                    string[] args = s.Split('|');
                    scores.Add(new Score(args[0], int.Parse(args[1])));
                }
            }
        }

        private void WriteScores(string path)
        {
            string output = "";
            foreach (Score s in scores)
            {
                output += s.Name + "|" + s.Points + ",";
            }
            output = output.Remove(output.Length - 1);

            File.WriteAllText(path, output);
        }

        public void RefreshScores(string path)
        {
            WriteScores(path);
            LoadScores(path);
        }
    }
}
