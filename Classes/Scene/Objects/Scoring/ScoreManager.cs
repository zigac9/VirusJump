using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace VirusJump.Classes.Scene.Objects.Scoring;

public class ScoreManager
{
    private static readonly string _fileName = "scores.xml";

    public ScoreManager()
        : this(new List<Score>())
    {
    }

    public ScoreManager(List<Score> scores)
    {
        Scores = scores;
        Highscores = new List<int>(5) { 0, 0, 0, 0, 0 };

        UpdateHighscores();
    }

    public List<int> Highscores { get; }

    public List<Score> Scores { get; private set; }

    public void Add(Score score, string playername)
    {
        var exists = Scores.Any(item => item.PlayerName == playername);
        if (exists)
        {
            var index = Scores.FindIndex(a => a.PlayerName == playername);
            if (Scores[index].Value < score.Value)
                Scores[index].Value = score.Value;
        }
        else
        {
            Scores.Add(score);
        }

        Scores = Scores.OrderByDescending(c => c.Value).ToList();

        UpdateHighscores();
    }

    public int BestOfYou(string playername)
    {
        var bestofyou = new List<Score>();
        bestofyou = Scores.Where(name => name.PlayerName == playername).OrderByDescending(c => c.Value).ToList();
        return bestofyou[0].Value;
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
        if (Scores.Count > 0)
            Highscores[0] = Scores[0].Value;
        else
            Highscores[0] = 0;
        if (Scores.Count > 1)
            Highscores[1] = Scores[1].Value;
        else
            Highscores[1] = 0;
        if (Scores.Count > 2)
            Highscores[2] = Scores[2].Value;
        else
            Highscores[2] = 0;
        if (Scores.Count > 3)
            Highscores[3] = Scores[3].Value;
        else
            Highscores[3] = 0;
        if (Scores.Count > 4)
            Highscores[4] = Scores[4].Value;
        else
            Highscores[4] = 0;

        // Highscores = Scores.Take(5).ToList(); // Takes the first 5 elements
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