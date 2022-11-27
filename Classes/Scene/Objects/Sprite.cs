using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusJump.Classes.Scene.Objects
{
    class Sprite
    {
        private string _texture;
        private Rectangle _sourceRectangle;
        private Vector2 _origin;
        private float _rotation;
        private SpriteEffects _spriteEffects;
        private int _scale;
        private int _layerDepth;
        private Color _color;

        public Sprite()
        {
            _texture = null;
            _sourceRectangle = new Rectangle();
            _origin = new Vector2();
        }

        public string Texturee
        {
            get => _texture;
            set => _texture = value;
        }

        public Rectangle SourceRectangle
        {
            get => _sourceRectangle;
            set => _sourceRectangle = value;
        }

        public Vector2 Origin
        {
            get => _origin;
            set => _origin = value;
        }

        public float Rotate
        {
            get => _rotation;
            set => _rotation = value;
        }

        public SpriteEffects SpriteEffect
        {
            get => _spriteEffects;
            set => _spriteEffects = value;
        }

        public int LayerDepth
        {
            get => _layerDepth;
            set => _layerDepth = value;
        }

        public int Scale
        {
            get => _scale;
            set => _scale = value;
        }

        public Color Colorr
        {
            get => _color;
            set => _color = value;
        }
    }
}
