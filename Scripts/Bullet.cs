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

	/// <summary>
	/// Called when creating a bullet to set the types and parameters.
	/// </summary>
	/// <param name="newType">Type of bullet</param>
	/// <param name="creator">enemy that created the bullet</param>
	/// <param name="newDirection">direction vector of the bullet</param>
	/// <param name="newSpeed">speed of the bullet</param>
	public void Inst(BulletType newType, Enemy creator, Vector2 newDirection, float newSpeed)
	{
		type = newType;
		orgin = creator;
        manager = GetParent<BulletManager>();
		direction = newDirection;
		speed = newSpeed;
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
