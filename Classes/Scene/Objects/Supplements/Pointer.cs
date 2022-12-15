using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;
using SharpDX.Direct2D1.Effects;
using System.Reflection;


namespace VirusJump.Classes.Scene.Objects.Supplements
{
    public class Pointer
    {
        private Vector2 _position;
        private SpriteSheet _spriteSheet;
        private Vector2 _scale;
        private AnimatedSprite _animatedSprite;


        public Pointer(ContentManager content) 
        {
            _position = new Vector2(0, 0);
            _scale = new Vector2(0.5f, 0.5f);
            _spriteSheet = content.Load<SpriteSheet>("assets/shoot.sf", new JsonContentLoader());
            _animatedSprite = new AnimatedSprite(_spriteSheet);
        }

        public void Draw(SpriteBatch sp)
        {
            _animatedSprite.Draw(sp, _position, 0f, _scale);
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public AnimatedSprite GetAnimatedSprite
        {
            get { return _animatedSprite; }
        }
    }
}
