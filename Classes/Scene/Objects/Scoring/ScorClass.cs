using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static VirusJump.Game1;

namespace VirusJump.Classes.Scene.Objects.Scoring;

public class ScorClass
{
    private readonly Vector2 _position;

    public ScorClass(Dictionary<string, SpriteFont> SpriteFontsLoad)
    {
        Score = 0;
        _position = new Vector2(15f, 4f);
        Check = true;
        SFont = SpriteFontsLoad["assets/SpriteFont1"];
    }

    public bool Check { get; set; }

    public SpriteFont SFont { get; set; }

    public int Score { get; set; }

    public void Draw(SpriteBatch sp, GameStateEnum gameState)
    {
        sp.DrawString(SFont, Score.ToString(), _position, Color.White, 0f, new Vector2(0, 0), new Vector2(1, 1),
            SpriteEffects.None, 0f);
    }
}