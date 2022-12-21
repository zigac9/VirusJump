using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;

namespace VirusJump.Classes.Scene.Objects.Supplements
{
    public class Pointer
    {
        private readonly AnimatedSprite _animatedSprite;

        private Vector2 _position;

        public Pointer(ContentManager content) 
        {
            _position = new Vector2(0, 0);
            var spriteSheet = content.Load<SpriteSheet>("assets/shoot.sf", new JsonContentLoader());
            _animatedSprite = new AnimatedSprite(spriteSheet);
        }

        public void Draw(SpriteBatch sp)
        {
            _animatedSprite.Draw(sp, _position, 0f, new Vector2(0.5f, 0.5f));
        }

        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public AnimatedSprite GetAnimatedSprite => _animatedSprite;
    }
}
