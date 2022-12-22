using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;

namespace VirusJump.Classes.Scene.Objects.Supplements;

public class Pointer
{
    public Pointer(ContentManager content)
    {
        Position = new Vector2(0, 0);
        var spriteSheet = content.Load<SpriteSheet>("assets/shoot.sf", new JsonContentLoader());
        GetAnimatedSprite = new AnimatedSprite(spriteSheet);
    }

    public Vector2 Position { get; set; }

    public AnimatedSprite GetAnimatedSprite { get; }

    public void Draw(SpriteBatch sp)
    {
        GetAnimatedSprite.Draw(sp, Position, 0f, new Vector2(0.5f, 0.5f));
    }
}