using Godot;
using System;
using System.Collections.Generic;

public partial class EnemyManager : Node
{
	// Enemy objects
	[Export] private PackedScene spiderObject;

	// Enemy list
	private List<Enemy> enemies = new List<Enemy>();

	// Player reference
	[Export] private Node2D player;

	// Bullet manager reference
	[Export] private BulletManager bulletManager;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CreateEnemy("Spider", new Vector2(100, 0));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Move(delta);
		EnemyAction(delta);
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
	/// Attempts to have all enemies perform an action
	/// </summary>
	/// <param name="delta">delta time</param>
	private void EnemyAction(double delta)
	{
		for (int i = 0; i < enemies.Count; i++)
		{
			enemies[i].DoAction(delta);
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
		Node newEnemy;
		switch (type)
		{
			case "Spider":
				newEnemy = spiderObject.Instantiate();
                AddChild(newEnemy);
                enemies.Add(newEnemy as Spider);

				enemies[enemies.Count - 1].Inst(player, bulletManager);

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
