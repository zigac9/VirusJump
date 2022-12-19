﻿using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Content;

namespace VirusJump.Classes.Scene.Objects.Supplements
{
    public class Sound
    {

        private Song _background;
        private Song _end;
        private SoundEffect _board;
        private bool _check;
        private bool _playCheck;

        public Sound(ContentManager content) 
        {
            _background = content.Load<Song>("assets/background");
            _end = content.Load<Song>("assets/patmat");
            _board = content.Load<SoundEffect>("assets/jump");
            Initialize();
        }

        public void Initialize()
        {
            _check = true;
            _playCheck = true;  
        }

        public bool PlayCheck
        {
            get => _playCheck;
            set => _playCheck = value;
        }
        
        public Song Background => _background;

        public Song End => _end;

        public SoundEffect Board => _board;
    }
}
