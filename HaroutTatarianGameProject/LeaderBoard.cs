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
            LoadScores();
        }

        public void Add(Score score)
        {
            if (IsQualifiedScore(score))
            {
                scores.Add(score);
                scores.Sort();
                RefreshScores();
            }
        }

        private bool IsQualifiedScore(Score score)
        {
            return scores.Where(s => s.Points >= score.Points).Count() != 10 && scores.Where(s=> s.Equals(score)).Count() == 0;
        }

        private void LoadScores()
        {
            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                if (!(string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text)))
                {
                    List<string> scoreStr = text.Split(',').ToList();
                    foreach (string s in scoreStr)
                    {
                        string[] args = s.Split('|');
                        scores.Add(new Score(args[0], int.Parse(args[1])));
                    }
                }
            }
        }

        private void WriteScores()
        {
            string output = "";
            foreach (Score s in scores)
            {
                output += s.Name + "|" + s.Points + ",";
            }
            output = output.Remove(output.Length - 1);

            File.WriteAllText(path, output);
        }

        private void RefreshScores()
        {
            WriteScores();
            LoadScores();
        }
    }
}
