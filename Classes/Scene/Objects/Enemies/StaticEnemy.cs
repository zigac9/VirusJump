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

namespace VirusJump.Classes.Scene.Objects.Enemies
{
    public class StaticEnemy
    {

        public Texture2D _staticEnemy;
        public Rectangle _position;

        public StaticEnemy() 
        {
            Initialize();
        }

        public void Initialize()
        {

        }
    }
}
