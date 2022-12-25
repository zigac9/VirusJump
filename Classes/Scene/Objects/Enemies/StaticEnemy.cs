using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using VirusJump.Classes.Scene.Objects.Boards;
using VirusJump.Classes.Scene.Objects.Jumpers;
using VirusJump.Classes.Scene.Objects.Scoring;
using VirusJump.Classes.Scene.Objects.Supplements;

namespace VirusJump.Classes.Scene.Objects.Enemies;

public class StaticEnemy
{
    private readonly List<Texture2D> _enemylist;

    private Rectangle _position;

    public StaticEnemy(IReadOnlyDictionary<string, Texture2D> textures)
    {
        TextureRand = 0;
        _enemylist = new List<Texture2D> { textures["assets/ena"], textures["assets/sedem"] };
        Initialize();
    }

    public Rectangle Position
    {
        get => _position;
        set => _position = value;
    }

    public int StRand { get; set; }

    public int TextureRand { get; set; }

    public void Initialize()
    {
        StRand = -1;
        _position = new Rectangle(-200, 800, 60, 55);
    }

    public void Draw(SpriteBatch s)
    {
        s.Draw(_enemylist[TextureRand], _position, Color.White);
    }

    public int Collision(Player player, ref bool collisionCheck)
    {
        //enemy dead
        if (_position.Y - player.PlayerPosition.Y - 45 < 5 && _position.Y - player.PlayerPosition.Y - 45 > -15 &&
            player.Speed.Y > 0 &&
            ((player.PlayerPosition.X + 15 > _position.X &&
              player.PlayerPosition.X + 15 < _position.X + player.PlayerPosition.Width) ||
             (player.PlayerPosition.X + 45 > _position.X &&
              player.PlayerPosition.X + 45 < _position.X + player.PlayerPosition.Width))) return 0;

        //player dead
        if (_position.Y - player.PlayerPosition.Y < 5 && _position.Y - player.PlayerPosition.Y > -35 &&
            player.Speed.Y < 0 &&
            ((player.PlayerPosition.X + 15 > _position.X &&
              player.PlayerPosition.X + 15 < _position.X + player.PlayerPosition.Height) ||
             (player.PlayerPosition.X + 45 > _position.X &&
              player.PlayerPosition.X + 45 < _position.X + player.PlayerPosition.Height)))
            // collisionCheck = false;
            return 1;

        // collisionCheck = true;
        return 2;
    }

    public void Update(Bullet bullet, BoardsList boardsList, Sound sound, Player player,
        bool gameOver, ref bool collisionCheck, ScorClass score, bool thingsCollisionCheck, Trampo trampo, Jetpack jetpack, Spring spring)
    {
        if (score.Score % 430 > 400 && Position.Y > 780)
        {
            var rnd = new Random();
            do
            {
                StRand = rnd.Next(0, boardsList.BoardList.Length - 1);
            } while (boardsList.BoardList[StRand].Position.Y > 0 ||
                     boardsList.BoardList[StRand].Visible == false ||
                     (spring.SRand == trampo.TRand && spring.SRand != -1 && trampo.TRand != -1) ||
                     (spring.SRand == jetpack.JRand && spring.SRand != -1 && jetpack.JRand != -1) ||
                     (trampo.TRand == jetpack.JRand && trampo.TRand != -1 && jetpack.JRand != -1));

            TextureRand = rnd.Next(0, 2);
        }

        if (StRand != -1)
            Position = new Rectangle(boardsList.BoardList[StRand].Position.X,
                boardsList.BoardList[StRand].Position.Y - 53, Position.Width,
                Position.Height);

        if (Collision(player, ref collisionCheck) == 0 && !gameOver && thingsCollisionCheck)
        {
            player.Speed = new Vector2(player.Speed.X, -15);
            StRand = -1;
        }
        else if (Collision(player, ref collisionCheck) == 1 && !gameOver &&
                 thingsCollisionCheck)
        {
            player.Speed = new Vector2(player.Speed.X, 0);
            MediaPlayer.Stop();
            sound.Dead.Play();
            collisionCheck = false;
        }

        if (Position.Y < 780 && StRand == -1)
            Position = new Rectangle(Position.X, Position.Y + 11,
                Position.Width, Position.Height);

        if (Position.Y > 795 || BulletCollision(bullet))
        {
            if (BulletCollision(bullet)) bullet.IsCheck = false;

            StRand = -1;
            Position = new Rectangle(-200, 800, 60, 55);
        }
    }

    private bool BulletCollision(Bullet bullet)
    {
        if (bullet.Position.X > _position.X &&
            bullet.Position.X + bullet.Position.Width < _position.X + _position.Width &&
            bullet.Position.Y > _position.Y &&
            bullet.Position.Y + bullet.Position.Height < _position.Y + _position.Height) return true;
        return false;
    }
}