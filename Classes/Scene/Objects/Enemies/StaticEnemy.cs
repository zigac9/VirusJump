using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using VirusJump.Classes.Scene.Objects.Supplements;
using VirusJump.Classes.Graphics;
using VirusJump.Classes.Scene.Objects.Boards;
using VirusJump.Classes.Scene.Objects.Jumpers;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Content;

namespace VirusJump.Classes.Scene.Objects.Enemies
{
    public class StaticEnemy
    {

        public Texture2D _staticEnemy;
        public Rectangle _position;
        public bool _visible;
        public StaticEnemy(ContentManager content) 
        {
            _staticEnemy = content.Load<Texture2D>("Doodle_jumpContent/e3");
            Initialize();
        }

        public void Initialize()
        {
            _position = new Rectangle(-200, 800, 60, 55);
            _visible = false;
        }

        public void Draw(SpriteBatch s)
        {
            s.Draw(_staticEnemy, _position, Color.White);
        }

        public int Collision(Player player, bool collisionCheck)
        {
            if (_position.Y - player.PlayerPosition.Y - 45 < 5 && _position.Y - player.PlayerPosition.Y - 45 > -15 && player.Speed.Y > 0 && ((player.PlayerPosition.X + 15 > _position.X && player.PlayerPosition.X + 15 < _position.X + 60) || (player.PlayerPosition.X + 45 > _position.X && player.PlayerPosition.X + 45 < _position.X + 60)))
            {
                return (0);// our enemy is dead!
            }
            else if (_position.Y - player.PlayerPosition.Y < 5 && _position.Y - player.PlayerPosition.Y > -35 && player.Speed.Y < 0 && ((player.PlayerPosition.X + 15 > _position.X && player.PlayerPosition.X + 15 < _position.X + 60) || (player.PlayerPosition.X + 45 > _position.X && player.PlayerPosition.X + 45 < _position.X + 60)))
            {
                collisionCheck = false;
                return (1);// you are dead!
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
            get { return _position; }
            set { _position = value; }
        }

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }
    }
}
