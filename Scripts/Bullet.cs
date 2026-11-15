using Godot;
using System;

public partial class Bullet : Area2D
{
	// Variables
	private Vector2 drawVector = new Vector2(0, 0);
	private BulletType type = BulletType.None;
	private Enemy origin = null;
	private BulletManager manager;
	public Color color;
	private string drawType;


	[Export] private Vector2 direction = new Vector2(0, 0);
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
		drawType = origin.type;
		manager = GetParent<BulletManager>();
		direction = newDirection;
		speed = newSpeed;
		Position = new Vector2(origin.Transform.Origin.X, origin.Transform.Origin.Y);
	}

	public override void _Process(double delta)
	{
		QueueRedraw();
	}

	/// <summary>
	/// Moves the bullet along it's current velocity path.
	/// </summary>
	/// <param name="delta">delta time</param>
	public void Move(double delta)
	{
		// check if origin still exists.
		if (origin != null)
		{
			if (origin.isDying)
			{
				origin = null;
			}
		}
		// Determine direction and velocity.
		if (isDeflected && origin != null)
		{
			direction = new Vector2(origin.Position.X - Transform.Origin.X, origin.Position.Y - Transform.Origin.Y);
		}
		// Move the bullet.
		Position += direction.Normalized() * speed * (float)delta;
	}

	/// <summary>
	/// Detects collisions with the bullet.
	/// </summary>
	/// <param name="collision">object being collided with.</param>
	private void OnCollision(PhysicsBody2D collision)
	{
		if (collision is Player p)
		{
			if (!isDeflected)
			{
				p.HandleBullets(this);
			}
		}
		else if (collision is Enemy e)
		{
			if (isDeflected)
			{
				e.TakeDamage();
				DestroySelf();
			}
		}
		else
		{
			DestroySelf();
		}
	}

	/// <summary>
	/// Calls the proper methods to destroy the bullet.
	/// </summary>
	public void DestroySelf()
	{
		manager.DestroyBullet(this);
	}

	public override void _Draw()
	{

		switch (type){
			case BulletType.Blue: color = Color.Color8(0, 0, 255, 255); 
				break;
			case BulletType.Green: color = Color.Color8(0, 255, 0, 255);
				break;
			case BulletType.Red: color = Color.Color8(255, 0, 0, 255);
				break;
			case BulletType.Web: color = Color.Color8(255, 255, 255, 255);
				break;
			case BulletType.Unblockable: color = Color.Color8(255, 255, 0, 255);
				break;
		}
		//if (origin == null)
		//{
		//	GD.Print("Something went wrong with bullets origin, could not find the enemy who fired");
		//}
		if (drawType == "Spider")
		{
			DrawCircle(drawVector, 2, color);
		}
		else if (drawType == "Book")
		{

			DrawRect(new Rect2 (drawVector, 10, 10), color, false, 1);
		}
		else if (drawType == "Amulet")
		{
			DrawRect(new Rect2(drawVector, 10, 10), color, true);
		}
		else if (drawType == "Sword")
		{
			DrawArc(drawVector, 10, 0, 3, 10, color, 3);
		}

	}
}
