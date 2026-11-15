using Godot;
using System;

public abstract partial class Enemy : CharacterBody2D
{
	protected int health;
	protected Vector2 direction;
	protected float speed;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public abstract void Move(double delta);

	//public abstract void DoAction(double delta);
}
