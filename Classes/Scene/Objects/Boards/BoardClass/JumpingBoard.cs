using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;
using SharpDX.Direct3D9;
using SharpDX.Direct2D1.Effects;
using System.Diagnostics;

namespace VirusJump.Classes.Scene.Objects.Boards.BoardClass
{
    public class JumpingBoard
    {
        private Texture2D _texture;
        private Rectangle _position;
        private bool _jump;
        private Vector2 _scale;
        private SpriteSheet _spriteSheet;

        public JumpingBoard(ContentManager content, Rectangle position)
        {
            _texture = content.Load<Texture2D>("Doodle_jumpContent/spring_in");
            _position = position;
            _jump = false;
            _scale = new Vector2(1f, 1f);
            //_spriteSheet = content.Load<SpriteSheet>("spritesheet.sf", new JsonContentLoader());
        }

        public void DrawSprite(AnimatedSprite s, SpriteBatch sp)
        {
            s.Draw(sp, new Vector2(_position.X,_position.Y), 0f, _scale);
        }


        public bool Collision(Player player)
        {
            Debug.WriteLine(player.PlayerPosition.ToString());
            Debug.WriteLine(_position.ToString());
            if (player.PlayerPosition.X + 15 > _position.X && player.PlayerPosition.X + 15 < _position.X + 60 || player.PlayerPosition.X + 45 > _position.X && player.PlayerPosition.X + 45 < _position.X + 60)
                if (_position.Y - player.PlayerPosition.Y - 60 < 5 && _position.Y - player.PlayerPosition.Y - 60 > -20 && player.PlayerSpeed.Y > 0)
                    return true;
                else return false;
            else return false;
        }

        public float TextureWidth
        {
            get { return _texture.Width * _scale.X; }
        }

        public Rectangle JumpingBoardPosition
        {
            get { return _position; }
            set { _position = value; }
        }

        public bool IsJumped
        {
            get { return _jump; }
            set { _jump = value; }
        }

        public SpriteSheet SpriteSheet
        {
            get { return _spriteSheet; }
            set { _spriteSheet = value; }
        }
    }
}
