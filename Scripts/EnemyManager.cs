using Godot;
using System;
using System.Collections.Generic;

public partial class EnemyManager : Node
{
	// Enemy objects
	[Export] private PackedScene spiderObject;

	// Enemy list
	private List<Enemy> enemies = new List<Enemy>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Move(delta);
	}

	/// <summary>
	/// Updates the movement of all enemies
	/// </summary>
	/// <param name="delta">delta time</param>
    private void Move(double delta)
    {
        // Calls move on all bullet objects.
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Move(delta);
        }
    }

	/// <summary>
	/// Handles the creation of Enemies
	/// </summary>
	/// <param name="type">Type of enemy</param>
	/// <param name="position">Location enemy is being spawned</param>
	public void CreateEnemy(string type, Vector2 position)
	{
		// Determine enemy type
		Node newEnemy = null;
		switch (type)
		{
			case "Spider":
				newEnemy = spiderObject.Instantiate();
                AddChild(newEnemy);
				enemies.Add(newEnemy as Spider);

				enemies[enemies.Count - 1].Position = position;
				break;
		}
	}

	/// <summary>
	/// Handles the destruction of enemies
	/// </summary>
	/// <param name="me">enemy being destroyed</param>
	public void DestroyEnemy(Enemy me)
	{
		enemies.Remove(me);
		me.QueueFree();
	}
}
