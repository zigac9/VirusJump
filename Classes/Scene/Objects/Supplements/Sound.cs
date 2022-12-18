using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace VirusJump.Classes.Scene.Objects.Supplements
{
    public class Sound
    {

        private Song _back;
        private Song _rect;
        private bool _check;
        private bool _playCheck;

        public Sound(ContentManager content) 
        {
            //_back = content.Load<Song>("assets/Pink");
            //_rect = content.Load<Song>("assets/shoot");
            Initialize();
        }

        public void Initialize()
        {
            _check = true;
            _playCheck = true;  
        }

        public bool PlayCheck
        {
            get { return _playCheck; }
            set { _playCheck = value; }
        }

        public Song Background
        {
            get { return _back; }
        }

        public Song Board
        {
            get { return _rect; }
        }
    }
}
