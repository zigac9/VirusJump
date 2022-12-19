using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using VirusJump.Classes.Scene.Objects.Supplements;

namespace VirusJump.Classes.Scene.Objects.Scoring
{
    public class ScoreManager
    {
        private static readonly string _fileName = "scores.xml";

        public List<Score> Highscores { get; private set; }

        public List<Score> Scores { get; private set; }

        public ScoreManager()
            : this(new List<Score>())
        {

        }

        public ScoreManager(List<Score> scores)
        {
            Scores = scores;

            UpdateHighscores();
        }

        public void Add(Score score)
        {
            var exists = Scores.Any(item => item.PlayerName == "Bob");
            if (exists)
            {
                var index = Scores.FindIndex(a => a.PlayerName == "Bob");
                if (Scores[index].Value < score.Value)
                    Scores[index].Value = score.Value;
            }
            else
            {
                Scores.Add(score);
            }
            Scores = Scores.Where(x => x.PlayerName == "Bob").OrderByDescending(c => c.Value).ToList();

            UpdateHighscores();
        }

        public static ScoreManager Load()
        {
            // If there isn't a file to load - create a new instance of "ScoreManager"
            if (!File.Exists(_fileName))
                return new ScoreManager();

            // Otherwise we load the file

            using (var reader = new StreamReader(new FileStream(_fileName, FileMode.Open)))
            {
                var serilizer = new XmlSerializer(typeof(List<Score>));

                var scores = (List<Score>)serilizer.Deserialize(reader);

                return new ScoreManager(scores);
            }
        }

        public void UpdateHighscores()
        {
            Highscores = Scores.Take(5).ToList(); // Takes the first 5 elements
        }

        public static void Save(ScoreManager scoreManager)
        {
            // Overrides the file if it alreadt exists
            using (var writer = new StreamWriter(new FileStream(_fileName, FileMode.Create)))
            {
                var serilizer = new XmlSerializer(typeof(List<Score>));

                serilizer.Serialize(writer, scoreManager.Scores);
            }
        }
    }
}