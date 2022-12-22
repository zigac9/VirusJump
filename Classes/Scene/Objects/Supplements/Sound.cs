using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace VirusJump.Classes.Scene.Objects.Supplements;

public class Sound
{
    private bool _check;

    public Sound(ContentManager content)
    {
        Background = content.Load<Song>("assets/background");
        End = content.Load<Song>("assets/patmat");
        Board = content.Load<SoundEffect>("assets/jump");
        PlayerShoot = content.Load<SoundEffect>("assets/shootPlayer");
        EnemyShoot = content.Load<SoundEffect>("assets/enemyShot");
        Dead = content.Load<SoundEffect>("assets/deadSound");
        Initialize();
    }

    public bool PlayCheck { get; set; }

    public Song Background { get; }

    public Song End { get; }

    public SoundEffect Board { get; }

    public SoundEffect PlayerShoot { get; }

    public SoundEffect EnemyShoot { get; }

    public SoundEffect Dead { get; }

    public void Initialize()
    {
        _check = true;
        PlayCheck = true;
    }
}