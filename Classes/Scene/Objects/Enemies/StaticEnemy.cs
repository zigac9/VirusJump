using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VirusJump.Classes.Scene.Objects.Supplements;
using Microsoft.Xna.Framework.Content;

namespace VirusJump.Classes.Scene.Objects.Enemies
{
    public class StaticEnemy
    {
        private readonly List<Texture2D> _enemylist;

        private Rectangle _position;

        private int _stRand;
        private int _textureint;

        public StaticEnemy(ContentManager content) 
        {
            _textureint = 0;
            var staticEnemy1 = content.Load<Texture2D>("assets/ena");
            var staticEnemy2 = content.Load<Texture2D>("assets/sedem");
            _enemylist = new List<Texture2D> { staticEnemy1, staticEnemy2 };
            Initialize();
        }

        public void Initialize()
        {
            _stRand = -1;
            _position = new Rectangle(-200, 800, 60, 55);
        }

        public void Draw(SpriteBatch s)
        {
            s.Draw(_enemylist[_textureint], _position, Color.White);
        }

        public int Collision(Player player, ref bool collisionCheck)
        {
            //enemy dead
            if (_position.Y - player.PlayerPosition.Y - 45 < 5 && _position.Y - player.PlayerPosition.Y - 45 > -15 && player.Speed.Y > 0 && ((player.PlayerPosition.X + 15 > _position.X && player.PlayerPosition.X + 15 < _position.X + player.PlayerPosition.Width) || (player.PlayerPosition.X + 45 > _position.X && player.PlayerPosition.X + 45 < _position.X + player.PlayerPosition.Width)))
            {
                return (0);
            }
            //player dead
            else if (_position.Y - player.PlayerPosition.Y < 5 && _position.Y - player.PlayerPosition.Y > -35 && player.Speed.Y < 0 && ((player.PlayerPosition.X + 15 > _position.X && player.PlayerPosition.X + 15 < _position.X + player.PlayerPosition.Height) || (player.PlayerPosition.X + 45 > _position.X && player.PlayerPosition.X + 45 < _position.X + player.PlayerPosition.Height)))
            {
                collisionCheck = false;
                return (1);
            }
            else
            {
                collisionCheck = true;
                return (2);
            }
        }

        public bool BulletCollision(Bullet bullet)
        {
            if (bullet.Position.X > _position.X && bullet.Position.X + bullet.Position.Width < _position.X + _position.Width && bullet.Position.Y > _position.Y && bullet.Position.Y + bullet.Position.Height < _position.Y + _position.Height) return true;
            else return false;
        }

        public Rectangle Position
        {
            get => _position;
            set => _position = value;
        }

        public int StRand
        {
            get => _stRand;
            set => _stRand = value;
        }

        public int TextureRand
        {
            get => _textureint;
            set => _textureint = value;
        }
    }
}
