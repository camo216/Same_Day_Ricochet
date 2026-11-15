using Godot;
using System;

public partial class DoorTrigger : Area2D
{
	public int level = (int)Stage.Blue;
	
	public bool changeLevel;
    public bool cleared = false;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
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
