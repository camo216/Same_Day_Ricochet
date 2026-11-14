using Godot;
using System;

public partial class Bullet : CharacterBody2D
{
	// Variables
	private BulletType type = BulletType.None;
	private Enemy orgin = null;
	private BulletManager manager;

	[Export] private Vector2 direction = new Vector2(0,0);
	[Export] private float speed = 0;

	public bool isDeflected = false;

	// Public properties
	public BulletType Type { get; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		manager = GetParent<BulletManager>();
	}

	/// <summary>
	/// Moves the bullet along it's current velocity path.
	/// </summary>
	public void Move()
	{
		Velocity = direction.Normalized() * speed;
		MoveAndSlide();
    }

	/// <summary>
	/// Calls the proper methods to destroy the bullet.
	/// </summary>
	public void DestroySelf()
	{
		manager.DestroyBullet(this);
	}
}
