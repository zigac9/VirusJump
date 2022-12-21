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
        //player
        Task.Run(() => LoadTexture("assets/DoodleR1"));
        Task.Run(() => LoadTexture("assets/injection"));
        Task.Run(() => LoadTexture("assets/manjetpack"));
        Task.Run(() => LoadTexture("assets/manjetpackL"));
        Task.Run(() => LoadTexture("assets/dead"));
        
        //background
        Task.Run(() => LoadTexture("assets/gradient"));
        Task.Run(() => LoadTexture("assets/kooh"));
        Task.Run(() => LoadTexture("assets/mainMenu1"));
        Task.Run(() => LoadTexture("assets/option"));
        Task.Run(() => LoadTexture("assets/sOn"));
        Task.Run(() => LoadTexture("assets/sOff"));
        Task.Run(() => LoadTexture("assets/notif"));
        Task.Run(() => LoadTexture("assets/pause"));
        Task.Run(() => LoadTexture("assets/sides"));
        Task.Run(() => LoadTexture("assets/gameOver"));
        Task.Run(() => LoadTexture("assets/highscore"));
        
        //bullet
        Task.Run(() => LoadTexture("assets/tir"));
        Task.Run(() => LoadTexture("assets/virus"));
        
        //trampo
        Task.Run(() => LoadTexture("assets/toshak"));
        
        //Spring
        Task.Run(() => LoadTexture("assets/fanar"));
        Task.Run(() => LoadTexture("assets/oFanar"));

        //jetpack
        Task.Run(() => LoadTexture("assets/jet"));
        
        //staticEnemy
        Task.Run(() => LoadTexture("assets/ena"));
        Task.Run(() => LoadTexture("assets/sedem"));

        //movingEnemy
        Task.Run(() => LoadTexture("assets/tri"));
        Task.Run(() => LoadTexture("assets/stiri"));
        Task.Run(() => LoadTexture("assets/pet"));


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