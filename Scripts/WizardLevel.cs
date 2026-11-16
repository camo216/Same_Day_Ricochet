using Godot;
using System;

public partial class WizardLevel : Node2D
{
    private AudioStreamPlayer music;
    private AudioStreamPlayer musicLoop;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        music = new AudioStreamPlayer();
        music.Stream = GD.Load<AudioStream>("res://Audio/Music/sdr-boss-intro.mp3");
        music.VolumeLinear = 1.6F;
        music.Finished += LoopMusic;
        AddChild(music);
        music.Play();

        musicLoop = new AudioStreamPlayer();
        musicLoop.Stream = GD.Load<AudioStream>("res://Audio/Music/sdr-boss-loop.mp3");
        musicLoop.VolumeLinear = 0.3F;
        musicLoop.Finished += LoopMusic;
        AddChild(musicLoop);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    private void LoopMusic() { musicLoop.Play(); }
}
