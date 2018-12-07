using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HaroutTatarianGameProject
{
    public class Leaderboard
    {
        public List<Score> ScoreList { get; set; }
        private readonly string path;

        public Leaderboard(string path)
        {
            this.path = path;
            ScoreList = new List<Score>();
            LoadScores();
        }

        public void Add(Score score)
        {
            if (IsQualifiedScore(score))
            {
                ScoreList.Add(score);
                ScoreList.Sort();
                if (ScoreList.Count > 5)
                {
                    ScoreList.RemoveAt(ScoreList.Count - 1);
                }
                RefreshScores();
            }
        }

        private bool IsQualifiedScore(Score score)
        {
            return (ScoreList.Where(s => s.Points >= score.Points).Count() != 5 && ScoreList.Where(s=> s.Equals(score)).Count() == 0);
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
                        ScoreList.Add(new Score(args[0], int.Parse(args[1])));
                    }
                }
            }
        }

        private void WriteScores()
        {
            string output = "";
            foreach (Score s in ScoreList)
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
