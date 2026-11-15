using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Door : CharacterBody2D
{
	private Color doorColor = new Color(169, 94, 0, 1);
	public bool cleared;
	[Export]private EnemyManager enemyManager;
	int EnemyCount;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		EnemyCount = enemyManager.EnemyCount;
		if (EnemyCount == 0)
		{
			cleared = true;
            QueueRedraw();
        }
		else
		{
			CollisionLayer = 1;
			QueueRedraw();
		}
		if (cleared)
		{
			CollisionLayer = 2;
			cleared = false;
            QueueRedraw();
        }
	}


	public override void _Draw()
	{
		if (!cleared)
		{
			DrawRect(new Rect2(0, -10, 20, 20), doorColor, false);
		}
		if (cleared)
		{
            DrawRect(new Rect2(0, -10, 20, 20), Color.Color8(0,0,0,0), false);
        }
			
	}
}
