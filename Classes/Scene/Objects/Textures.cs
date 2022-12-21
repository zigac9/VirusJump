using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace VirusJump.Classes.Scene.Objects;

public class Textures
{
    private ContentManager _content;
    private Dictionary<string, Texture2D> textures = new();
    private readonly object _lock = new();
    

    public Textures(ContentManager content)
    {
        _content = content;
        Task.Run(() => LoadTexture("assets/DoodleR1"));
        Task.Run(() => LoadTexture("assets/injection"));
        Task.Run(() => LoadTexture("assets/manjetpack"));
        Task.Run(() => LoadTexture("assets/manjetpackL"));
        Task.Run(() => LoadTexture("assets/dead"));
    }
    
    private void LoadTexture(string textureName)
    {
        // Load the texture using the ContentManager
        var texture = _content.Load<Texture2D>(textureName);

        // Use the lock statement to synchronize access to the dictionary
        lock (_lock)
        {
            // Add the texture to the dictionary
            textures.Add(textureName, texture);
        }
    }

    public Dictionary<string, Texture2D> Textures1 => textures;
}