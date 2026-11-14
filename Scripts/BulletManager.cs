using Godot;
using System;
using System.Collections.Generic;

public partial class BulletManager : Node
{
	[Export] private PackedScene bulletObject;

	private List<Bullet> bullets = new List<Bullet>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CreateBullet(BulletType.None, new Spider(), new Vector2(2, 1), 2);
        CreateBullet(BulletType.None, new Spider(), new Vector2(1, 1), 2);

        CreateBullet(BulletType.None, new Spider(), new Vector2(2, 4), 3);

        CreateBullet(BulletType.None, new Spider(), new Vector2(2, 2), 4);

        CreateBullet(BulletType.None, new Spider(), new Vector2(7, 1), 5);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Move(delta);
	}

	/// <summary>
	/// Calls move on all bullets in the scene.
	/// </summary>
	/// <param name="delta">delta time</param>
	private void Move(double delta)
	{
		// Calls move on all bullet objects.
		for (int i = 0; i < bullets.Count; i++)
		{
			bullets[i].Move(delta);
		}
	}

	/// <summary>
	/// Creates a new bullet with the provided info and add it to the bullet list.
	/// </summary>
	public void CreateBullet(BulletType type, Enemy me, Vector2 direction, float speed)
	{
		// create the bullet
		var newBullet = bulletObject.Instantiate();
		AddChild(newBullet);
		bullets.Add(newBullet as Bullet);

		// set the bullets parameters.
		bullets[bullets.Count - 1].Inst(type, me, direction, speed);
	}

	/// <summary>
	/// Handles the destruction of bullets.
	/// </summary>
	/// <param name="me">Bullet that is being destroyed</param>
	public void DestroyBullet(Bullet me)
	{
		bullets.Remove(me);
		me.QueueFree();
	}
}
