using Godot;
using System;

public partial class Bullet : CharacterBody2D
{
	// Variables
	private BulletType type = BulletType.None;
	private Enemy origin = null;
	private BulletManager manager;

	[Export] private Vector2 direction = new Vector2(0,0);
	[Export] private float speed = 0;

	[Export] public bool isDeflected = false;

	// Public properties
	public BulletType Type { get => type; }

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
		origin = creator;
        manager = GetParent<BulletManager>();
		direction = newDirection;
		speed = newSpeed;
    }

	/// <summary>
	/// Moves the bullet along it's current velocity path.
	/// </summary>
	/// <param name="delta">delta time</param>
	public void Move(double delta)
	{
		// Determine direction and velocity.
		if (isDeflected)
		{
            direction = new Vector2(origin.Position.X - Transform.Origin.X, origin.Position.Y - Transform.Origin.Y);  
        }
        Velocity = direction.Normalized() * speed;

		// Move the bullet.
        MoveAndCollide(Velocity * (float)delta);
    }

	/// <summary>
	/// Calls the proper methods to destroy the bullet.
	/// </summary>
	public void DestroySelf()
	{
		manager.DestroyBullet(this);
	}
}
