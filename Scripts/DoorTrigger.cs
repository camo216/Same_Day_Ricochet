using Godot;
using System;

public partial class DoorTrigger : Area2D
{
	public int level = (int)Stage.Blue;

	private AudioStreamPlayer levelMusic;

    public bool changeLevel;
    public bool cleared = false;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        levelMusic = new AudioStreamPlayer();
        levelMusic.Stream = GD.Load<AudioStream>("res://Audio/Music/sdr-fight.mp3");
		levelMusic.VolumeLinear = 0.3F;
        levelMusic.Finished += LoopMusic;
        AddChild(levelMusic);
        levelMusic.Play();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void LoopMusic() { levelMusic.Play(); }

	private void OnCollision(CharacterBody2D collision)
	{
		if (collision is Player p && cleared == true)
		{
			changeLevel = true;
			ChangeLevel();
		}
		
	}
	private void ChangeLevel()
	{
		switch (level)
		{
			case (int)Stage.Blue:
				level = (int)Stage.Red;
				
				changeLevel = false;
				break;
			case (int)Stage.Red:
				level = (int)Stage.Green;
				changeLevel = false;
				break;
			case (int)Stage.Green:
				level = (int)Stage.Wizard;
				changeLevel = false;
				break;
		}
	}

    
}
