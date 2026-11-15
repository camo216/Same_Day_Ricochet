using Godot;
using System;

public abstract partial class Enemy : CharacterBody2D
{
	[Export] protected int health;
    public string type;
	protected Vector2 direction;
	protected float speed;
	protected Node2D player;
    protected BulletManager bulletManager;
    protected Random rng = new Random();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public abstract void Inst(Node2D newPlayer, BulletManager bullet);

	public abstract void Move(double delta);

	public abstract void DoAction(double delta);

    /// <summary>
    /// Finds the direction the bullet should travel in.
    /// </summary>
    /// <param name="angle">angle from the player in degrees</param>
    /// <returns>Direction vector of bullet.</returns>
    protected Vector2 BulletDirection(float angle)
    {
        Vector2 direction = new Vector2(player.Position.X - Transform.Origin.X, player.Position.Y - Transform.Origin.Y);
        direction = direction.Rotated(Mathf.DegToRad(angle));
		direction = direction.Normalized();

        return direction;
    }

    /// <summary>
    /// Has the enemy take damage when getting hit.
    /// </summary>
    public void TakeDamage()
    {
        health--;
    }
}
