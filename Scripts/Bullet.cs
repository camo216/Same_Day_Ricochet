using Godot;
using System;

public partial class Bullet : Node
{
	// Variables
	private BulletType type;
	private Enemy orgin;

	private Vector2 dirction;
	private float speed;

	public bool isDeflected;

	// Public properties
	public BulletType Type { get; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
