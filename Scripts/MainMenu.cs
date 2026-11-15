using Godot;
using System;
[Tool]

public partial class MainMenu : Control
{
	private Rect2 rect = new Rect2(0, 0, 1200, 900);
	private AudioStreamPlayer music;
	private AudioStreamPlayer musicLoop;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		music = new AudioStreamPlayer();
		music.Stream = GD.Load<AudioStream>("res://Audio/Music/sdr-theme-intro.mp3");
		music.Autoplay = true;
		music.Finished += PlayLoop;
		AddChild(music);

		musicLoop = new AudioStreamPlayer();
		musicLoop.Stream = GD.Load<AudioStream>("res://Audio/Music/sdr-theme-loop.mp3");
        musicLoop.Finished += PlayLoop;
        AddChild(musicLoop);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _Draw()
	{
		DrawRect(rect, Color.Color8(0, 0, 0, 255));
		base._Draw();
	}

	public void LoadLevel()
	{
		var scene = GD.Load<PackedScene>("res://Game.tscn").Instantiate();
        GetParent().AddChild(scene);

        QueueFree();
	}

	public void PlayLoop()
    {
        musicLoop.Play();
    }
}
