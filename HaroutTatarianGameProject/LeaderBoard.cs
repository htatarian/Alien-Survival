using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HaroutTatarianGameProject
{
    public class Leaderboard
    {
        public List<Score> ScoreList { get; set; }

        private const int maxHighScores = 5;
        // Path to save and load the leaderboard list from
        private readonly string path;

        public Leaderboard(string path)
        {
            this.path = path;
            ScoreList = new List<Score>();
            LoadScores();
        }

        /// <summary>
        /// Adds a score to leaderboard if the score qualifies,
        /// then refreshes the list
        /// </summary>
        /// <param name="score">Score to be added</param>
        public void Add(Score score)
        {
            if (IsQualifiedScore(score))
            {
                ScoreList.Add(score);
                ScoreList.Sort();
                if (ScoreList.Count > maxHighScores)
                {
                    ScoreList.RemoveAt(ScoreList.Count - 1);
                }
                RefreshScores();
            }
        }

        /// <summary>
        /// Check if a score is qualified to be listed in the leaderboard
        /// </summary>
        /// <param name="score">Score to be checked</param>
        /// <returns>True if qualified, otherwise false</returns>
        private bool IsQualifiedScore(Score score)
        {
            return (ScoreList.Where(s => s.Points >= score.Points).Count() != maxHighScores && ScoreList.Where(s=> s.Equals(score)).Count() == 0);
        }

        /// <summary>
        /// Loads scores from file to list
        /// </summary>
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

        /// <summary>
        /// Write scores from list to file
        /// </summary>
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

        /// <summary>
        /// Write scores from list to file
        /// Load scores from file to list
        /// </summary>
        private void RefreshScores()
        {
            WriteScores();
            LoadScores();
        }
    }
}
