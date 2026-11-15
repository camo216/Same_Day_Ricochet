using Godot;
using System;

public partial class BlueDoorTrigger : Area2D
{
	public int level = 0;
	Player player = new Player();
	private AudioStreamPlayer levelMusic;

    public bool changeLevel;
    public bool cleared = true;
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
		//GD.Print("Collide");
		if (collision is Player p)
		{
			
			ChangeLevel();
		}
	}
	
	public void ChangeLevel()
	{
		GetTree().ChangeSceneToFile("res://Objects/red_level.tscn");
	}

    
}
